using Nsd.Repository.Base;
using Nsd.Repository.Ef.StorageProcedureEntities.Lei;
using System;
using System.Collections.Generic;

namespace Nsd.Repository.Ef.Repositories.Lei.Extensions
{
    public static class LeiCodeExtensions
    {
        public static IEnumerable<string> GetLeiCodeOperations(this IStoredProcedureRepository repository, DateTime dt, string docTypeMn)
        {
            return repository.ExecuteProc<IEnumerable<string>>("rp_lei_code_operations", TimeSpan.FromMinutes(1),
                new { report_date = dt, doc_type_mn = docTypeMn });
        }

        public static int LeiCodeHistoryExpirationCheck(this IStoredProcedureRepository repository)
        {
            return repository.ExecuteProc<int>("lei_code_history_expiration_check", null);
        }

        public static IEnumerable<LeiExpirationCheck> LeiCodeExpirationCheck(this IStoredProcedureRepository repository)
        {
            return repository.QueryProc<LeiExpirationCheck>("lei_code#expiration_check", null);
        }
    }
}
