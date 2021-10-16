using System;
using System.Collections.Generic;
using System.Data;

using Nsd.Repository.Base;
using Nsd.Repository.Ef.Model.Entities;
using Nsd.Repository.Ef.Repositories.Lei.Models;
using Nsd.Repository.Ef.StorageProcedureEntities.Lei;

namespace Nsd.Repository.Ef.Repositories.Lei
{
    public interface ILeiPublicRepository : IRepository<LeiPublic>
    {
        void SaveXmlFileRefs(string fileNameFull, long fileSizeFull, string fileNameDelta, long? fileSizeDelta, LeiVersion leiVersion, LeiXmlFileType leiXmlFileType);
        string GetNdcLeiCode();
        IEnumerable<LeiPublicTop> GetLeiPublicTopData(LeiXmlFileType leiXmlFileType, LeiVersion leiVersion, int? companyId = null);
        IEnumerable<LeiPublicTop> GetLeiPublicPreCheckTopData(LeiXmlFileType leiXmlFileType, LeiVersion leiVersion, int? companyId = null, int? leiPreCheckId = null);
        List<LeiPublicModel> GetLeiPublic(List<int> companyIds);
        DateTime? GetLastFileDate();
        DataTable UpdateLeiCodes(LeiVersion leiVersion);
        string GetSubscriptionRecipients();
        void LeiPublicUpdate(List<int> companyIds);
    }
}
