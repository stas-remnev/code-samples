using Nsd.Repository.Ef.Model.Entities;

namespace Nsd.Repository.Ef.Repositories.Lei
{
    public class LeiPublicLevel2PreCheckRepository : BaseRepository<LeiPublicLevel2PreCheck>, ILeiPublicLevel2PreCheckRepository
    {
        public LeiPublicLevel2PreCheckRepository(BaseRepositoryContext context) : base(context)
        {
        }

    }
}
