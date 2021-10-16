using System;
using System.Collections.Generic;
using System.Linq;
using Nsd.Repository.Base;

namespace Nsd.Repository.Ef.Repositories.XSbr.Extensions
{
    public static class XSbrRepositoryExtensions
    {
        public static List<IssueNdcToCompanyIdModel> GetIssueNdcCodesWithBaseCompanyIds(this IRepository<Model.Entities.XSbr> repository, string[] drNdcCodes)
        {
            var issues = repository.Query<IssueNdcToCompanyIdModel>($@"
        select xs_origin.ndc_code, c.company_id from x_sbr xs_base
          join companies c on xs_base.issuer_id = c.company_id
          join dr_securities ds on ds.base_object_id = xs_base.object_id and ds.base_object_type = xs_base.object_type
          join x_sbr xs_origin on ds.dr_security_id = xs_origin.object_id
        where xs_origin.ndc_code in ('{String.Join("', '", drNdcCodes)}')
        ", null).ToList();

            return issues;
        }

        public static List<CompanyBaseIssuerInfoModel> GetCompanyBaseIssuerInfo(this IRepository<Model.Entities.XSbr> repository, string[] drNdcCodes)
        {
            var issues = repository.Query<CompanyBaseIssuerInfoModel>($@"
        select xs.ndc_code [ndc_code],
               c.ndc_cmp_code [cmp_ndc_code],
               state_reg_numbers.state_reg_number [state_reg_number],
               base_resident_status.status [is_base_resident],
               case
                 when base_resident_status.status = '2' then c.short_name_en
                 else coalesce(c.short_name, c.full_name)
               end [base_resident_name],
               c.inn [inn],
               c.kpp [kpp],
               c.ogrn [ogrn],
               oksm_codes.code [oksm_code],
               case
                 when exists (
                     select s.company_id
                     from statuses s
                     join companies c1 on s.company_id = c1.company_id
                     join status_types st on s.status_type_id = st.status_type_id
                       and st.status_type_mn = 'МХК'
                     where s.company_id = c.company_id
                   ) then 'LEI:' + lei_codes.cmp_code
                 when c.tin is not null then 'TIN:' + c.tin
                 when lei_codes.cmp_code is not null then 'LEI:' + lei_codes.cmp_code
                 when c.tax_number is not null then 'RN:' + c.tax_number
                 when c.state_reg_num is not null then 'RN:' + c.state_reg_num
               end [tin],
               c.foreign_economy_sector_code [foreign_economy_sector_code]
        from x_sbr xs
        join dr_securities ds on xs.dr_security_id = ds.dr_security_id
        join x_sbr xs_base on ds.base_object_id = xs_base.object_id
          and ds.base_object_type = xs_base.object_type
        join companies c on xs_base.issuer_id = c.company_id
        outer apply (
          select case
                   when exists (
                       select s.company_id
                       from statuses s
                       join companies c1 on s.company_id = c1.company_id
                       join status_types st on s.status_type_id = st.status_type_id
                       join company_types ct on c1.comp_type_id = ct.comp_type_id
                       where s.company_id = c.company_id
                         and (st.status_type_mn = 'МХК' or ct.comp_type_short_name in ('МФО', 'МО'))
                     ) then '2'
                   when c.is_resident = 'N' then '2'
                   when c.is_resident = 'Y' then '1'
                 end [status],
                 c.company_id
        ) as base_resident_status
        outer apply (
          select case
                   when exists (
                       select s.company_id
                       from statuses s
                       join companies c1 on s.company_id = c1.company_id
                       join status_types st on s.status_type_id = st.status_type_id
                         and st.status_type_mn = 'МХК'
                       where s.company_id = c.company_id
                     ) then '996'
                   when exists (
                       select s.company_id
                       from statuses s
                       join companies c1 on s.company_id = c1.company_id
                       join company_types ct on c1.comp_type_id = ct.comp_type_id
                         and ct.comp_type_short_name in ('МФО', 'МО')
                       where s.company_id = c.company_id
                     ) then '998'
                   else (
                       select replicate('0', 3 - len(c1.digital_code)) + cast(c1.digital_code as varchar) [digital_code]
                       from countries c1
                       where c.country_code = c1.country_code
                     )
                 end [code],
                 c.company_id
        ) as oksm_codes
        outer apply (
          select case
                   when (
                       select count(*)
                       from (
                         select distinct i.security_id,
                                         i.state_reg_number
                         from issues i
                         where i.security_id = xs_base.object_id
                       ) bi
                     ) = 1 then (
                       select top 1 state_reg_number
                       from issues i
                       where i.security_id = xs_base.object_id
                     )
                   else '0'
                 end as state_reg_number,
                 xs_base.object_id
        ) as state_reg_numbers
        outer apply (
          select distinct cc.cmp_code,
                          cc.company_id
          from cmp_codes cc
          join cmp_code_types cct on cc.cmp_code_type_id = cct.cmp_code_type_id
            and cct.cmp_code_type_mn = 'LEI'
          left join lei_code_history lch on cc.cmp_code_id = lch.cmp_code_id
            and lch.lei_code_state != 'DUPLICATE'
          where cc.status = 'A'
            and cc.company_id = c.company_id
        ) lei_codes
        where xs.ndc_code in ('{String.Join("', '", drNdcCodes)}')
        ", null).ToList();

            return issues;
        }
    }
}
