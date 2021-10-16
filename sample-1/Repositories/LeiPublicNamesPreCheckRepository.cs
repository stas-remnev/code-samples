using System.Collections.Generic;
using System.Linq;
using Nsd.Repository.Ef.Model.Entities;
using Nsd.Repository.Ef.StorageProcedureEntities.Lei;

namespace Nsd.Repository.Ef.Repositories.Lei
{
    public class LeiPublicNamesPreCheckRepository : BaseRepository<LeiPublicNamesPreCheck>, ILeiPublicNamesPreCheckRepository
    {
        public LeiPublicNamesPreCheckRepository(BaseRepositoryContext context) : base(context)
        {
        }

        public List<LeiPublicNamesPreCheckModel> GetActualLeiPublicNamesPreCheck()
        {
            var names = Query<LeiPublicNamesPreCheckModel>(@"
                select rn.name_id,
                       rn.company_id,
                       rn.language_code,
                       rn.cmp_name,
                       rn.cmp_name_short,
                       rn.is_official,
                       rn.is_archived,
                       rn.is_by_mistake,
                       rn.deleted_dt
                from (
                  select lpn.name_id,
                         lpn.company_id,
                         lpn.language_code,
                         lpn.cmp_name,
                         lpn.cmp_name_short,
                         lpn.is_official,
                         lpn.is_archived,
                         lpn.is_by_mistake,
                         lpn.deleted_dt,
                         row_number() over (partition by lpn.company_id, lpn.name_id order by lpn.insert_dt desc) as row_num
                  from dbo.lei_public_names_pre_check as lpn
                ) as rn
                where rn.row_num = 1
                ").ToList();

            return names;
        }
    }
}
