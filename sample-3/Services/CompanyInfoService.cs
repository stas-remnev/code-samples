using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nsd.Common.Extensions;
using Nsd.Repository.Base;
using Nsd.Repository.Ef;
using Nsd.Repository.Ef.Model.Entities;
using Nsd.Service.Base;
using Nsd.Service.CorpDb.Services.Companies.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nsd.Repository.Ef.Repositories.XSbr.Extensions;
using CompaniesRep = Nsd.Repository.Ef.Model.Entities.Companies;

namespace Nsd.Service.CorpDb.Services.Companies
{
    public class CompanyInfoService : BaseService, ICompanyInfoService
    {
        private readonly IReadOnlyRepository<CompaniesRep> _companiesRepository;
        private readonly IRepository<XSbr> _xSbrRepository;
        private readonly IReadOnlyRepository<CmpCodeTypes> _cmpCodeTypesRepository;
        private readonly IReadOnlyRepository<CmpCodes> _cmpCodesRepository;
        private readonly IReadOnlyRepository<StatusHistory> _cmpStatusHistoryRepository;
        private readonly IMapper _mapper;

        private const string IssuerCodeMn = "FCSM";

        public CompanyInfoService(
            IReadOnlyRepository<CompaniesRep> companiesRepository,
            IReadOnlyRepository<CmpCodeTypes> cmpCodeTypesRepository,
            IReadOnlyRepository<CmpCodes> cmpCodesRepository,
            IReadOnlyRepository<StatusHistory> cmpStatusHistoryRepository,
            IMapper mapper,
            BaseServiceContext context, IRepository<XSbr> xSbrRepository) : base(context)
        {
            _companiesRepository = companiesRepository;
            _cmpCodeTypesRepository = cmpCodeTypesRepository;
            _cmpCodesRepository = cmpCodesRepository;
            _cmpStatusHistoryRepository = cmpStatusHistoryRepository;
            _mapper = mapper;
            _xSbrRepository = xSbrRepository;
        }

        public async Task<CompanyFullInfoServiceModel> GetInfo(string ndcCode)
        {
            CompanyFullInfoServiceModel cmpInfo = null;
            var cmp = await _companiesRepository.Get(x => x.NdcCmpCode == ndcCode).FirstOrDefaultAsync();
            if (cmp != null)
            {
                cmpInfo = new CompanyFullInfoServiceModel
                {
                    NdcCode = cmp.NdcCmpCode,
                    IssuerCode = await GetIssuerCode(cmp.CompanyId),
                    ForeignEconomySectorCode = cmp.ForeignEconomySectorCode
                };
            }

            return cmpInfo;
        }

        public async Task<List<CompanyBySearchStringInfoServiceModel>> GetListOfCompanyInfoByStrictSearch(string search, int limit)
        {
            var cmp = await _companiesRepository.Get(x => x.CommonName == search || x.NdcCmpCode == search).Take(limit).ToListAsync();

            return _mapper.Map<List<CompanyBySearchStringInfoServiceModel>>(cmp);
        }

        public async Task<List<CompanyBySearchStringInfoServiceModel>> GetListOfCompanyInfoBySearchString(string search, int limit)
        {
            var cmp = await _companiesRepository.Get(x => x.CommonName.Contains(search) || x.NdcCmpCode.Contains(search)).Take(limit).ToListAsync();

            return _mapper.Map<List<CompanyBySearchStringInfoServiceModel>>(cmp);
        }
        

        public async Task<CompanyBaseIssuerInfoServiceModel> GetBaseIssuerInfo(string drNdcCode)
        {
            return (await GetDrBaseIssuer(new[] { drNdcCode })).FirstOrDefault();
        }

        public async Task<List<CompanyBaseIssuerInfoServiceModel>> GetBaseIssuerInfo(string[] drNdcCode)
        {
            return await GetDrBaseIssuer(drNdcCode);
        }

        public async Task<List<CompanyBaseIssuerInfoServiceModel>> GetAllBaseIssuersInfo()
        {
            var depReceiptNdcCodes = await _xSbrRepository.Get(x => x.ObjectType == "D").Select(a => a.NdcCode).ToArrayAsync();

            var cmpInfos = new List<CompanyBaseIssuerInfoServiceModel>();

            if (depReceiptNdcCodes != null && depReceiptNdcCodes.Length > 0)
            {
                return await GetDrBaseIssuer(depReceiptNdcCodes);
            }

            return cmpInfos;
        }

        private async Task<List<CompanyBaseIssuerInfoServiceModel>> GetDrBaseIssuer(string[] drNdcCodes)
        {
            var infos =
                _xSbrRepository.GetCompanyBaseIssuerInfo(drNdcCodes);

            return _mapper.Map<List<CompanyBaseIssuerInfoServiceModel>>(infos);

        }


        public async Task<List<CompanyFullInfoServiceModel>> GetInfos(string[] ndcCodes)
        {
            var cmpInfos = new List<CompanyFullInfoServiceModel>();
            var cmps = await _companiesRepository.Get(x => ndcCodes.Contains(x.NdcCmpCode)).ToListAsync();
            if (cmps != null)
            {
                foreach (var cmp in cmps)
                {
                    var cmpInfo = new CompanyFullInfoServiceModel
                    {
                        NdcCode = cmp.NdcCmpCode,
                        IssuerCode = await GetIssuerCode(cmp.CompanyId),
                        ForeignEconomySectorCode = cmp.ForeignEconomySectorCode
                    };
                    cmpInfos.Add(cmpInfo);
                }
            }

            return cmpInfos;
        }

        public async Task<IEnumerable<CompaniesWithActiveStatusServiceModel>> GetCompaniesWithActiveStatusAsync(int statusId, DateTime dateTime)
        {
            var companies = _companiesRepository.Get();
            var companyStatusHistory = _cmpStatusHistoryRepository.Get();
            var cmpNdcCodes = _cmpCodesRepository.Get(c => c.Status == "A" && c.CmpCodeType.CmpCodeTypeMn == "NDC");

            return (await companies
                .Where(cmp =>
                        companyStatusHistory
                            .Where(h => h.CompanyId == cmp.CompanyId && h.OperDate.Date <= dateTime.Date && h.StatusTypeId == statusId)
                            .OrderByDescending(x => x.OperDate)
                            .Select(x => x.OperType)
                            .FirstOrDefault() == "A"
                        &&
                        cmpNdcCodes.Any(code => code.CompanyId == cmp.CompanyId))
                .ToListAsync())
                .Select(_mapper.Map<CompaniesWithActiveStatusServiceModel>);
        }

        #region private
        private async Task<string> GetIssuerCode(int companyId)
        {
            string code = null;
            var cmpCodeType = await _cmpCodeTypesRepository.Get(c => c.CmpCodeTypeMn == IssuerCodeMn).FirstOrDefaultAsync();
            if (cmpCodeType != null)
            {
                var cmpCode = await _cmpCodesRepository
                    .Get(c => c.CompanyId == companyId && c.CmpCodeTypeId == cmpCodeType.CmpCodeTypeId)
                    .FirstOrDefaultAsync();

                code = !cmpCode.IsNullOrWhitespace() ? cmpCode.CmpCode : null;
            }

            return code;
        }
        #endregion
    }
}
