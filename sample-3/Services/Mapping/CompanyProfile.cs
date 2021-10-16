using AutoMapper;
using Nsd.Repository.Ef.Repositories.XSbr;
using Nsd.Service.CorpDb.Services.Companies.Model;

namespace Nsd.Service.CorpDb.Services.Companies.Mapping
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Repository.Ef.Model.Entities.Companies, CompaniesWithActiveStatusServiceModel>()
                .ForMember(x => x.NdcCode, o => o.MapFrom(x => x.NdcCmpCode));

            CreateMap<CompanyBaseIssuerInfoModel, CompanyBaseIssuerInfoServiceModel>();
        }
    }
}
