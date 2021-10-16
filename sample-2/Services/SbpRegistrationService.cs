using System;
using AutoMapper;
using Newtonsoft.Json;
using Nsd.Repository.Ef.Repositories.ControlUtils;
using Nsd.Service.Base;
using Nsd.Service.CorpDb.Services.Bpm.Model;
using RestSharp;

namespace Nsd.Service.CorpDb.Services.Bpm
{
    class SbpRegistrationService : BaseService, ISbpRegistrationService
    {
        private readonly IMapper _mapper;


        private readonly IRestClient _client;

        //Методы для запросов
        private const string ApiVersion = "api/v1/";
        private const string RegisterNewCompanyMethod = ApiVersion + "RegisterNewCompany"; //Регистрация компании
        private const string RegisterNewTcpMethod = ApiVersion + "RegisterNewMerchant"; //Регистрация ТСП
        private const string RegisterNewAccountMethod = ApiVersion + "RegisterNewAccount"; //Регистрация счета

        public SbpRegistrationService(IControlRepository controlRepository, IMapper mapper, BaseServiceContext context) : base(context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            var baseUrl = new Uri(controlRepository.GetAppParameter("WS", "IPSGATE_WS_SBP").Varvalue);
            _client = new RestClient(baseUrl);
        }


        public IRestResponse RegisterNewCompany(DocRegSbpCmpRegInfo cmpInfo)
        {
            var response = Request(RegisterNewCompanyMethod, cmpInfo);

            return response;
        }
        public IRestResponse RegisterNewAccount(DocRegSbpAccountCmpRegInfo accInfo)
        {
            var response = Request(RegisterNewAccountMethod, accInfo);

            return response;
        }       
        
        public IRestResponse RegisterNewTcp(DocRegSbpTcpRegInfo tcpInfo)
        {
            var response = Request(RegisterNewTcpMethod, tcpInfo);

            return response;
        }


        /// <summary>
        /// Осуществляет запрос к сервису IPSGate.
        /// </summary>
        /// <typeparam name="T">Класс параметров</typeparam>
        /// <param name="method">Имя метода</param>
        /// <param name="postParams">Параметры запроса</param>
        /// <returns></returns>
        private IRestResponse Request<T>(string method, T postParams)
            where T : class
        {
            IRestRequest request = new RestRequest(method, Method.POST);

            var jsonParams = JsonConvert.SerializeObject(postParams);
            request.AddJsonBody(jsonParams);

            var response = _client.Post(request);
            return response;
        }

    }
}
