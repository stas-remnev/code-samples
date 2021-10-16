using LinqToDB.Data;
using Nsd.Common.Extensions;
using Nsd.Repository.Ef.Model.Entities;
using System.Linq;

namespace Nsd.Repository.Ef.Repositories.Lei
{
    class LeiXmlTagsValuesRepository : BaseRepository<LeiXmlTagsValues>, ILeiXmlTagsValuesRepository
    {
        public LeiXmlTagsValuesRepository(BaseRepositoryContext context) : base(context)
        {
        }

        public LeiXmlTagsValues LoadByCompanyId(int companyId)
        {
            return Query<LeiXmlTagsValues>(@"select top 1 *
from
(select top 1 
			1 as order_id,
			rec_id,
	        company_id,
	        entity_legal_form_code,
	        other_legal_form,
	        validation_source,
            legal_address,
            head_quarters_address
from lei_xml_tags_values
where company_id = @company_id
union all
select
			2 as order_id,
			0 as rec_id,
	        @company_id as company_id,
	        null as entity_legal_form_code,
	        null as other_legal_form,
	        null as validation_source,
            null as legal_address,
            null as head_quarters_address) as inn
order by order_id", new DataParameter("@company_id", companyId)).FirstOrDefault();
        }

        public void Save(LeiXmlTagsValues leiXmlTagsValues)
        {
            Execute(@"merge lei_xml_tags_values as target
using (select @company_id as company_id,
			  @entity_legal_form_code as entity_legal_form_code,
			  @other_legal_form as other_legal_form,
			  @validation_source as validation_source,
            @legal_address as legal_address,
            @head_quarters_address as head_quarters_address) as source on target.company_id = source.company_id
when not matched by target then 
	insert (company_id, entity_legal_form_code, other_legal_form, validation_source, head_quarters_address, legal_address)
	values (source.company_id, source.entity_legal_form_code, source.other_legal_form, source.validation_source, source.head_quarters_address, source.legal_address)
when matched then
	update set company_id = source.company_id,
			   entity_legal_form_code = source.entity_legal_form_code,
			   other_legal_form = source.other_legal_form,
			   validation_source = source.validation_source,
             head_quarters_address = source.head_quarters_address,
             legal_address = source.legal_address;",new DataParameter("@company_id", leiXmlTagsValues.CompanyId),
                                                    new DataParameter("@entity_legal_form_code", leiXmlTagsValues.EntityLegalFormCode),
                                                    new DataParameter("@other_legal_form", leiXmlTagsValues.OtherLegalForm),
                                                    new DataParameter("@validation_source", leiXmlTagsValues.ValidationSource),
                                                    new DataParameter("@head_quarters_address", leiXmlTagsValues.HeadquartersAddress),
                                                    new DataParameter("@legal_address", leiXmlTagsValues.LegalAddress));
        }

        public void Delete(int recId)
        {
            var lei = Get().Where(x => x.RecId == recId).FirstOrDefault();
            Delete(lei);
        }

        public string GetCompanyValidationSource(int companyId)
        {
            return Query<string>(@"select validation_source from dbo.get_company_validation_source(@company_id)", new DataParameter("@company_id", companyId)).FirstOrDefault();
        }

        public string GetOtherRegistrationAuthorityId(int companyId)
        {
            var otherRegistrationAuthorityId = Query<string>(
@"select other_registration_authority_id
from lei_xml_tags_values
where company_id = @company_id",
                new DataParameter("@company_id", companyId)).FirstOrDefault();

            if (otherRegistrationAuthorityId != null)
            {
                otherRegistrationAuthorityId = otherRegistrationAuthorityId.Trim();

                if (otherRegistrationAuthorityId.IsNullOrWhitespace())
                    otherRegistrationAuthorityId = null;
            }

            return otherRegistrationAuthorityId;
        }

        public string GetMailRouting(int companyId)
        {
            var mailRouting = Query<string>(@"select mail_routing from lei_xml_tags_values where company_id = @company_id", new DataParameter("@company_id", companyId)).FirstOrDefault();

            if (mailRouting != null)
            {
                mailRouting = mailRouting.Trim();

                if (mailRouting.IsNullOrWhitespace())
                    mailRouting = null;
            }

            return mailRouting;
        }
    }
}
