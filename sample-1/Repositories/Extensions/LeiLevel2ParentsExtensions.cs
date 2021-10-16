using System;
using Nsd.Repository.Base;
using System.Collections.Generic;
using System.Linq;
using Nsd.Repository.Ef.StorageProcedureEntities.Entities;

namespace Nsd.Repository.Ef.Repositories.Lei.Extensions
{
    public static class LeiLevel2ParentsExtensions
    {
        public static List<LeiLevel2ParentsInfo> GetLeiLevel2Parents(this IStoredProcedureRepository repository, int childCompanyId)
        {
            return repository.QueryProc<LeiLevel2ParentsInfo>("lei_level2_parents#get_list", TimeSpan.FromMinutes(5), new { child_company_id = childCompanyId }).ToList();
        }
    }
}
