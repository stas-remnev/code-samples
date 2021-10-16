using LinqToDB.Data;
using Nsd.Repository.Ef.Model.Entities;
using Nsd.Repository.Ef.Repositories.Lei.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Nsd.Repository.Base;
using Nsd.Common;
using Nsd.Repository.Ef.Repositories.Lei.Models;
using LinqToDB;
using Nsd.Repository.Ef.StorageProcedureEntities.Lei;

namespace Nsd.Repository.Ef.Repositories.Lei
{
    class LeiPublicRepository : BaseRepository<LeiPublic>, ILeiPublicRepository
    {
        private readonly IReadOnlyRepository<Companies> _companiesRepository;
        private readonly IReadOnlyRepository<LeiXmlTagsValues> _leiXmlTagsValuesRepository;
        public LeiPublicRepository(IReadOnlyRepository<Companies> companiesRepository, BaseRepositoryContext context, IReadOnlyRepository<LeiXmlTagsValues> leiXmlTagsValuesRepository) : base(context)
        {
            _companiesRepository = companiesRepository;
            _leiXmlTagsValuesRepository = leiXmlTagsValuesRepository;
        }

        /// <summary>
        /// Список организаций.
        /// </summary>
        /// <returns></returns>
        public IList<Companies> GetCompaniesWithLeiCodes()
        {
            return (IList<Companies>)QueryToListAsync<Companies>(@"select company_id, ndc_cmp_code, full_name, short_name, common_name, full_name_en, short_name_en, common_name_full_en, common_name_short_en,
        cnt.country_name,ct.city_name, c.is_resident, c.id_pcs from companies c
        left join countries cnt on c.country_code = cnt.country_code
        left join cities ct on c.city_id = ct.city_id
        where exists(select * from dbo.v_cmp_codes vc where vc.company_id = c.company_id and vc.cmp_code_type_mn = 'LEI')", new { });
        }

        /// <summary>
        /// Загружает свежие данные для Lei.
        /// </summary>
        /// <param name="soonVerification">if set to <c>true</c> [soon verification].</param>
        /// <param name="companyId">Идентификатор компании</param>
        /// <returns></returns>
        public List<LeiPublic> GetLeiChanges(bool soonVerification, int? companyId = null)
        {
            return QueryProc<LeiPublic>("lei_public#get_changes", TimeSpan.FromMinutes(10),
                     new DataParameter("@SoonVerification", soonVerification ? 'Y' : 'N'),
                    new DataParameter("@company_id", companyId)).ToList();
        }

        /// <summary>
        /// Загружает данные по записям для проверки на дубликаты
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<LeiPublic> GetDuplicateCheckRecords(int? companyId = null)
        {
            return QueryProc<LeiPublic>("lei_check_duplicate_records#get_list", TimeSpan.FromMinutes(10),
                    new DataParameter("@company_id", companyId)).ToList();
        }

        /// <summary>
        /// Переключает флажок "Не отправлять" и возвращает его новое состояние.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns></returns>
        public bool LeiUploadToggle(int companyId)
        {
            return ExecuteProc("lei_upload_stops#toggle", new DataParameter("@company_id", companyId)).ToString() == "Y";
        }

        /// <summary>
        /// Загружает актуальные данные Lei.
        /// </summary>
        /// <param name="soonVerification">if set to <c>true</c> [soon verification].</param>
        /// <returns></returns>
        public List<LeiPublic> GetLeiPublic(bool soonVerification)
        {
            return QueryProc<LeiPublic>("lei_public#get_public", new DataParameter("@SoonVerification", soonVerification ? 'Y' : 'N')).ToList();
        }

        /// <summary>
        /// Получить список заявлений на верификацию кода LEI.
        /// </summary>
        /// <returns> Список заявлений на верификацию кода LEI </returns>
        public List<LeiVerificationRequests> GetNewRequests()
        {
            return Query<LeiVerificationRequests>(@"select  record_id,
                                xml_request,
                                lei_code,
                                company_id,
                                edo_doc_id,
                                sender_code,
                                sender_id,
                                doc_id,
                                process_status_mn,
                                lei_refuse_reason,
                                lei_client_department_refuse_reason_id,
                                not_match_cmp_name,
                                not_match_ogrn,
                                under_control,
                                comment,
                                insert_dt,
                                insert_user
                        from    lei_verification_requests as lvr
                        where   isnull(lei_code, '') = '' and
                                process_status_mn = 'NEW'").ToList();
        }

        /// <summary>
        /// Загружает новые заявления по коду Lei.
        /// </summary>
        /// <returns></returns>
        public int? NotificationDocIdGetByParentDocId(int parentDocId)
        {
            return ExecuteProc<DocLeNotification>("doc_lei_notification#get_id", new DataParameter("@doc_id", parentDocId)).DocId;
        }

        /// <summary>
        /// Загружает данные по истории Lei.
        /// </summary>
        /// <param name="soonVerification">if set to <c>true</c> [soon verification].</param>
        /// <returns></returns>
        public List<LeiPublic> GetLeiHistory(bool soonVerification)
        {
            return QueryProc<LeiPublic>("lei_public#get_history", new DataParameter("@SoonVerification", soonVerification ? 'Y' : 'N')).ToList();
        }

        /// <summary>
        /// Устанавливает или снимает флаг "Оплачено" в БД.
        /// </summary>
        /// <param name="docId">The document identifier.</param>
        /// <param name="payed">if set to <c>true</c> [payed].</param>
        public void SetPayedFlag(int docId, bool payed)
        {
            Execute(@"update dbo.doc_lei set payed = @payed where doc_id = @doc_id", new DataParameter("@payed", payed ? "Y" : "N"), new DataParameter("@doc_id", docId));
        }

        public IList<LeiCodeHistory> GetLeiCodeHistory(DateTime? dateFrom, DateTime? dateTill, bool forCorrespondence = false)
        {
            return QueryProc<LeiCodeHistory>("lei_code_history#get_list", new DataParameter("@date_from", dateFrom),
                                                                   new DataParameter("@date_till", dateTill),
                                                                   new DataParameter("@for_correspondence", forCorrespondence ? "Y" : "N")).ToList();
        }

        public DataTable GetLeiHistoryStatistics(object historyDateFrom, object historyDateTill, object director, object finDirector)
        {
            return (DataTable)QueryProc<DataTable>("rp_lei_history_statistics", new DataParameter("@from_date", historyDateFrom),
                                                                                new DataParameter("@till_date", historyDateTill),
                                                                                new DataParameter("@director", director),
                                                                                new DataParameter("@fin_director", finDirector));
        }

        /// <summary>
        /// Возвращает код LEI НРД.
        /// </summary>
        /// <returns></returns>
        public string GetNdcLeiCode()
        {
            return Execute<string>("select dbo.get_cmp_code(dbo.get_ndc_id(), 'LEI')", new { });
        }

        /// <summary>
        /// Обновляет коды LEI новыми данными.
        /// </summary>
        public DataTable UpdateLeiCodes(LeiVersion leiVersion)
        {
            // Выполним обновление и получим данные о том, что обновилось.
            return ExecuteProc<DataTable>("lei_public_update", TimeSpan.FromMinutes(10), new DataParameter("lei_version", leiVersion.ToNumberString()));
        }

        /// <summary>
        /// Возвращает список получателей уведомлений.
        /// </summary>
        /// <returns></returns>
        public string GetSubscriptionRecipients()
        {
            return QueryProc<string>("im_email_subscription_recipients", new DataParameter("im_email_group_mn", "LEI_UPDATE")).FirstOrDefault();
        }

        /// <summary>
        /// Возвращает актуальные (top) данные по кодам LEI.
        /// </summary>
        public IEnumerable<LeiPublicTop> GetLeiPublicTopData(LeiXmlFileType leiXmlFileType, LeiVersion leiVersion, int? companyId = null)
        {
            var result = QueryProc<LeiPublicTop>("lei_public#get_public", new DataParameter("@lei_version", leiVersion.ToNumberString()), 
                new DataParameter("@LastDayOnly", leiXmlFileType == LeiXmlFileType.Delta ? "Y" : "N"), new DataParameter("@lei_code_status", "A"), new DataParameter("@company_id", companyId ?? null)).ToList();

            var name = Query<LeiPublicName>(@"select  company_id,
         is_official,
         language_code,
         cmp_name,
         transliterated,
         format_version
 from dbo.v_lei_public_names
 union
 select company_id,
         is_official,
         language_code,
         cmp_name,
         transliterated,
         format_version
 from dbo.v_lei_public_names2", new { }).ToList();

            var companies = _companiesRepository.Get()
                .Where(w => companyId == null || w.CompanyId == companyId).ToList();

            foreach (var r in result)
            {
                r.Names = name.Where(x => x.CompanyId == r.CompanyId).ToList();
                r.Company = companies.Where(x => x.CompanyId == r.CompanyId).FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// Возвращает актуальные (top) данные по кодам LEI.
        /// </summary>
        public IEnumerable<LeiPublicTop> GetLeiPublicPreCheckTopData(LeiXmlFileType leiXmlFileType, LeiVersion leiVersion, int? companyId = null, int? leiPreCheckId = null)
        {
            var result = QueryProc<LeiPublicTop>("lei_public_pre_check#get_public", new DataParameter("@lei_version", leiVersion.ToNumberString()), new DataParameter("@company_id", companyId ?? null)).ToList();

            var names = Query<LeiPublicName>(@"
select  company_id,
        is_official,
        language_code,
        cmp_name,
        transliterated,
        format_version
from    dbo.v_lei_public_names_pre_check
union
select  company_id,
        is_official,
        language_code,
        cmp_name,
        transliterated,
        format_version
from    dbo.v_lei_public_names2_pre_check", new { }).ToList();

            var companies = _companiesRepository.Get()
                .Where(w => companyId == null || w.CompanyId == companyId).ToList();

            var tagValues = _leiXmlTagsValuesRepository.Get()
                .Where(w => companyId == null || w.CompanyId == companyId).ToList();

            companies.ForEach(x=> x.LeiXmlTagsValues = tagValues.Where(w => w.CompanyId == x.CompanyId).ToList());

            foreach (var r in result)
            {
                r.Names = names.Where(x => x.CompanyId == r.CompanyId).ToList();
                r.Company = companies.Where(x => x.CompanyId == r.CompanyId).FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// Возвращает последнюю дату ранее сгенерированных файлов без учета сегодняшних.
        /// </summary>
        /// <returns></returns>
        public DateTime? GetLastFileDate()
        {
            return Query<DateTime>(@"
select top 1
        file_date
from    dbo.lei_file_refs
where   dbo.date_only(file_date) < dbo.date_only(getdate())
order by file_date desc").FirstOrDefault();
        }

        /// <summary>
        /// Сохраняет данные по файлам в таблице lei_file_refs.
        /// </summary>
        /// <param name="fileNameFull">The file name full.</param>
        /// <param name="fileSizeFull">The file size full.</param>
        /// <param name="fileNameDelta">The file name delta.</param>
        /// <param name="fileSizeDelta">The file size delta.</param>
        /// <param name="leiVersion"></param>
        /// <param name="leiXmlFileType"></param>
        public void SaveXmlFileRefs(string fileNameFull, long fileSizeFull, string fileNameDelta, long? fileSizeDelta, LeiVersion leiVersion, LeiXmlFileType leiXmlFileType)
        {
            string iterationNumber = string.Empty;
            int position = fileNameFull.IndexOf('-', fileNameFull.Length - 10);
            if (position >= 0)
                iterationNumber = fileNameFull.Substring(position + 1, fileNameFull.IndexOf(".xml") - position - 1);
            else iterationNumber = "0";

            ExecuteProc("lei_file_refs#save", 
                new DataParameter("@file_name_full", fileNameFull),
                new DataParameter("@file_size_full", fileSizeFull),
                new DataParameter("@file_name_delta", fileNameDelta),
                new DataParameter("@file_size_delta", fileSizeDelta),
                new DataParameter("@file_type", leiXmlFileType.GetFilePostfix()),
                new DataParameter("@file_format_version", leiVersion.ToNumberString()),
                new DataParameter("@iteration_number", iterationNumber));
        }

        /// <summary>
        /// Возвращает последнее имя файла.
        /// </summary>
        public string GetLastFileName(LeiVersion leiVersion)
        {
            return Query<string>($"select top 1 file_name_full from lei_file_refs where file_format_version = '{leiVersion.ToNumberString()}' order by file_date desc").FirstOrDefault();
        }

        /// <summary>
        /// Возвращает данные по коду LEI
        /// </summary>
        /// <param name="leiCode"></param>
        /// <returns></returns>
        public LeiPublic GetLeiPublicByLeiCode(string leiCode)
        {
            LeiPublic result;

            result = Query<LeiPublic>(@"select	*
                                        from	dbo.v_lei_public_data
                                        where	lei_code = @lei_code",
                       new DataParameter("@lei_code", leiCode)).First();

            if (result != null)
            {
                result.ManagingLou = GetNdcLeiCode();
                var dt = DateTime.Now;
                dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
                result.LeiAssignmentDate = result.LeiUpdateDate = dt;
                result.LeiNextRecertificationDate = result.LeiAssignmentDate.Value.AddYears(1);
            }

            return result;
        }

        public void LeiPublicUpdate(List<int> companyIds)
        {
            DataParameter[] dataParameters = new DataParameter[]
            {
                new DataParameter ("company_ids", DataTableHelper.ToTable(companyIds.Select(x => new { id = x })), DataType.Structured),
                new DataParameter("lei_version", "2.0")
            };

            ExecuteProc("lei_public_update", TimeSpan.FromMinutes(5), dataParameters);
        }

        public List<LeiPublicModel> GetLeiPublic(List<int> companyIds)
        {
            DataParameter[] dataParameters = new DataParameter[]
            {
                new DataParameter ("@companyIds", DataTableHelper
                    .ToTable(companyIds.Select(x => new { id = x })), DataType.Structured)
            };

            return Query<LeiPublicModel>($@"
                select lp.*,
                       (
                         select case
                             when lus.company_id is not null then 'Y'
                             else 'N'
                           end
                       ) as upload_stop
                from lei_public lp
                left join lei_upload_stops lus on lp.company_id = lus.company_id
                where lp.company_id in ({String.Join(", ", companyIds.ToArray())})").ToList();
        }
    }
}
