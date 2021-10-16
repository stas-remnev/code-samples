using System.Collections.Generic;
using System.Data;

using Nsd.Repository.Base;
using Nsd.Repository.Ef.Model.Entities;
using Nsd.Repository.Ef.Repositories.Lei.Models;
using Nsd.Repository.Ef.StorageProcedureEntities.Lei;

namespace Nsd.Repository.Ef.Repositories.Lei
{
    public interface ILeiPublicLevel2Repository : IRepository<LeiPublicLevel2>
    {
        DataTable UpdateLeiCodes(LeiVersion leiVersion);

        IEnumerable<LeiPublicTop> GetLeiPublicTopData(LeiXmlFileType leiXmlFileType, LeiVersion leiVersion, int? companyId = null);
        List<LeiPublicLevel2Model> GetLeiPublicLevel2(List<int> companyIds);
        IEnumerable<LeiPublicTop> GetLeiPublicPreCheckTopData(LeiXmlFileType leiXmlFileType, string linkAttrib, int companyId, int leiPreCheckId);

        List<LeiPublicLevel2Changes> LeiPublicLevel2GetChanges(List<int> companyIds = null);

        void LeiPublicLevel2Update(List<int> companyIds);
    }
}
