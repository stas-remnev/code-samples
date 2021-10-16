using Nsd.Repository.Ef.StorageProcedureEntities.Lei;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nsd.Service.CorpDb.Services.Lei
{
    public interface ILeiPreCheckService
    {
        /// <summary>
        /// Отправка запроса PreCheck в GLEIF
        /// </summary>
        /// <param name="leiPreCheckId"></param>
        /// <param name="requestAuthor"></param>
        /// <returns></returns>
        Task<IRestResponse> SendPreCheckAsync(int leiPreCheckId, string requestAuthor, bool forNightPorter = false);

        /// <summary>
        /// П.1   Проверка наличия расхождений
        /// </summary>
        bool ProcessChanges(List<int> companyIds = null, string user = null, List<LeiPublicLevel2Changes> level2Changes = null);

        bool CheckCompanies(List<int> companiesToCheck, List<LeiPublicChanges> leiPublicChanges,
            List<LeiPublicLevel2Changes> leiPublicLevel2Changes, List<int> preCheckIds, string user = null, bool forced = false,
            bool isPendingValidStatusEnabled = true);

        /// <summary>
        /// Проверка №7
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        bool CheckNumberSeven(int companyId, out LeiPublicChanges leiPublicChange, out List<LeiPublicLevel2Changes> leiPublicLevel2Changes, out bool isLevel1Error);

        /// <summary>
        /// Проверка на наличие в журнале PreCheck Api последней топовой записи с установленными флагами «Ручное подтвеждение» или «Автоматическое подтверждение» значением “1”.
        /// </summary>
        /// <returns></returns>
        bool ManualOrAutoConfirmationChecked(string leiCode);

        /// <summary>
        /// Список записей для отправки в PreCHeck
        /// </summary>
        /// <returns>Список записей LeiPreCheck</returns>
        List<LeiPreCheckModel> GetLeiPreCheckRecordsForGleif();

        /// <summary>
        /// П.2 Отправка данных в GLEIF на PreCheck API проверку
        /// </summary>
        void SendLeiToPreCheckProcess(bool forNightPorter = false, string requestAuthor = null);

        /// <summary>
        /// Выполнение операции FastPreCheck. Запускается при смене статуса документа на код LEI (Новое - Исполнен и На исполнении - исполнено)
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="user"></param>
        /// <param name="preCheckIds"></param>
        /// <returns></returns>
        bool FastPreCheck(int companyId, string user, out List<int> preCheckIds);

        /// <summary>
        /// Проверка на наличие в журнале PreCheck Api записи по рассматриваемому коду
        /// </summary>
        /// <param name="leiCode"></param>
        /// <returns></returns>
        bool IsPreCheckApiExists(string leiCode);

        /// <summary>
        /// П.3 Фиксация данных в «стандартных» таблицах для возможности последующего формирования и отправки в GLEIF XML файлов
        /// </summary>
        void LeiPublicDataFixationProcess();

        void LeiPublicDataFixation(List<int> companiesToFixationList);

        /// <summary>
        /// Функция «Автоматическое разрешение отправки данных в GLEIF на pre-check и фиксации в XML по LEI, перешедшим в LAPSED»
        /// </summary>
        void NightPorter();

        bool IsPrecheckSuccessful(int leiPrecheckId);
    }
}
