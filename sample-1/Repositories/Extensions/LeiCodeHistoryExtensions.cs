using Nsd.Repository.Base;
using Nsd.Repository.Ef.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nsd.Repository.Ef.Repositories.Lei.Extensions
{
    public static class LeiCodeHistoryExtensions
    {
        /// <summary>
        /// Выбрать все коды, перешедшие сегодня в статус LAPSED
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetTodayLapsed(this IReadOnlyRepository<LeiCodeHistory> repository)
        {
            return repository.Get()
                .Where(h => h.LeiCodeState == "LAPSED" && 
                            h.InsertDt.Date == DateTime.Now.Date &&
                            h.LeiCodeHistoryId == repository.Get()
                                                            .Where(h2 => h2.CmpCodeId == h.CmpCodeId)
                                                            .OrderByDescending(c => c.InsertDt)
                                                            .Select(h3 => h3.LeiCodeHistoryId)
                                                            .First())
                .Select(l => l.CmpCode.CmpCode)
                .Distinct();
        }
    }
}
