using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Nsd.Service.CorpDb.Services.Bpm;
using Nsd.Service.CorpDb.Services.Bpm.Model;
using Nsd.Web.Api.CorpDb.Base;
using Nsd.Web.Api.CorpDb.Controllers.Proxies.Bpm.Model;

namespace Nsd.Web.Api.CorpDb.Controllers.Proxies.Bpm
{
    [Route("proxies/bpm/[controller]")]
    [Authorize]
    public class SbpRegistrationController : ProxyApiController
    {
        private readonly ISbpRegistrationService _sbpRegistration;
        private readonly IMapper _mapper;

        public SbpRegistrationController(ISbpRegistrationService sbpRegistrationInfoService, IMapper mapper)
        {
            _sbpRegistration = sbpRegistrationInfoService;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Запрос в IPSGATE на регистрацию компании
        /// </summary>
        /// <param name="sbpRegistrationCompanyInfoModel"></param>
        /// <returns></returns>
        [HttpPost("register-new-company")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterNewCompany([FromBody] SbpRegistrationCompanyInfoModel sbpRegistrationCompanyInfoModel)
        {
            var response = _sbpRegistration.RegisterNewCompany(_mapper.Map<DocRegSbpCmpRegInfo>(sbpRegistrationCompanyInfoModel));

            if (response.IsSuccessful)
                return Ok(sbpRegistrationCompanyInfoModel);
            return NotFound(new ProblemDetails
            {
                Detail = response.StatusDescription,
                Title = response.Content
            });
        }

        /// <summary>
        /// Запрос в IPSGATE на регистрацию счета
        /// </summary>
        /// <param name="sbpRegistrationAccountInfoModel"></param>
        /// <returns></returns>
        [HttpPost("register-new-account")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterNewAccount([FromBody] SbpRegistrationAccountInfoModel sbpRegistrationAccountInfoModel)
        {
            var response = _sbpRegistration.RegisterNewAccount(_mapper.Map<DocRegSbpAccountCmpRegInfo>(sbpRegistrationAccountInfoModel));

            if (response.IsSuccessful)
                return Ok(sbpRegistrationAccountInfoModel);
            return NotFound(new ProblemDetails
            {
                Detail = response.StatusDescription,
                Title = response.Content
            });
        }

        /// <summary>
        /// Запрос в IPSGATE на регистрацию ТСП
        /// </summary>
        /// <param name="sbpRegistrationAccountInfoModel"></param>
        /// <returns></returns>
        [HttpPost("register-new-tcp")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterNewTcp([FromBody] SbpRegistrationTcpInfoModel sbpRegistrationAccountInfoModel)
        {
            var response = _sbpRegistration.RegisterNewTcp(_mapper.Map<DocRegSbpTcpRegInfo>(sbpRegistrationAccountInfoModel));

            if (response.IsSuccessful)
                return Ok(sbpRegistrationAccountInfoModel);
            return NotFound(new ProblemDetails
            {
                Detail = response.StatusDescription,
                Title = response.Content
            });
        }
    }
}


