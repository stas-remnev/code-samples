using Nsd.Repository.Ef.Model.Entities;

namespace Nsd.Repository.Ef.Repositories.Lei
{
    public class LeiPublicPreCheckRepository : BaseRepository<LeiPublicPreCheck>, ILeiPublicPreCheckRepository
    {
        public LeiPublicPreCheckRepository(BaseRepositoryContext context) : base(context)
        {
        }

    }
}
