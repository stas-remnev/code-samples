using Nsd.Common.Extensions;
using Nsd.Repository.Ef.Model.Entities;
using Nsd.Repository.Ef.StorageProcedureEntities.Lei;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nsd.Repository.Ef.Repositories.Lei
{
    public class LeiPreCheckRepository : BaseRepository<LeiPreCheck>, ILeiPreCheckRepository
    {
        public LeiPreCheckRepository(BaseRepositoryContext context) : base(context)
        {
        }

        /// <summary>
        /// Идентификаторы самых новых записей в журнале PreCheck по каждому переданному коду
        /// </summary>
        /// <param name="codes">Список кодов, по которым нужно получить последние данные</param>
        /// <param name="initiator">Пользователь, добавивший записи в журнал PreCheck</param>
        /// <returns>Идентификаторы самых новых записей из журнала Lei PreCheck API по дате добавления</returns>
        public List<int> LeiPreCheckTopIdsForCodes(IEnumerable<string> codes, string initiator = null)
        {
            return Get()
                    .Where(h => h.InsertDt.Date == DateTime.Now.Date &&
                                (initiator.IsNullOrEmpty() || h.RequestInitiator == initiator) &&
                                h.LeiPreCheckId == Get()
                                                    .Where(h2 => h2.LeiCode == h.LeiCode)
                                                    .OrderByDescending(hSort => hSort.InsertDt)
                                                    .Select(hSelect => hSelect.LeiPreCheckId)
                                                    .First())
                    .Join(codes, l => l.LeiCode, c => c, (l, c) => l)
                    .Select(lpc => lpc.LeiPreCheckId) // нам нужны только идентификаторы, не нужно тратить зря память на выгрузку XML
                    .Distinct()
                    .ToList();
        }

        public List<LeiPreCheckModel> GetLeiPreCheckRecordsForGleif()
        {
            var records = Query<LeiPreCheckModel>(@"select lpc.*
            from lei_pre_check lpc
                join (
                    select lpc.company_id as company_id,
            max(lpc.lei_pre_check_id) as lei_pre_check_id
            from lei_pre_check lpc
                group by lpc.company_id
                ) ar on ar.lei_pre_check_id = lpc.lei_pre_check_id
            where lpc.send_permission_type = 1
            and lpc.response_date is null
            and lpc.send_date is null").ToList();

            return records;
        }

        public bool ManualOrAutoConfirmationChecked(string leiCode)
        {
            var leiPrecheck = Query<LeiPreCheckModel>(
@"select top 1 *
from lei_pre_check as lpc
where lpc.lei_code = @lei_code
order by lei_pre_check_id desc", new { lei_code = leiCode }).ToList().FirstOrDefault();

            return (leiPrecheck != null) && (leiPrecheck.AutoConfirmation == 1 || leiPrecheck.ManualConfirmation == 1);
        }
    }
}
