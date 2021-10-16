using AutoMapper;
using Nsd.Service.CorpDb.Services.Bpm.Model;
using Nsd.Web.Api.CorpDb.Controllers.Proxies.Bpm.Model;

namespace Nsd.Web.Api.CorpDb.Controllers.Proxies.Bpm.Mapping
{
    public class SbpRegistrationProfile : Profile
    {
        public SbpRegistrationProfile()
        {
            CreateMap<SbpRegistrationCompanyServiceModel, SbpRegistrationCompanyInfoModel>();
            CreateMap<SbpRegistrationAccountServiceModel, SbpRegistrationAccountInfoModel>();
            CreateMap<SbpRegistrationTcpServiceModel, SbpRegistrationTcpInfoModel>();

            CreateMap<SbpRegistrationTcpInfoModel, DocRegSbpTcpRegInfo>();
            CreateMap<SbpRegistrationAccountInfoModel, DocRegSbpAccountCmpRegInfo>();
            CreateMap<SbpRegistrationCompanyInfoModel, DocRegSbpCmpRegInfo>();
        }
    }
}
