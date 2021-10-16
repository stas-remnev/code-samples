using Nsd.Service.CorpDb.Services.Bpm.Model;
using RestSharp;

namespace Nsd.Service.CorpDb.Services.Bpm
{
    public interface ISbpRegistrationService
    {
        IRestResponse RegisterNewCompany(DocRegSbpCmpRegInfo cmpInfo);
        IRestResponse RegisterNewAccount(DocRegSbpAccountCmpRegInfo accInfo);
        IRestResponse RegisterNewTcp(DocRegSbpTcpRegInfo tcpInfo);
    }
}
