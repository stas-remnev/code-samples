using Nsd.Repository.Base;
using Nsd.Repository.Ef.Model.Entities;
using System.Collections.Generic;
using Nsd.Repository.Ef.StorageProcedureEntities.Lei;

namespace Nsd.Repository.Ef.Repositories.Lei
{
    public interface ILeiPreCheckRepository : IRepository<LeiPreCheck>, IAsyncRepository<LeiPreCheck>
    {
        /// <summary>
        /// Идентификаторы самых новых записей в журнале PreCheck по каждому переданному коду
        /// </summary>
        /// <param name="codes">Список кодов, по которым нужно получить последние данные</param>
        /// <param name="initiator">Пользователь, добавивший записи в журнал PreCheck</param>
        /// <returns>Идентификаторы самых новых записей из журнала Lei PreCheck API по дате добавления</returns>
        List<int> LeiPreCheckTopIdsForCodes(IEnumerable<string> codes, string initiator = null);

        /// <summary>
        /// Возвращает записи из журнала PreCheck, готовые к отпраке в Gleif
        /// </summary>
        /// <returns>Массив идентификаторов</returns>
        List<LeiPreCheckModel> GetLeiPreCheckRecordsForGleif();

        /// <summary>
        /// Проверка на наличие в журнале PreCheck Api последней топовой записи с установленными
        /// флагами «Ручное подтвеждение» или «Автоматическое подтверждение» значением “1”.
        /// </summary>
        /// <returns></returns>
        bool ManualOrAutoConfirmationChecked(string leiCode);
    }
}