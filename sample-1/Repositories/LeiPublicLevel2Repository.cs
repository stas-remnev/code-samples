using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using Nsd.Common;
using Nsd.Common.Extensions;
using Nsd.Repository.Base;
using Nsd.Repository.Ef.Model.Entities;
using Nsd.Repository.Ef.Repositories.Lei.Models;
using Nsd.Repository.Ef.StorageProcedureEntities.Lei;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Nsd.Repository.Ef.Repositories.Lei
{
    class LeiPublicLevel2Repository : BaseRepository<LeiPublicLevel2>, ILeiPublicLevel2Repository
    {
        private readonly IReadOnlyRepository<VLeiPublicNamesLevel2> _leiPublicNames2;

        public LeiPublicLevel2Repository(IReadOnlyRepository<VLeiPublicNamesLevel2> leiPublicNames2, BaseRepositoryContext context) : base(context)
        {
            _leiPublicNames2 = leiPublicNames2;
        }

        public DataTable UpdateLeiCodes(LeiVersion leiVersion)
        {
            // Выполним обновление и получим данные о том, что обновилось.
            return ExecuteProc<DataTable>("lei_public_level2_update", TimeSpan.FromMinutes(10));
        }

        public IEnumerable<LeiPublicTop> GetLeiPublicTopData(LeiXmlFileType leiXmlFileType, LeiVersion leiVersion, int? companyId = null)
        {
            var result = QueryProc<LeiPublicTop>("lei_public_level2#get_public", TimeSpan.FromMinutes(5),
                new DataParameter("@lei_xml_file_type", leiXmlFileType.GetMapValue()), new DataParameter("@company_id", companyId ?? null)).ToList();

            var leiPublicNames2 = Query<LeiPublicNameLevel2>(@"
             select company_id,
                     is_official,
                     language_code,
                     cmp_name
             from v_lei_public_names_level2", TimeSpan.FromMinutes(5), new { }).ToList();

            foreach (var r in result)
            {
                r.NamesLevel2 = leiPublicNames2.Where(x => x.CompanyId == r.CompanyId).ToList();
            }

            return result;
        }

        public IEnumerable<LeiPublicTop> GetLeiPublicPreCheckTopData(LeiXmlFileType leiXmlFileType, string linkAttrib, 
            int companyId, int leiPreCheckId)
        {
            var result = QueryProc<LeiPublicTop>("lei_public_level2_pre_check#get_public", TimeSpan.FromMinutes(5),
                new DataParameter("@lei_xml_file_type", leiXmlFileType.GetMapValue()),
                new DataParameter("@link_attrib", linkAttrib),
                new DataParameter("@lei_pre_check_id", leiPreCheckId),
                new DataParameter("@company_id", companyId));

            var names = Query<LeiPublicNameLevel2>(@"
             select company_id,
                     is_official,
                     language_code,
                     cmp_name
             from v_lei_public_names_level2_pre_check
             where company_id = @company_id", TimeSpan.FromMinutes(5), new DataParameter("@company_id", companyId)).ToList();

            foreach (var r in result)
            {
                r.NamesLevel2 = names.Where(x => x.CompanyId == r.CompanyId).ToList();
            }

            return result;
        }

        public List<LeiPublicLevel2Changes> LeiPublicLevel2GetChanges(List<int> companyIds = null)
        {
            DataParameter[] dataParameters = new DataParameter[]
            {
                new DataParameter ("company_ids", DataTableHelper.ToTable(companyIds.Select(x => new { id = x })), DataType.Structured),
                new DataParameter("fixation_mode", "N")
            };

            return QueryProc<LeiPublicLevel2Changes>("lei_public_level2_update", TimeSpan.FromMinutes(5), dataParameters).ToList();
        }

        public void LeiPublicLevel2Update(List<int> companyIds)
        {
            DataParameter[] dataParameters = new DataParameter[]
            {
                new DataParameter ("company_ids", DataTableHelper.ToTable(companyIds.Select(x => new { id = x })), DataType.Structured),
                new DataParameter("fixation_mode", "Y")
            };

            ExecuteProc("lei_public_Level2_update", TimeSpan.FromMinutes(5), dataParameters);
        }
        public List<LeiPublicLevel2Model> GetLeiPublicLevel2(List<int> companyIds)
        {
            DataParameter[] dataParameters = new DataParameter[]
            {
                new DataParameter ("@companyIds", DataTableHelper
                    .ToTable(companyIds.Select(x => new { id = x })), DataType.Structured)
            };

            return Query<LeiPublicLevel2Model>($@"
                select lpl2.*,
                       (
                         select case
                             when l2us.company_id is not null then 'Y'
                             else 'N'
                           end
                       ) as upload_stop
                from lei_public_level2 lpl2
                left join lei_level2_upload_stops l2us on lpl2.company_id = l2us.company_id
                where lpl2.child_lei_update_date is not null and lpl2.company_id in ({String.Join(", ", companyIds.ToArray())})").ToList();
        }
    }
}
