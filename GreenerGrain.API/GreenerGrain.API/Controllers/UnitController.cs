using API.Framework.Interfaces;
using GreenerGrain.Framework.Controllers;
using GreenerGrain.Framework.Result;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GreenerGrain.Domain.Payloads;

namespace Google.API.Account.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UnitController : ApiBaseController
    {
        #region Fields

        /// <summary>
        /// Referencia interna ao serviço 
        /// </summary>
        private readonly IUnitService _unitService = null;

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor
        /// </summary>

        public UnitController(IApiContext apiContext, IUnitService unitService) : base(apiContext)
        {
            _unitService = unitService;
        }

        #endregion
        #region Controller Methods

        /// <summary>
        /// Realiza a autorização (login) de uma conta de usuárioo
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [Route("{code}")]
        [HttpPost]
        [ProducesDefaultResponseType(typeof(ApiResponse<UnitViewModel>))]
        public IActionResult Authorization(string code)
        {
            var response = this.ServiceInvoke(_unitService.GetByUnitCode, code);
            return response;
        }

        [Route("Verify")]
        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<OkResult>))]
        public IActionResult VerifyUnits()
        {
            _unitService.VerifyStatus();
            return StatusCode(200);
        }

        [Route("Alive")]
        [HttpPost]
        [ProducesDefaultResponseType(typeof(ApiResponse<OkResult>))]
        public IActionResult VerifyUnits([FromBody] UnitAlivePayload payload )
        {
            
            var response = this.ServiceInvoke(_unitService.UnitAlive, payload);

            return StatusCode(200);
        }
        #endregion

    }
}