using AutoMapper;
using EFCore.BulkExtensions;
using Humanizer;
using LinqToDB.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MoreLinq;
using Newtonsoft.Json;
using Nsd.Common;
using Nsd.Common.Extensions;
using Nsd.Repository.Base;
using Nsd.Repository.Ef.Model.Entities;
using Nsd.Repository.Ef.Repositories.CalendarUtils;
using Nsd.Repository.Ef.Repositories.ControlUtils;
using Nsd.Repository.Ef.Repositories.Lei;
using Nsd.Repository.Ef.Repositories.Lei.Extensions;
using Nsd.Repository.Ef.Repositories.Lei.Models;
using Nsd.Repository.Ef.StorageProcedureEntities.Lei;
using Nsd.Resources;
using Nsd.Service.Base;
using Nsd.Service.CommonServices.Certificate;
using Nsd.Service.CommonServices.Email;
using Nsd.Service.CorpDb.Services.Lei.Model;
using Nsd.Service.Rest.External.Services.Lei;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Linq;
using Task = System.Threading.Tasks.Task;

namespace Nsd.Service.CorpDb.Services.Lei
{
    public class LeiPreCheckService : BaseService, ILeiPreCheckService
    {
        private readonly IControlRepository _controlRepository;
        private readonly ICertificateService _certificateService;
        private readonly ILeiPreCheckRestService _leiPreCheckRestService;
        private readonly ILeiPreCheckRepository _leiPreCheckRepository;
        private readonly X509Certificate2 _gleifCertificate;
        private readonly IStoredProcedureRepository _storedProcedureRepository;
        private readonly ILeiPublicPreCheckRepository _leiPublicPreCheckRepository;
        private readonly ILeiPublicNamesPreCheckRepository _leiPublicNamesPreCheckRepository;
        private readonly ILeiPublicLevel2PreCheckRepository _leiPublicLevel2PreCheckRepository;
        private readonly ILeiPublicNamesLevel2PreCheckRepository _leiPublicNamesLevel2PreCheckRepository;
        private readonly ILeiPublicRepository _leiPublicRepository;
        private readonly ILeiVersionService _leiVersionService;
        private readonly IReadOnlyRepository<CmpAnotherNames> _cmpAnotherNames;
        private readonly IReadOnlyRepository<LeiCodeHistory> _leiCodeHistoryRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSendService _emailSendService;
        private readonly IEmailGroupRecipientsGetter _emailGroupRecipientsGetter;
        protected ITokenizer500 _tokenizer;
        private readonly ILeiPublicLevel2Repository _leiPublicLevel2Repository;
        private readonly ICalendarRepository _calendarRepository;

        // Группа рассылки для формирования уведомлений об ошибках
        private const string GleifDataQualityNotificationGroupName = "GLEIF_DATA_QUALITY_NOTIFICATION";

        private string _leiOriginator;

        private const string LinkAttribDirect = "DIRECT";
        private const string LinkAttribUltimate = "ULTIMATE";


        private bool _isEqualLevel1 = true;
        private bool _isEqualLevel2 = true;
        private bool _pendingValidationPassed = true;
        private bool _level2NamesWasChanged = false;
        private bool _level1NamesWasChanged = false;
        private bool _checkNumber8Passed = true;

        public ITransactionFactory TransactionFactory { get; }

        public LeiPreCheckService(
            BaseServiceContext context,
            IControlRepository controlRepository,
            ICertificateService certificateService,
            ILeiPreCheckRestService leiPreCheckRestService,
            ILeiPreCheckRepository leiPreCheckRepository,
            IStoredProcedureRepository storedProcedureRepository,
            ILeiPublicPreCheckRepository leiPublicPreCheckRepository,
            ILeiPublicNamesPreCheckRepository leiPublicNamesPreCheckRepository,
            ILeiPublicLevel2PreCheckRepository leiPublicLevel2PreCheckRepository,
            ILeiPublicNamesLevel2PreCheckRepository leiPublicNamesLevel2PreCheckRepository,
            IMapper mapper,
            IEmailSendService emailSendService,
            IEmailGroupRecipientsGetter emailGroupRecipientsGetter,
            IRepository<CmpAnotherNames> cmpAnotherNames,
            ILeiPublicRepository leiPublicRepository,
            ILeiVersionService leiVersionService,
            ITokenizer500 tokenizer,
            IReadOnlyRepository<LeiCodeHistory> leiCodeHistoryRepository,
            ILeiPublicLevel2Repository leiPublicLevel2Repository,
            ICalendarRepository calendarRepository, ITransactionFactory transactionFactory)
        : base(context)
        {
            _controlRepository = controlRepository;
            _certificateService = certificateService;
            _leiPreCheckRestService = leiPreCheckRestService;
            _leiPreCheckRepository = leiPreCheckRepository;
            _storedProcedureRepository = storedProcedureRepository;
            _leiPublicPreCheckRepository = leiPublicPreCheckRepository;
            _leiPublicNamesPreCheckRepository = leiPublicNamesPreCheckRepository;
            _leiPublicLevel2PreCheckRepository = leiPublicLevel2PreCheckRepository;
            _leiPublicNamesLevel2PreCheckRepository = leiPublicNamesLevel2PreCheckRepository;
            _mapper = mapper;

            var clientCertificateSubject = _controlRepository.GetAppParameter("LEI", "ClientCertificateSubject").Varvalue;
            var clientCertificateSn = _controlRepository.GetAppParameter("LEI", "ClientCertificateSN").Varvalue;
            _gleifCertificate = _certificateService.GetCertificate(clientCertificateSubject, clientCertificateSn);

            _emailSendService = emailSendService;
            _emailGroupRecipientsGetter = emailGroupRecipientsGetter;
            _cmpAnotherNames = cmpAnotherNames;
            _leiPublicRepository = leiPublicRepository;
            _leiVersionService = leiVersionService;
            _tokenizer = tokenizer;
            _leiCodeHistoryRepository = leiCodeHistoryRepository;
            _leiOriginator = _leiPublicRepository.GetNdcLeiCode();
            _leiPublicLevel2Repository = leiPublicLevel2Repository;
            _calendarRepository = calendarRepository;
            TransactionFactory = transactionFactory;
        }


        /// <summary>
        /// Отправка запроса PreCheck в GLEIF
        /// </summary>
        /// <param name="leiPreCheckId"></param>
        /// <param name="requestAuthor"></param>
        /// <returns></returns>
        public async Task<IRestResponse> SendPreCheckAsync(int leiPreCheckId, string requestAuthor, bool forNightPorter = false)
        {
            Logger.Info($"Отправка PreCheck для leiPreCheckId {leiPreCheckId}. Сертификат: Subject - {_gleifCertificate.Subject};" +
                $" ExpirationDate - {_gleifCertificate.GetExpirationDateString()}", LogContext);

            var leiPreCheck = await _leiPreCheckRepository.Get().Where(r => r.LeiPreCheckId == leiPreCheckId).FirstOrDefaultAsync();
            if (leiPreCheck == null)
                throw new Exception($"Отсутствует запись в таблице lei_pre_check с id {leiPreCheckId}");

            leiPreCheck.RequestAuthor = requestAuthor;
            leiPreCheck.SendDate = DateTime.Now;
            leiPreCheck.Status = "Отправлен";
            await _leiPreCheckRepository.UpdateAsync(leiPreCheck);

            IRestResponse restResponse = null;

            try
            {
                restResponse = await _leiPreCheckRestService.SendPreCheckAsync(_gleifCertificate, leiPreCheck.LeiXml, leiPreCheck.RrXml, leiPreCheck.RepexXml);

                Logger.Info($"Получен ответ для leiPreCheckId {leiPreCheckId}: статус {restResponse.StatusCode} - {restResponse.StatusDescription}." +
                    $"Контент: {restResponse.Content}; ErrorMessage: {restResponse.ErrorMessage}", LogContext);

                leiPreCheck.ResponseDate = DateTime.Now;
                leiPreCheck.Status = "Получен ответ";
                leiPreCheck.Response = restResponse.Content;
                await _leiPreCheckRepository.UpdateAsync(leiPreCheck);

                if (restResponse.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    var resSuccess = JsonConvert.DeserializeObject<LeiPreCheckResponseSuccess>(restResponse.Content);

                    leiPreCheck.ResponseStatus = "200"; // Так по ФТ

                    if (resSuccess != null)
                    {
                        var hasBusinessErrors = !IsPrecheckResponseSuccessful(resSuccess);
                        if (hasBusinessErrors)
                        {
                            // Отправляется уведомление о наличие бизнес-ошибок
                            await SendReportAboutBuisnessError(leiPreCheck);
                        }

                        if (!hasBusinessErrors || forNightPorter)
                        {
                            leiPreCheck.AutoConfirmation = 1;
                        }
                    }
                }
                else if ((int)restResponse.StatusCode >= 400)
                {
                    try
                    {
                        var resError = JsonConvert.DeserializeObject<LeiPreCheckResponseError>(restResponse.Content);
                        if (resError != null)
                        {
                            if (resError.Errors != null && resError.Errors.Length > 0)
                            {
                                leiPreCheck.ResponseStatus = resError.Errors[0].Status;
                                leiPreCheck.ResponseStatusDesc = resError.Errors[0].Title;
                                leiPreCheck.ResponseStatusDetails = resError.Errors[0].Detail;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        leiPreCheck.ResponseStatus = ((int)restResponse.StatusCode).ToString();
                        throw;
                    }

                    // Если в ответе вернулся «Код статуса о приеме» >= 400, то выполняется уведомление пользователя о технической ошибки
                    await SendReportAboutTechnicalError(leiPreCheck, restResponse.Content);
                }
                else
                {
                    leiPreCheck.ResponseStatus = ((int)restResponse.StatusCode).ToString();
                    leiPreCheck.ResponseStatusDesc = restResponse.ErrorMessage;
                }

                await _leiPreCheckRepository.UpdateAsync(leiPreCheck);
            }
            catch (Exception e)
            {
                Logger.Error($"Исключение при отправке запроса c id {leiPreCheck.LeiPreCheckId}, LEI-код {leiPreCheck.LeiCode}", e, LogContext);

                leiPreCheck.ResponseStatusDesc = e.Message;
                await _leiPreCheckRepository.UpdateAsync(leiPreCheck);

                throw;
            }

            return restResponse;
        }

        public bool IsPrecheckSuccessful(int leiPrecheckId)
        {
            var precheck = _leiPreCheckRepository.Get().Where(r => r.LeiPreCheckId == leiPrecheckId).FirstOrDefault();
            if (precheck == null || precheck.Response.IsNullOrWhitespace())
                return false;

            var resSuccess = JsonConvert.DeserializeObject<LeiPreCheckResponseSuccess>(precheck.Response);
            return IsPrecheckResponseSuccessful(resSuccess);
        }

        private bool IsPrecheckResponseSuccessful(LeiPreCheckResponseSuccess response)
        {
            return response.Data.Attributes.Results.All(r => r.Result.InList("PASS", "NOT_APPLICABLE"));
        }

        private async Task SendReportAboutBuisnessError(LeiPreCheck lpc)
        {
            var recipients = await _emailGroupRecipientsGetter.GetAsync(GleifDataQualityNotificationGroupName);

            var letter = new Letter
            {
                To = recipients,
                From = "developer@nsd.ru",
                Subject = $"Результат проверки pre-check содержит бизнес-ошибки, по отправленному коду {lpc.LeiCode}",
                Body = $"По коду {lpc.LeiCode}, по запросу pre-check получен ответ в котором по результатам проверки обнаружены бизнес-ошибки." +
                       $"{Environment.NewLine}Перейдите в журнал pre-check для проверки полученного результата.",
                IsHtml = false
            };

            _emailSendService.Send(letter);
        }

        private async Task SendReportAboutTechnicalError(LeiPreCheck lpc, string error)
        {
            var recipients = await _emailGroupRecipientsGetter.GetAsync(GleifDataQualityNotificationGroupName);

            var letter = new Letter
            {
                To = recipients,
                From = "developer@nsd.ru",
                Subject = $"Техническая ошибка при выполнения запроса по коду {lpc.LeiCode}",
                Body = $"По коду {lpc.LeiCode}, по запросу pre-check получен ответ с технической ошибкой. {error}",
                IsHtml = false
            };

            _emailSendService.Send(letter);
        }


        /// <summary>
        /// П.1   Проверка наличия расхождений
        /// </summary>
        public bool ProcessChanges(List<int> companyIds = null, string user = null, List<LeiPublicLevel2Changes> level2Changes = null)
        {
            List<LeiPublicChanges> leiPublicChanges = null;
            List<LeiPublicLevel2Changes> leiPublicLevel2Changes = null;
            if (companyIds != null)
            {
                leiPublicChanges = _storedProcedureRepository.LeiPublicGetChanges().Where(x => companyIds.Contains(x.CompanyId)).ToList(); // расхождения по level1
                leiPublicLevel2Changes = level2Changes.IsNullOrEmpty() ? _leiPublicLevel2Repository.LeiPublicLevel2GetChanges(companyIds) :
                    level2Changes.Where(w => companyIds.Contains(w.CompanyId)).ToList(); // расхождения по level2
            }
            else
            {
                leiPublicChanges = _storedProcedureRepository.LeiPublicGetChanges(); // расхождения по level1
                leiPublicLevel2Changes = level2Changes.IsNullOrEmpty() ? _storedProcedureRepository.LeiPublicLevel2GetChanges() : level2Changes; // расхождения по level2
            }

            // Нужно убрать записи, которые нельзя отправлять на проверку в GLEIF
            leiPublicChanges = leiPublicChanges.Where(r => r.IsCheck8Passed.HasValue && r.IsCheck8Passed.Value).ToList();
            leiPublicLevel2Changes = leiPublicLevel2Changes.Where(r => r.IsCheck8Passed.HasValue && r.IsCheck8Passed.Value).ToList();

            var companiesToCheck = leiPublicChanges.Select(s => s.CompanyId)
                .Union(leiPublicLevel2Changes.Select(s => s.CompanyId)).Distinct().ToList();

            if (!companiesToCheck.Any())
                return false;

            return CheckCompanies(companiesToCheck, leiPublicChanges, leiPublicLevel2Changes, null, user);
        }

        /// <summary>
        /// Функция выполняет поиск и обработку расхождений данных между записями, готовыми к обработке и историческими записями в таблицах фиксации PreCheck
        /// </summary>
        /// <param name="companiesToCheck">Список идентификаторов компаний для проверки</param>
        /// <param name="leiPublicChanges">Изменения по Level1</param>
        /// <param name="leiPublicLevel2Changes">Изменения по Level2</param>
        /// <param name="preCheckIds">Список идентификаторов журнала PreCheck API</param>
        /// <param name="user">Пользователь, инициировавший проверку</param>
        /// <param name="forced">Для пометки, что выполняется специальный сценарий. С отличиями, описанными в П1</param>
        /// <param name="isPendingValidStatusEnabled">Включать в проверку записи со статусом документа PENDING_VALIDATION</param>
        /// <returns>true - если расхождения найдены и обработаны, false - иначе</returns>
        public bool CheckCompanies(List<int> companiesToCheck, List<LeiPublicChanges> leiPublicChanges,
            List<LeiPublicLevel2Changes> leiPublicLevel2Changes, List<int> preCheckIds, string user = null, bool forced = false,
            bool isPendingValidStatusEnabled = true)
        {
            bool hasChanges = false;

            List<LeiPublicPreCheck> leiPublicPreChecks;
            var leiPublicNamesPreCheck =
                _mapper.Map<List<LeiPublicNamesPreCheck>>(_leiPublicNamesPreCheckRepository.GetActualLeiPublicNamesPreCheck());

            var leiPublicNamesLevel2PreCheck =
                _mapper.Map<List<LeiPublicNamesLevel2PreCheck>>(_leiPublicNamesLevel2PreCheckRepository.GetActualLeiPublicNamesLevel2PreCheck());

            var leiPublicLevel2 = _leiPublicLevel2Repository.GetLeiPublicLevel2(companiesToCheck);
            var leiPublicLevel1 = _leiPublicRepository.GetLeiPublic(companiesToCheck);

            var cmpAnotherNames = _cmpAnotherNames.Get().ToList();
            var leiPreCheck = _leiPreCheckRepository.GetLeiPreCheck(companiesToCheck);

            try
            {
                leiPublicPreChecks = _leiPublicPreCheckRepository.Get().ToList();
            }
            catch (Exception e)
            {
                Logger.Info($"Произошла ошибка при чтении записи из журнала фиксации PreCheck Level1: {e}", LogContext);
                throw;
            }

            List<LeiPublicLevel2PreCheckModel> publicLevel2PreChecks = null;
            try
            {
                publicLevel2PreChecks = _leiPublicLevel2PreCheckRepository.GetLeiPublicLevel2PreCheck(companiesToCheck);
            }
            catch (Exception e)
            {
                Logger.Info($"Произошла ошибка при чтении записи из журнала фиксации PreCheck Level2: {e}", LogContext);
                throw;
            }

            //companiesToCheck = new List<int>() { 21125 };
            foreach (var cmpId in companiesToCheck)
            {
                Logger.Info($"Обработка компании companyId = {cmpId}", LogContext);

                var level1Changes = leiPublicChanges.Where(r => r.CompanyId == cmpId).OrderBy(r => r.LeiCodeStatus).FirstOrDefault();
                var level2Changes = leiPublicLevel2Changes.Where(w => w.CompanyId == cmpId).ToList();

                _isEqualLevel1 = _isEqualLevel2 = _pendingValidationPassed = _checkNumber8Passed = true;
                _level1NamesWasChanged = _level2NamesWasChanged = false;

                // Проверка по Level 1
                var level1Result = CheckLevel1(cmpId, level1Changes, leiPublicLevel1, leiPublicPreChecks, leiPublicNamesPreCheck,
                    cmpAnotherNames, leiPreCheck, isPendingValidStatusEnabled);

                // чтобы не делать проверку по Level2, если по Level1 не прошла
                if (!_checkNumber8Passed)
                {
                    Logger.Info($"По Level 1 не пройдена проверка №8. Фиксация не будет проведена.", LogContext);
                    continue;
                }

                // Проверка по Direct Level 2
                var level2DirectResult = CheckLevel2(cmpId, level2Changes, publicLevel2PreChecks,
                    leiPublicNamesLevel2PreCheck, cmpAnotherNames, leiPublicLevel2, leiPreCheck, LinkAttribDirect);

                // чтобы не делать лишнюю проверку по Level2, если предыдущая не прошла
                if (!_checkNumber8Passed)
                {
                    Logger.Info($"По Level 2 Direct не пройдена проверка №8. Фиксация не будет проведена.", LogContext);
                    continue;
                }

                // Проверка по Ultimate Level 2
                var level2UltimateResult = CheckLevel2(cmpId, level2Changes, publicLevel2PreChecks,
                    leiPublicNamesLevel2PreCheck, cmpAnotherNames, leiPublicLevel2, leiPreCheck, LinkAttribUltimate);

                if (!_checkNumber8Passed)
                {
                    Logger.Info($"По Level 2 Ultimate не пройдена проверка №8. Фиксация не будет проведена.", LogContext);
                    continue;
                }

                if (!_pendingValidationPassed)
                {
                    Logger.Info($"Сработало исключение для «PENDING_VALIDATION. Фиксация не требуется", LogContext);
                    continue;
                }

                if (level1Result.IsNullOrWhitespace() && level2DirectResult.IsNullOrEmpty() && level2UltimateResult.IsNullOrEmpty())
                {
                    Logger.Info($"Данных для фиксации не найдено", LogContext);
                    continue;
                }

                if (_isEqualLevel1 && _isEqualLevel2)
                {
                    Logger.Info($"Расхождений не найдено. Фиксация не требуется", LogContext);
                    continue;
                }

                var leiCode = level1Result?.LeiCode
                              ?? level2DirectResult.FirstOrDefault()?.ChildLeiCode
                              ?? level2UltimateResult.FirstOrDefault()?.ChildLeiCode;

                //Получаем запись для журнала
                var leiPreCheckRecord = GetRecordForLeiPreCheck(cmpId, leiCode, user);
                if (forced)
                {
                    leiPreCheckRecord.RequestInitiator = user;
                    leiPreCheckRecord.SendPermissionType = 1;
                    leiPreCheckRecord.SendPermissionDate = DateTime.Now;
                    leiPreCheckRecord.SendPermissionInitiator = user;
                }

                if (!leiPreCheckRecord.IsNullOrWhitespace())
                {
                    _leiPreCheckRepository.Insert(leiPreCheckRecord);

                    preCheckIds?.Add(leiPreCheckRecord.LeiPreCheckId);
                    Logger.Info($"Добавлена запись LeiPreCheckId={leiPreCheckRecord.LeiPreCheckId} в журнал фиксации", LogContext);

                    hasChanges = true;
                }
                else
                {
                    Logger.Info($"Не удалось получить запись для журнала PreCheck. " +
                                $"Идентификатор записи в таблице фиксации не будет заполнен", LogContext);
                    continue;
                }

                // Фиксация по Level 1
                Logger.Info($"Фиксация данных в таблице фиксации PreCheck по Level1", LogContext);
                if (level1Result.IsNullOrWhitespace())
                    Logger.Info($"Данные для фиксации по Level1 отсутствуют", LogContext);
                else
                {
                    level1Result.LeiPreCheckId = leiPreCheckRecord.LeiPreCheckId;
                    _leiPublicPreCheckRepository.Insert(level1Result);
                    Logger.Info($"Добавлена запись LeiCodeId = {level1Result.LeiCodeId}", LogContext);
                }

                Logger.Info($"Фиксация данных данных о наименованиях организаций по Level1 ", LogContext);
                var level1Names = LeiPublicNamesPreCheckFixation(cmpId, leiPublicNamesPreCheck, cmpAnotherNames, _level1NamesWasChanged, leiPreCheckRecord.LeiPreCheckId);

                // Фиксация по Level 2
                var level2Results = new List<LeiPublicLevel2PreCheck>();

                if (!level2DirectResult.IsNullOrWhitespace())
                    level2Results.AddRange(level2DirectResult);
                if (!level2UltimateResult.IsNullOrWhitespace())
                    level2Results.AddRange(level2UltimateResult);

                Logger.Info($"Фиксация данных ({level2Results.Count} шт.) в таблице фиксации PreCheck по Level2", LogContext);

                level2Results.ForEach(x => x.InsertDt = DateTime.Now);
                level2Results.ForEach(x => x.InsertUser = Environment.UserDomainName + @"\" + Environment.UserName);
                level2Results.ForEach(x => x.LeiPreCheckId = leiPreCheckRecord.LeiPreCheckId);
                level2Results.ForEach(x => x.LeiCodeId = 0);

                using (var transaction = TransactionFactory.Begin())
                {
                    try
                    {
                        if (level2DirectResult.IsNullOrEmpty())
                            Logger.Info($"Данные для фиксации по Level2 Direct отсутствуют", LogContext);
                        else
                            _leiPublicLevel2PreCheckRepository.BulkInsert(level2Results,
                                new BulkConfig { SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.FireTriggers });
                    }
                    catch (Exception e)
                    {
                        Logger.Info($"Ошибка при фиксации данных с помощью BulkInsert. Попытка зафиксировать записи по очереди." +
                                    $"{e}", LogContext);
                        level2Results.ForEach(x => _leiPublicLevel2PreCheckRepository.Insert(x));
                    }

                    transaction.Commit();
                }
                Logger.Info($"Добавлено записей: {level2Results.Count} шт.", LogContext);


                Logger.Info($"Фиксация данных о наименованиях организаций по Level2 для Parent", LogContext);
                int? parentCompanyId = level2Changes.FirstOrDefault(x => x.ParentCompanyId != null && !x.ParentPniCode.IsNullOrWhitespace())?.ParentCompanyId;

                if (parentCompanyId != null)
                {
                    Logger.Info($"ParentCompanyId={parentCompanyId}", LogContext);
                    var level2Names = LeiPublicNamesPreCheckFixation((int)parentCompanyId, cmpAnotherNames,
                        leiPublicNamesLevel2PreCheck, _level2NamesWasChanged, leiPreCheckRecord.LeiPreCheckId);
                }

                Logger.Info($"Формирование XML ", LogContext);
                var leiXml = GetLeiXml(cmpId, leiPreCheckRecord.LeiPreCheckId);
                var rrXml = GetRrXml(cmpId, leiPreCheckRecord.LeiPreCheckId);
                var repexXml = GetRepexXml(cmpId, leiPreCheckRecord.LeiPreCheckId);

                leiPreCheckRecord.LeiXml = leiXml.IsNullOrWhitespace() ? null : leiXml.ToString();
                leiPreCheckRecord.RrXml = rrXml.IsNullOrWhitespace() ? null : rrXml.ToString();
                leiPreCheckRecord.RepexXml = repexXml.IsNullOrWhitespace() ? null : repexXml.ToString();

                Logger.Info($"Обновление XML ", LogContext);
                _leiPreCheckRepository.Update(leiPreCheckRecord);

                Logger.Info($"Обработка компании companyId={cmpId} завершена", LogContext);
            }

            return hasChanges;
        }


        /// <summary>
        /// Фиксация в таблицы фиксации PreCheck данных о наименованиях организаций на других языках
        /// </summary>
        public List<LeiPublicNamesPreCheck> LeiPublicNamesPreCheckFixation(int companyId,
            List<LeiPublicNamesPreCheck> leiPublicNames, List<CmpAnotherNames> cmpAnotherNames,
            bool level1NamesWasChanged, int leiPreCheckId)
        {
            if (!level1NamesWasChanged) return null;
            List<LeiPublicNamesPreCheck> names = new List<LeiPublicNamesPreCheck>();

            cmpAnotherNames = cmpAnotherNames.Where(w => w.CompanyId == companyId).ToList();

            if (cmpAnotherNames.IsNullOrEmpty())
            {
                Logger.Info($"Для организации company_id = {companyId} наименований на других языках не найдено", LogContext);
                return null;
            }

            leiPublicNames = leiPublicNames.Where(w => w.CompanyId == companyId).ToList();

            foreach (var an in cmpAnotherNames)
            {
                if (!leiPublicNames.Any(lpn => lpn.NameId == an.NameId
                                               && lpn.IsArchived == an.IsArchived
                                               && lpn.DeletedDt == an.DeletedDt
                                               && lpn.IsOfficial == an.IsOfficial
                                               && lpn.IsByMistake == an.IsByMistake
                                               && lpn.LanguageCode == an.LanguageCode
                                               && lpn.CmpName == an.CmpName
                                               && lpn.CmpNameShort == an.CmpNameShort))
                {
                    var leiPublicNameNewItem = _mapper.Map<LeiPublicNamesPreCheck>(an);
                    leiPublicNameNewItem.LeiPreCheckId = leiPreCheckId;
                    leiPublicNameNewItem.InsertDt = DateTime.Now;
                    leiPublicNameNewItem.InsertUser = Environment.UserDomainName + @"\" + Environment.UserName;
                    leiPublicNameNewItem.RecId = 0;
                    names.Add(leiPublicNameNewItem);
                    Logger.Info($"Зафиксирована запись CmpName={an.CmpName}, CmpNameShort={an.CmpNameShort}, " +
                                $"IsByMistake={an.IsByMistake}," +
                        $" LanguageCode={an.LanguageCode}, IsOfficial={an.IsOfficial}, " +
                                $"DeletedDt={an.DeletedDt}, IsArchived={an.IsArchived}", LogContext);
                }
            }

            if (!names.IsNullOrEmpty())
            {
                using (var transaction = TransactionFactory.Begin())
                {
                    _leiPublicNamesPreCheckRepository.BulkInsert(names,
                                new BulkConfig { SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.FireTriggers });

                    transaction.Commit();
                }


            }

            return names;
        }

        /// <summary>
        /// Фиксация в таблицы фиксации PreCheck данных о наименованиях организаций на других языках
        /// </summary>
        public List<LeiPublicNamesLevel2PreCheck> LeiPublicNamesPreCheckFixation(int companyId, List<CmpAnotherNames> cmpAnotherNames,
            List<LeiPublicNamesLevel2PreCheck> leiPublicNames, bool level2NamesWasChanged, int leiPreCheckId)
        {
            if (!level2NamesWasChanged) return null;

            List<LeiPublicNamesLevel2PreCheck> names = new List<LeiPublicNamesLevel2PreCheck>();

            cmpAnotherNames = cmpAnotherNames.Where(w => w.CompanyId == companyId).ToList();

            if (cmpAnotherNames.IsNullOrEmpty())
            {
                Logger.Info($"Для организации company_id = {companyId} наименований на других языках не найдено", LogContext);
                return null;
            }

            leiPublicNames = leiPublicNames.Where(w => w.CompanyId == companyId).ToList();

            foreach (var an in cmpAnotherNames)
            {
                if (!leiPublicNames.Any(lpn => lpn.NameId == an.NameId
                                               && lpn.IsArchived == an.IsArchived
                                               && lpn.DeleteDt == an.DeletedDt
                                               && lpn.IsOfficial == an.IsOfficial
                                               && lpn.LanguageCode == an.LanguageCode
                                               && lpn.CmpName == an.CmpName))
                {
                    var leiPublicNameNewItem = _mapper.Map<LeiPublicNamesLevel2PreCheck>(an);
                    leiPublicNameNewItem.LeiPreCheckId = leiPreCheckId;
                    leiPublicNameNewItem.LeiPreCheckId = leiPreCheckId;
                    leiPublicNameNewItem.InsertDt = DateTime.Now;
                    leiPublicNameNewItem.InsertUser = Environment.UserDomainName + @"\" + Environment.UserName;
                    leiPublicNameNewItem.RecId = 0;

                    names.Add(leiPublicNameNewItem);
                    Logger.Info($"Зафиксирована запись CmpName={an.CmpName}, LanguageCode={an.LanguageCode}, " +
                                $"IsOfficial={an.IsOfficial}, DeletedDt={an.DeletedDt}," +
                        $" IsArchived={an.IsArchived}", LogContext);
                }
            }

            if (!names.IsNullOrEmpty())
            {
                using (var transaction = TransactionFactory.Begin())
                {
                    _leiPublicNamesLevel2PreCheckRepository.BulkInsert(names,
                        new BulkConfig { SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.FireTriggers });

                    transaction.Commit();
                }


            }

            return names;
        }

        /// <summary>
        /// Проверка данных по Level2
        /// </summary>
        /// <param name="companyId">ID компании</param>
        /// <param name="level2ChangesAll">Список изменений по Level2</param>
        /// <param name="leiPublicLevel2PreChecks">Записи таблицы фиксации PreCheck</param>
        /// <param name="leiPublicLevel2All">Данные из стандартной таблицы фиксации</param>
        /// <param name="leiPreCheck">Данные из журнала PreCheck</param>
        /// <param name="linkAttrib">Тип связи (DIRECT/ULTIMATE)</param>
        /// <param name="leiPublicNamesPreCheck">Данные из стандартной таблицы фиксации наименовий на других языках</param>
        /// <param name="cmpAnotherNames">Данные из стандартной таблицы наименовий на других языках</param>
        /// <returns></returns>
        private List<LeiPublicLevel2PreCheck> CheckLevel2(int companyId, List<LeiPublicLevel2Changes> level2ChangesAll,
            List<LeiPublicLevel2PreCheckModel> leiPublicLevel2PreChecks,
            List<LeiPublicNamesLevel2PreCheck> leiPublicNamesPreCheck,
            List<CmpAnotherNames> cmpAnotherNames,
            List<LeiPublicLevel2Model> leiPublicLevel2All,
            List<LeiPreCheckModel> leiPreCheck,
            string linkAttrib)
        {
            Logger.Info($"Подготовка данных по Level2 для companyId = {companyId}, linkAttib = {linkAttrib}", LogContext);


            List<LeiPublicLevel2PreCheck> leiPublicLevel2PreCheckLastRecords;
            bool isEquals = true;

            var leiPreCheckLastRecord = leiPreCheck.Where(w => w.CompanyId == companyId)
                .OrderByDescending(o => o.LeiPreCheckId).FirstOrDefault();

            if (leiPreCheckLastRecord != null)
                Logger.Info($"Идентификатор последней записи в журнале PreCheck = {leiPreCheckLastRecord.LeiPreCheckId}", LogContext);
            else Logger.Info($"Идентификатор последней записи в журнале не найден", LogContext);

            //Отбираем изменения по текущему linkAttrib
            var level2ChangesCurrentLinkAttrib = level2ChangesAll
                .Where(w => w.CompanyId == companyId && w.LinkAttrib == linkAttrib
                ).ToList();

            //Отбираем данные из стандартной таблицы по текущему linkAttrib
            var leiPublicLevel2 = leiPublicLevel2All
                .Where(w => w.CompanyId == companyId && w.LinkAttrib == linkAttrib)
                .ToList();

            Logger.Info($"Найдено данных в стандартной таблице фиксации: {leiPublicLevel2.Count}", LogContext);
            Logger.Info($"Найдено изменений: {level2ChangesCurrentLinkAttrib.Count}", LogContext);

            // Объединяем данные из lei_public_level2 с текущими изменениями в один набор
            var level2NewData = _mapper.Map<List<LeiPublicLevel2PreCheck>>(leiPublicLevel2).Union(_mapper.Map<List<LeiPublicLevel2PreCheck>>(level2ChangesCurrentLinkAttrib)).ToList();

            if (level2NewData.IsNullOrEmpty())
            {
                Logger.Info($"Данных для фиксации нету. Выход из проверки по текущему linkattrib", LogContext);
                return null;
            }

            if (level2ChangesCurrentLinkAttrib.Any(a => a.HaveError)
                || level2ChangesCurrentLinkAttrib.Any(a => a.UploadStop)
                || leiPublicLevel2.Any(a => a.UploadStop))
            {
                Logger.Info($"Проверка 8 не пройдена. Дальнейшая проверка пропускается", LogContext);
                _checkNumber8Passed = false;
                return level2NewData;
            }

            //Проверка последней отправки
            if (!IsLastPreCheckStatusCheckPassed(leiPreCheckLastRecord))
            {
                return level2NewData;
            }

            if (level2ChangesCurrentLinkAttrib.IsNullOrEmpty())
                Logger.Info($"Изменений нету. Для фиксации будут использованы текущие значения", LogContext);


            //Проверка наименований на других языках
            if (level2ChangesCurrentLinkAttrib.
                Any(x => !x.ParentPniCode.IsNullOrWhitespace()))
            {
                int? parentCompanyId = level2ChangesCurrentLinkAttrib.FirstOrDefault(w => w.ParentCompanyId != null)?.ParentCompanyId;

                if (parentCompanyId != null)
                    if (!PublicNamesEqual((int)parentCompanyId, leiPublicNamesPreCheck, cmpAnotherNames))
                    {
                        _isEqualLevel2 = false;
                        _level2NamesWasChanged = true;
                        return level2NewData;
                    }
            }

            //Данные по последней записи из таблицы фиксации PreCheck
            try
            {
                leiPublicLevel2PreCheckLastRecords = _mapper.Map<List<LeiPublicLevel2PreCheck>>(leiPublicLevel2PreChecks
                    .Where(w => w.LeiPreCheckId == leiPreCheckLastRecord?.LeiPreCheckId
                                && w.LinkAttrib == linkAttrib)).ToList();
            }
            catch (Exception e)
            {
                Logger.Info($"Произошла ошибка при чтении записи из журнала фиксации PreCheck для записи с {companyId}, " +
                            $"{e}", LogContext);
                throw;
            }


            //Если в журнале фиксации ранее не участвовал, то фиксируем
            if (leiPublicLevel2PreCheckLastRecords.IsNullOrEmpty())
            {
                Logger.Info($"Запись в таблице фиксации PreCheck не найдена. Фиксация текущей записи.", LogContext);
                _isEqualLevel2 = false;
                return level2NewData;
            }

            //Разное количество = расхождение
            if (level2NewData.Count != leiPublicLevel2PreCheckLastRecords.Count)
            {
                Logger.Info($"Обнаружено различие в количестве данных. " +
                            $"Ранее зафиксировано записей {leiPublicLevel2PreCheckLastRecords.Count}. " +
                            $"Текущее количество записей для фиксации {level2NewData.Count}", LogContext);
                _isEqualLevel2 = false;
                return level2NewData;
            }

            //Ищем среди зафиксирвоанных данных все данные для фиксации
            foreach (var level2 in level2NewData)
            {
                if (leiPublicLevel2PreCheckLastRecords.Any(a =>
                    PublicInstancePropertiesEqual(a, level2, "LeiCodeId",
                        "LeiPreCheckId",
                        "InsertDt", "InsertUser", "ChildLeiUpdateDate",
                        "LastUpdateDate", "PniLastUpdateDate", "EventDate", "RelationshipPeriodEndDate")))
                    isEquals = true;
                else
                {
                    Logger.Info($"Обнаружено различие данных. Фиксация записи", LogContext);
                    isEquals = false;
                    break;
                }
            }

            if (!isEquals)
            {
                Logger.Info($"Запись CompanyId={companyId}, ChildLeiCode={level2NewData.FirstOrDefault()?.ChildLeiCode} добавлена в очередь на отправку в PreCheck", LogContext);
                _isEqualLevel2 = false;
                return level2NewData;
            }
            Logger.Info($"Обработка записи с company_id {companyId} по Level 2 завершена", LogContext);

            return level2NewData;
        }

        /// <summary>
        /// Проверка по Level1
        /// </summary>
        /// <param name="companyId">ID компании</param>
        /// <param name="level1Change">Набор данных для проверки</param>
        /// <param name="leiPreCheck">Данные из журнала PreCheck</param>
        /// <param name="isPendingValidStatusEnabled">Включать в проверку записи со статусом документа PENDING_VALIDATION</param>
        /// <param name="leiPublicAll">Таблица фиксации lei_public</param>
        /// <param name="leiPublicPreChecks">Записи таблицы фиксаии PreCheck</param>
        /// <param name="leiPublicNamesPreCheck">Наименования на других языках по Level 1 PreCheck</param>
        /// <param name="cmpAnotherNames">Наименования на других языках</param>
        /// <returns></returns>
        private LeiPublicPreCheck CheckLevel1(int companyId,
            LeiPublicChanges level1Change,
            List<LeiPublicModel> leiPublicAll,
            List<LeiPublicPreCheck> leiPublicPreChecks,
            List<LeiPublicNamesPreCheck> leiPublicNamesPreCheck,
            List<CmpAnotherNames> cmpAnotherNames,
            List<LeiPreCheckModel> leiPreCheck,
            bool isPendingValidStatusEnabled = true)
        {
            Logger.Info($"Подготовка данных по Level1 для company_id = {companyId}", LogContext);
            LeiPublicPreCheck leiPublicPreCheckLastRecord;
            bool isEquals = true;

            var leiPreCheckLastRecord = leiPreCheck.Where(w => w.CompanyId == companyId)
                .OrderByDescending(o => o.LeiPreCheckId).FirstOrDefault();

            if (leiPreCheckLastRecord != null)
                Logger.Info($"Идентификатор последней записи в журнале PreCheck " +
                            $"= {leiPreCheckLastRecord.LeiPreCheckId}", LogContext);
            else Logger.Info($"Идентификатор последней записи в журнале не найден", LogContext);


            //Отбираем текущие изменения 
            var level1Changes = level1Change;

            var leiPublic = leiPublicAll.Where(x => x.CompanyId == companyId)
                .OrderByDescending(x => x.LeiCodeId).FirstOrDefault();

            // Данные для фиксации
            LeiPublicPreCheck level1NewData;
            if (level1Changes.IsNullOrWhitespace())
            {
                Logger.Info($"Изменений нет. Поиск данных в стандартной таблице фиксации", LogContext);
                level1NewData = _mapper.Map<LeiPublicPreCheck>(leiPublic);
            }
            else
            {
                Logger.Info($"Изменения найдены. Произойдет фиксация данных из ЗГкО Level1", LogContext);
                level1NewData = _mapper.Map<LeiPublicPreCheck>(level1Changes);
            }

            if (level1NewData.IsNullOrWhitespace())
            {
                Logger.Info($"Данных для фиксации не найдено", LogContext);
                return null;
            }

            if ((level1Changes != null && (level1Changes.UploadStop || level1Changes.HaveError))
                || (leiPublic != null && leiPublic.UploadStop))
            {
                Logger.Info($"Проверка 8 не пройдена. Дальнейшая проверка пропускается", LogContext);
                _checkNumber8Passed = false;
                return level1NewData;
            }

            if (!level1NewData.IsNullOrWhitespace())
            {
                level1NewData.LeiUpdateDate = DateTime.Now;
                level1NewData.InsertDt = DateTime.Now;
                level1NewData.InsertUser = Environment.UserDomainName + @"\" + Environment.UserName;
            }
            else
                Logger.Info($"Изменений нету. Для фиксации будут использованы текущие значения", LogContext);


            //Проверка последней отправки
            if (!IsLastPreCheckStatusCheckPassed(leiPreCheckLastRecord)) return level1NewData;


            //Проверка наименований на других языках
            if (!PublicNamesEqual(companyId, leiPublicNamesPreCheck, cmpAnotherNames))
            {
                _isEqualLevel1 = false;
                _level1NamesWasChanged = true;
                return level1NewData;
            }

            //Данные по последней записи из таблицы фиксации PreCheck
            try
            {
                leiPublicPreCheckLastRecord = _mapper.Map<LeiPublicPreCheck>(leiPublicPreChecks
                    .Where(w => w.LeiPreCheckId == leiPreCheckLastRecord?.LeiPreCheckId).FirstOrDefault());
            }
            catch (Exception e)
            {
                Logger.Info($"Произошла ошибка при чтении записи из журнала фиксации PreCheck для записи с {companyId}, " +
                            $"{e}", LogContext);
                throw;
            }

            //«Исключение для «PENDING_VALIDATION»
            //До момента появления строки фиксации в таблице фиксации PreСheck(аналоге Lei_Public) по данному коду
            //проверка наличия расхождений по нему не делается. 
            if (isPendingValidStatusEnabled)
                if (level1Change?.RegistrationStatus == "PENDING_VALIDATION"
                    && leiPublicPreChecks.All(w => w.CompanyId != companyId))
                {
                    Logger.Info($"Получен статус PENDING_VALIDATION. Запись в таблице фиксации PreCheck не найдена. Проверка пропускается.", LogContext);
                    _pendingValidationPassed = false;
                    return level1NewData;
                }

            //Если в журнале фиксации ранее не участвовал, то фиксируем
            if (leiPublicPreCheckLastRecord.IsNullOrWhitespace())
            {
                Logger.Info($"Запись в таблице фиксации PreCheck не найдена. Фиксация текущей записи.", LogContext);
                _isEqualLevel1 = false;
                return level1NewData;
            }


            if (PublicInstancePropertiesEqual(leiPublicPreCheckLastRecord, level1NewData, "LeiCodeId",
                    "LeiPreCheckId",
                    "InsertDt", "InsertUser", "ChildLeiUpdateDate",
                    "LastUpdateDate", "PniLastUpdateDate", "LeiUpdateDate",
                    level1Change?.RegistrationStatus == "PENDING_VALIDATION" ? "LeiVerificationDate" : null,
                    level1Change?.RegistrationStatus == "PENDING_VALIDATION" ? "LeiNextRecertificationDate" : null))
                isEquals = true;
            else
            {
                isEquals = false;
            }


            if (!isEquals)
            {
                Logger.Info($"Запись CompanyId={companyId}, ChildLeiCode={level1NewData?.LeiCode} добавлена в очередь на отправку в PreCheck", LogContext);

                _isEqualLevel1 = false;
                return level1NewData;
            }
            Logger.Info($"Обработка записи с company_id {companyId} по Level 2 завершена", LogContext);

            return level1NewData;
        }

        public List<LeiPreCheckModel> GetLeiPreCheckRecordsForGleif()
        {
            return _leiPreCheckRepository.GetLeiPreCheckRecordsForGleif();
        }

        /// <summary>
        /// П.2 Отправка данных в GLEIF на PreCheck API проверку
        /// </summary>
        public void SendLeiToPreCheckProcess(bool forNightPorter = false, string requestAuthor = null)
        {
            Logger.Info($"Отправка данных в GLEIF на PreCheck API проверку", LogContext);

            var recordsForSend = _leiPreCheckRepository.GetLeiPreCheckRecordsForGleif();

            if (recordsForSend.IsNullOrWhitespace())
            {
                Logger.Info($"Данных, удовлетворяющих условиям, не найдено", LogContext);
                return;
            }

            SendLeiToPreCheck(_mapper.Map<List<LeiPreCheck>>(recordsForSend), forNightPorter: forNightPorter, requestAuthor: requestAuthor);
            Logger.Info($"Отправка данных в GLEIF на PreCheck API проверку завершена", LogContext);
        }

        public void SendLeiToPreCheck(List<LeiPreCheck> recordsForSend, bool forNightPorter = false, string requestAuthor = null)
        {
            foreach (var rec in recordsForSend)
            {
                Logger.Info($"Отправка данных по LeiPreCheckId = {rec.LeiPreCheckId}, CompanyId={rec.CompanyId}", LogContext);
                requestAuthor = requestAuthor.IsNullOrWhitespace() ? "LeiPreCheckJob" : requestAuthor;
                var result = SendPreCheckAsync(rec.LeiPreCheckId, requestAuthor, forNightPorter: forNightPorter);
                result.Wait();
                Logger.Info($"Отправка данных по LeiPreCheckId = {rec.LeiPreCheckId} выполнена", LogContext);
            }

        }
        /// <summary>
        /// П.3 Фиксация данных в «стандартных» таблицах для возможности последующего формирования и отправки в GLEIF XML файлов
        /// </summary>
        public void LeiPublicDataFixationProcess()
        {
            Logger.Info($"Фиксация данных в «стандартных» таблицах", LogContext);

            var leiPublicChanges = _storedProcedureRepository.LeiPublicGetChanges()
                .Where(w => w.AutoAcceptReceived == true || w.AcceptAppliedByUser == true);
            var leiPublicLevel2Changes = _storedProcedureRepository.LeiPublicLevel2GetChanges()
                .Where(w => w.AutoAcceptReceived == true || w.AcceptAppliedByUser == true);

            var companiesToFixationList = leiPublicChanges.Select(s => s.CompanyId)
                .Union(leiPublicLevel2Changes.Select(s => s.CompanyId)).Distinct().ToList();

            // Для дочерних организаций нужна также инфомация по родительким организациям для корректной работы state'ов
            leiPublicLevel2Changes.ForEach(r =>
            {
                if (r.ParentCompanyId.HasValue && !companiesToFixationList.Contains(r.ParentCompanyId.Value))
                    companiesToFixationList.Add(r.ParentCompanyId.Value);
            });

            Logger.Info($"Список организация для фиксации: " +
                        $"{string.Join(", ", companiesToFixationList.ToArray())}", LogContext);

            if (companiesToFixationList.IsNullOrWhitespace()) return;

            LeiPublicDataFixation(companiesToFixationList);

            Logger.Info($"Фиксация данных в «стандартных» таблицах завершена", LogContext);
        }

        public void LeiPublicDataFixation(List<int> companiesToFixationList)
        {
            Logger.Info($"Фиксация данных в «стандартных» таблицах по Level1", LogContext);
            LeiPublicUpdate(companiesToFixationList);

            Logger.Info($"Фиксация данных в «стандартных» таблицах по Level2", LogContext);
            LeiPublicLevel2Update(companiesToFixationList);
        }

        /// <summary>
        /// Фиксация данных в «стандартных» таблицах Level1
        /// </summary>
        /// <param name="companyIds">Список идентификаторов компаний</param>
        public void LeiPublicUpdate(List<int> companyIds)
        {
            _leiPublicRepository.LeiPublicUpdate(companyIds);
        }

        /// <summary>
        /// Фиксация данных в «стандартных» таблицах по Level2
        /// </summary>
        /// <param name="companyIds">Список идентификаторов компаний</param>
        public void LeiPublicLevel2Update(List<int> companyIds)
        {
            _leiPublicLevel2Repository.LeiPublicLevel2Update(companyIds);
        }

        private LeiPreCheck GetRecordForLeiPreCheck(int companyId, string leiCode = null, string requestInitiator = null)
        {
            return new LeiPreCheck
            {
                LeiCode = leiCode ?? String.Empty,
                CompanyId = companyId,
                RequestInitiator = requestInitiator.IsNullOrWhitespace() ? "LeiPreCheckJob" : requestInitiator,
                LeiXml = null,
                RrXml = null,
                RepexXml = null,
                SendPermissionType = 0,
                ManualConfirmation = 0,
                AutoConfirmation = 0
            };
        }

        private XDocument GetRrXml(int companyId, int leiPreCheckId, string linkAttrib = null)
        {
            Logger.Info($"Формирование rr_xml.", LogContext);

            // Данные по Level2 по Direct Parent или Ultimate Parent представлены организацией имеющей LEI код. 
            var leiVersion = LeiVersion.OneLevel2;
            var leiXmlFileType = LeiXmlFileType.Public;

            var topDataList = GetLeiPublicLevel2PreCheckTopData(leiXmlFileType, leiVersion, companyId, leiPreCheckId, linkAttrib);

            if (topDataList.IsNullOrEmpty())
            {
                Logger.Info($"Данные для rr_xml не найдены", LogContext);
                return null;
            }

            var xdoc = _leiVersionService.WithXmlService(leiVersion, leiXmlFileType, s => s.BuildDocument(topDataList, leiXmlFileType, null, _leiOriginator));

            try
            {
                ValidateXmlFile(leiVersion, leiXmlFileType, xdoc);
            }
            catch (Exception e)
            {
                Logger.Info($"{e.Message}", LogContext);
                return null;
            }

            return xdoc;
        }

        private XDocument GetRepexXml(int companyId, int leiPreCheckId, string linkAttrib = null)
        {
            Logger.Info($"Формирование repex_xml.", LogContext);

            // Данные по Level2 по Direct Parent или Ultimate Parent представлены причиной отсутствия связи, т.е. заполнен Repex или имеющей только PNI код 
            var leiVersion = LeiVersion.OneLevel2;
            var leiXmlFileType = LeiXmlFileType.RepEx;

            var topDataList = GetLeiPublicLevel2PreCheckTopData(leiXmlFileType, leiVersion, companyId, leiPreCheckId, linkAttrib);

            if (topDataList.IsNullOrEmpty())
            {
                Logger.Info($"Данные для repex_xml не найдены", LogContext);
                return null;
            }

            var xdoc = _leiVersionService.WithXmlService(leiVersion, leiXmlFileType, s => s.BuildDocument(topDataList, leiXmlFileType, null, _leiOriginator));

            try
            {
                ValidateXmlFile(leiVersion, leiXmlFileType, xdoc);
            }
            catch (Exception e)
            {
                Logger.Info($"{e.Message}", LogContext);
                return null;
            }

            return xdoc;
        }

        private List<LeiPublicTop> GetLeiPublicPreCheckTopData(LeiXmlFileType leiXmlFileType, LeiVersion leiVersion, int companyId, int leiPreCheckId)
        {
            var topDataList = _leiPublicRepository.GetLeiPublicPreCheckTopData(leiXmlFileType, leiVersion, companyId, leiPreCheckId)
                .Where(tp => tp.CompanyId == companyId).ToList();

            return topDataList;
        }


        private List<LeiPublicTop> GetLeiPublicLevel2PreCheckTopData(LeiXmlFileType leiXmlFileType, LeiVersion leiVersion, int companyId, int leiPreCheckId, string linkAttrib = null)
        {
            var topDataList = _leiPublicLevel2Repository.GetLeiPublicPreCheckTopData(leiXmlFileType, linkAttrib, companyId,
                leiPreCheckId).Where(tp => tp.CompanyId == companyId).ToList();

            return topDataList;
        }

        /// <summary>
        /// Возвращает XML по Level1 для отправки в Gleif
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        private XDocument GetLeiXml(int companyId, int leiPreCheckId)
        {
            Logger.Info($"Формирование lei_xml.", LogContext);

            var leiVersion = LeiVersion.Two;
            var leiXmlFileType = LeiXmlFileType.Public;

            var topDataList = GetLeiPublicPreCheckTopData(leiXmlFileType, leiVersion, companyId, leiPreCheckId);
            if (topDataList.IsNullOrEmpty())
            {
                Logger.Info($"Данные для lei_xml не найдены", LogContext);
                return null;
            }


            var xdoc = _leiVersionService.WithXmlService(leiVersion, leiXmlFileType, s => s.BuildDocument(topDataList, leiXmlFileType, null, _leiOriginator));

            try
            {
                ValidateXmlFile(leiVersion, leiXmlFileType, xdoc);
            }
            catch (Exception e)
            {
                Logger.Info($"{e.Message}", LogContext);
                return null;
            }

            Logger.Info($"Формирование lei_xml завершено", LogContext);

            return xdoc;
        }


        /// <summary>
        /// Валидировать XML-файл с выгрузкой кодов LEI
        /// </summary>
        /// <param name="leiVersion"></param>
        /// <param name="leiXmlFileType"></param>
        /// <param name="xdoc"></param>
        protected void ValidateXmlFile(LeiVersion leiVersion, LeiXmlFileType leiXmlFileType, XDocument xdoc)
        {
            XDocument rootXsdScheme = CommonResources.Xml();
            XDocument xsdScheme = _leiVersionService.GetXsdScheme(leiVersion, leiXmlFileType);

            IEnumerable<Exception> validationResult = XmlUtils.ValidateXml(xdoc, new[] { rootXsdScheme, xsdScheme });
            if (!validationResult.Any())
                return;

            var errorsFlat = string.Join(";", validationResult.Select(ex => ex.Message).Distinct());
            var message = "XML не прошел валидацию. Ошибки: {0}".FormatWith(errorsFlat);

            throw new Exception(message);
        }

        /// <summary>
        /// Функция проверяет результат последнего запроса PreCheck API
        /// </summary>
        /// <returns></returns>
        private bool IsLastPreCheckStatusCheckPassed(LeiPreCheckModel lastPreCheckRecord)
        {
            Logger.Info($"Обработка результатов запроса по последней записи в журнале PreCheck", LogContext);
            if (lastPreCheckRecord != null && (!lastPreCheckRecord.ResponseStatus.IsNullOrWhitespace() &&
                lastPreCheckRecord.ResponseStatus.ToInt() < 400) && lastPreCheckRecord.AutoConfirmation == 0 && lastPreCheckRecord.ManualConfirmation == 0)
            {
                Logger.Info($"Проверка пропускается. ResponseStatus = {lastPreCheckRecord.ResponseStatus}," +
                    $" AutoConfirmation = {lastPreCheckRecord.AutoConfirmation}, ManualConfirmation = {lastPreCheckRecord.ManualConfirmation}", LogContext);
                return false;
            }
            return true;
        }


        /// <summary>
        /// Функция сравнения свойств двух классов
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">Первый объект сравнения</param>
        /// <param name="to">Второй объект сравнения</param>
        /// <param name="ignore">Свойства, который следует пропустить</param>
        /// <returns></returns>
        public bool PublicInstancePropertiesEqual<T>(T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                Type type = typeof(T);
                List<string> ignoreList = new List<string>(ignore);
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(pi.Name))
                    {
                        object selfValue = type.GetProperty(pi.Name)?.GetValue(self, null);
                        object toValue = type.GetProperty(pi.Name)?.GetValue(to, null);

                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return self == to;
        }

        public bool PublicNamesEqual(int companyId, List<LeiPublicNamesPreCheck> leiPublicNamesPreCheck, List<CmpAnotherNames> cmpAnotherNames)
        {
            Logger.Info($"Проверка наименований на других языках по Level1", LogContext);
            leiPublicNamesPreCheck = leiPublicNamesPreCheck
                .Where(w => w.CompanyId == companyId).ToList();

            cmpAnotherNames = cmpAnotherNames.Where(w => w.CompanyId == companyId).ToList();

            foreach (var an in cmpAnotherNames)
            {
                if (!leiPublicNamesPreCheck.Any(lpn => lpn.NameId == an.NameId
                                                    && lpn.IsArchived == an.IsArchived
                                                    && lpn.DeletedDt == an.DeletedDt
                                                    && lpn.IsOfficial == an.IsOfficial
                                                    && lpn.IsByMistake == an.IsByMistake
                                                    && lpn.LanguageCode == an.LanguageCode
                                                    && lpn.CmpName == an.CmpName
                                                    && lpn.CmpNameShort == an.CmpNameShort))
                {
                    Logger.Info($"Найдено отличие в наименовании CmpName={an.CmpName}, CmpNameShort={an.CmpNameShort}, IsByMistake={an.IsByMistake}, LanguageCode={an.LanguageCode}, IsOfficial={an.IsOfficial}, DeletedDt={an.DeletedDt}, IsArchived={an.IsArchived}", LogContext);
                    return false;
                }
            }

            return true;
        }


        public bool PublicNamesEqual(int companyId, List<LeiPublicNamesLevel2PreCheck> leiPublicNamesPreCheck, List<CmpAnotherNames> cmpAnotherNames)
        {
            Logger.Info($"Проверка наименований на других языках по Level2", LogContext);
            leiPublicNamesPreCheck = leiPublicNamesPreCheck.Where(w => w.CompanyId == companyId).ToList();
            cmpAnotherNames = cmpAnotherNames.Where(w => w.CompanyId == companyId).ToList();


            foreach (var an in cmpAnotherNames)
            {
                if (!leiPublicNamesPreCheck.Any(lpn => lpn.NameId == an.NameId
                                                       && lpn.IsArchived == an.IsArchived
                                                       && lpn.DeleteDt == an.DeletedDt
                                                       && lpn.IsOfficial == an.IsOfficial
                                                       && lpn.LanguageCode == an.LanguageCode
                                                       && lpn.CmpName == an.CmpName))
                {
                    Logger.Info($"Найдено отличие в CmpName={an.CmpName}, LanguageCode={an.LanguageCode}, IsOfficial={an.IsOfficial}, DeletedDt={an.DeletedDt}, IsArchived={an.IsArchived}", LogContext);
                    return false;
                }
            }

            return true;
        }

        public bool FastPreCheck(int companyId, string user, out List<int> preCheckIds)
        {
            var changeL1 = _storedProcedureRepository.LeiPublicGetChanges().FirstOrDefault(w => w.CompanyId == companyId);
            var leiPublicLevel2Changes = _leiPublicLevel2Repository.LeiPublicLevel2GetChanges(new List<int> { companyId });

            var directParent = leiPublicLevel2Changes.Where(x => x.LinkAttrib == LinkAttribDirect)
                .OrderByDescending(x => x.ChildLeiUpdateDate).FirstOrDefault();
            var ultimateParent = leiPublicLevel2Changes.Where(x => x.LinkAttrib == LinkAttribUltimate)
                .OrderByDescending(x => x.ChildLeiUpdateDate).FirstOrDefault();

            List<LeiPublicLevel2Changes> changesL2 = new List<LeiPublicLevel2Changes>();
            if (directParent != null)
                changesL2.Add(directParent);
            if (ultimateParent != null)
                changesL2.Add(ultimateParent);

            if (CheckNumberEight(companyId, changeL1, changesL2))
            {
                preCheckIds = new List<int>();
                CheckCompanies(new List<int> { companyId }, new List<LeiPublicChanges> { changeL1 }, changesL2, preCheckIds, user, true);

                // Возвращаем результат восьмой проверки
                return true;
            }
            else
            {
                preCheckIds = null;
                return false;
            }
        }

        /// <summary>
        /// Проверка наличия незаполненных полей для таблиц фиксации для записи из журнала Level1.
        /// </summary>
        /// <param name="change"></param>
        /// <returns></returns>
        public bool Level1RecordsHasRestrictions(LeiPublicChanges change)
        {
            return change != null && (change.HaveError || change.UploadStop);
        }

        /// <summary>
        /// Проверка наличия незаполненных полей для таблиц фиксации для записей по коду lei из журнала Level2.
        /// </summary>
        /// <param name="changes"></param>
        /// <returns></returns>
        public bool Level2RecordsHasRestrictions(List<LeiPublicLevel2Changes> changes)
        {
            return changes.Any(x => x != null && (x.HaveError || x.UploadStop == true));
        }

        /// <summary>
        /// Проверка №8
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="changesL1"></param>
        /// <param name="changesL2"></param>
        /// <returns></returns>
        public bool CheckNumberEight(int companyId, LeiPublicChanges changesL1, List<LeiPublicLevel2Changes> changesL2)
        {
            bool result = false;
            result = changesL1 != null ? result = !Level1RecordsHasRestrictions(changesL1) : result;
            result = changesL2.Count > 0 ? !Level2RecordsHasRestrictions(changesL2) : result;

            return result;
        }

        /// <summary>
        /// Проверка на наличие в журнале PreCheck Api записи по рассматриваемому коду
        /// </summary>
        /// <param name="leiCode"></param>
        /// <returns></returns>
        public bool IsPreCheckApiExists(string leiCode)
        {
            return _leiPreCheckRepository.Get().Any(r => r.LeiCode == leiCode);
        }

        /// <summary>
        /// Проверка на наличие в журнале PreCheck Api последней топовой записи с установленными
        /// флагами «Ручное подтвеждение» или «Автоматическое подтверждение» значением “1”.
        /// </summary>
        /// <returns></returns>
        public bool ManualOrAutoConfirmationChecked(string leiCode)
        {
            return _leiPreCheckRepository.ManualOrAutoConfirmationChecked(leiCode);
        }

        /// <summary>
        /// Проверка №7
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="leiPublicChange"></param>
        /// <param name="leiPublicLevel2Changes"></param>
        /// <param name="isLevel1Error">Флаг "Ошибка по Level 1 (true) или по Level 2"</param>
        /// <returns></returns>
        public bool CheckNumberSeven(int companyId, out LeiPublicChanges leiPublicChange, out List<LeiPublicLevel2Changes> leiPublicLevel2Changes, out bool isLevel1Error)
        {
            leiPublicChange = null;
            leiPublicLevel2Changes = null;
            isLevel1Error = true;

            // Получение записи для проверки на дубликаты из Level1 (Журнал Level1, Закладка ЗГКО, активный чекбокс "Показать записи для проверки на дубликаты")
            var lp = _storedProcedureRepository.GetDuplicateCheckRecordsLevel1(companyId);
            if (lp.HaveError)
            {
                return false;
            }

            // Если по организации, запрашивающей присвоение кода LEI(указанной в документе), отсутствует необходимая 
            // информация для формирования файла XML(rr_xml.xml или repex_xml.xml)(т.е.нет действующей «неархивной» записи по Direct на вкладке LEVEL2, см.п.11.2), 
            // то отправки данных на проверку в PreCheck API не должно быть и пользователю выдается сообщение с указанием организации,
            // по которой отсутствуют данные данные(по Level2) необходимые для формирования Request в PreCheck API.
            // Так же и для Ultimate
            var l2Parents = _storedProcedureRepository.GetLeiLevel2Parents(companyId).Where(r => !r.IsArchive && !r.DeleteDt.HasValue).ToList();

            if (!l2Parents.Any(r => r.LinkAttrib == LinkAttribDirect) || !l2Parents.Any(r => r.LinkAttrib == LinkAttribUltimate))
            {
                isLevel1Error = false;
                return false;
            }

            leiPublicChange = lp;

            // Метод вызывается при нажатии на конопку "Проверка PreCheck" после успешной проверки номер 7 для фиксации данных по организации,
            // чтобы были данные для формирования xml по level2 (см. п. 11.5 в ФТ)
            var d = _storedProcedureRepository.GetChangesForPendingValidation(lp.LeiCode, LinkAttribDirect);
            var u = _storedProcedureRepository.GetChangesForPendingValidation(lp.LeiCode, LinkAttribUltimate);

            leiPublicLevel2Changes = new List<LeiPublicLevel2Changes>();
            if (!d.IsNullOrWhitespace())
                leiPublicLevel2Changes.Add(d);
            if (!u.IsNullOrWhitespace())
                leiPublicLevel2Changes.Add(u);

            return true;
        }

        /// <summary>
        /// Функция «Автоматическое разрешение отправки данных в GLEIF на pre-check и фиксации в XML по LEI, перешедшим в LAPSED»
        /// </summary>
        public void NightPorter()
        {
            if (_calendarRepository.IsHoliday(DateTime.Now))
            {
                // по кодам LEI, только что получившим статус LAPSED в LeiExpirationCheckJob
                // будут добавлены записи в журнал PreCheck, если данные корректны
                ProcessChanges(user: "LeiExpirationCheckJob");

                PermitSendingToPreCheckForLapsed();

                SendLeiToPreCheckProcess(forNightPorter: true);

                LeiPublicDataFixationProcess();
            }
        }

        /// <summary>
        /// Для тех LEI кодов, которые перешли в статус «LAPSED» и добавлена запись в журнал PreCheck,
        /// в журнале «Журнал PreCheck API» проставить чек-бокс «Разрешить отправку данных в GLEIF на Pre-check»
        /// </summary>
        private void PermitSendingToPreCheckForLapsed()
        {
            // записи из истории кодов Lei, в которых код сегодня перешёл в LAPSED
            var leiCodeHistoryLapsed = _leiCodeHistoryRepository.GetTodayLapsed();

            // самые новые записи в журнале PreCheck по каждому коду, который сегодня перешёл в LAPSED
            var leiPreChecksTopIdsList = _leiPreCheckRepository.LeiPreCheckTopIdsForCodes(codes: leiCodeHistoryLapsed, initiator: "LeiExpirationCheckJob");

            leiPreChecksTopIdsList.ForEach((lpcId) =>
            {
                var entityToUpdate = _leiPreCheckRepository.Get().Where(l => l.LeiPreCheckId == lpcId).First();

                entityToUpdate.SendPermissionType = 1;
                entityToUpdate.SendPermissionInitiator = "LeiExpirationCheckJob";
                entityToUpdate.SendPermissionDate = DateTime.Now;
                
                _leiPreCheckRepository.Update(entityToUpdate);
            });
        }
    }
}
