using System.Collections.Generic;
using System.Linq;
using Nsd.Repository.Ef.Model.Entities;
using Nsd.Repository.Ef.StorageProcedureEntities.Lei;

namespace Nsd.Repository.Ef.Repositories.Lei
{
    public class LeiPublicNamesLevel2PreCheckRepository : BaseRepository<LeiPublicNamesLevel2PreCheck>, ILeiPublicNamesLevel2PreCheckRepository
    {
        public LeiPublicNamesLevel2PreCheckRepository(BaseRepositoryContext context) : base(context)
        {
        }

        public List<LeiPublicNamesLevel2PreCheckModel> GetActualLeiPublicNamesLevel2PreCheck()
        {
            var names = Query<LeiPublicNamesLevel2PreCheckModel>(@"
                select rn.name_id,
                       rn.company_id,
                       rn.language_code,
                       rn.cmp_name,
                       rn.is_official,
                       rn.is_archived,
                       rn.delete_dt
                from (
                  select lpnl2.name_id,
                         lpnl2.company_id,
                         lpnl2.language_code,
                         lpnl2.cmp_name,
                         lpnl2.is_official,
                         lpnl2.is_archived,
                         lpnl2.delete_dt,
                         row_number() over (partition by lpnl2.company_id, lpnl2.name_id order by lpnl2.insert_dt desc) as row_num
                  from dbo.lei_public_names_level2_pre_check as lpnl2
                ) as rn
                where rn.row_num = 1
                ").ToList();

            return names;
        }
    }
}
