using AutoMapper;
using Nsd.Service.CorpDb.Services.Bpm.Model;

namespace Nsd.Service.CorpDb.Services.Bpm.Mapping
{
    public class SbpRegistrationProfile : Profile
    {
        public SbpRegistrationProfile()
        {
            CreateMap<DocRegSbpCmpRegInfo, SbpRegistrationCompanyServiceModel>();
            CreateMap<DocRegSbpAccountCmpRegInfo, SbpRegistrationAccountServiceModel>();
            CreateMap<DocRegSbpTcpRegInfo, SbpRegistrationTcpServiceModel>();
        }
    }
}
