using Nsd.Repository.Base;
using Nsd.Repository.Ef.Model.Entities;

namespace Nsd.Repository.Ef.Repositories.Lei
{
    public interface ILeiXmlTagsValuesRepository : IRepository<LeiXmlTagsValues>
    {
        LeiXmlTagsValues LoadByCompanyId(int companyId);
        void Save(LeiXmlTagsValues leiXmlTagsValues);
        void Delete(int recId);
        string GetCompanyValidationSource(int companyId);
        string GetOtherRegistrationAuthorityId(int companyId);
        string GetMailRouting(int companyId);
    }
}