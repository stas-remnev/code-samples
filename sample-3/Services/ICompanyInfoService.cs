using Nsd.Service.CorpDb.Services.Companies.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nsd.Service.CorpDb.Services.Companies
{
    public interface ICompanyInfoService
    {
        Task<CompanyFullInfoServiceModel> GetInfo(string ndcCode);

        Task<List<CompanyBySearchStringInfoServiceModel>> GetListOfCompanyInfoByStrictSearch(string search, int limit);
        Task<List<CompanyBySearchStringInfoServiceModel>> GetListOfCompanyInfoBySearchString(string search, int limit);

        Task<CompanyBaseIssuerInfoServiceModel> GetBaseIssuerInfo(string drNdcCode);
        Task<List<CompanyBaseIssuerInfoServiceModel>> GetBaseIssuerInfo(string[] drNdcCode);
        Task<List<CompanyBaseIssuerInfoServiceModel>> GetAllBaseIssuersInfo();
        Task<IEnumerable<CompaniesWithActiveStatusServiceModel>> GetCompaniesWithActiveStatusAsync(int statusId, DateTime dateTime);
        Task<List<CompanyFullInfoServiceModel>> GetInfos(string[] ndcCode);
    }
}
