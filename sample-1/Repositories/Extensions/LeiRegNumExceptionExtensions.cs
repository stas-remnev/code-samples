using Nsd.Repository.Base;
using Nsd.Repository.Ef.Model.Entities;
using System.Linq;

namespace Nsd.Repository.Ef.Repositories.Lei.Extensions
{
    public static class LeiRegNumExceptionExtensions
    {
        public static string GetRegNumException(this IRepository<LeiRegNumExceptions> repository, string regNum, string tagName, string legalJurisdiction)
        {
            return repository.Query<string>("select dbo.lei_get_reg_num_exception(@reg_num, @tag_name, @legal_jurisdiction)",
                new { reg_num = regNum, tag_name = tagName, @legal_jurisdiction = legalJurisdiction }).FirstOrDefault();
        }
    }
}
