using System.Collections.Generic;
using Nsd.Repository.Base;
using Nsd.Repository.Ef.Model.Entities;
using Nsd.Repository.Ef.StorageProcedureEntities.Lei;

namespace Nsd.Repository.Ef.Repositories.Lei
{
    public interface ILeiPublicNamesPreCheckRepository : IRepository<LeiPublicNamesPreCheck>
    {
        List<LeiPublicNamesPreCheckModel> GetActualLeiPublicNamesPreCheck();
    }
}