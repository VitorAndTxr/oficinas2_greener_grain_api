using API.Framework.Interfaces;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Framework.Controllers;
using GreenerGrain.Framework.Result;
using GreenerGrain.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GreenerGrain.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HealthController : ApiBaseController
    {
        #region Fields

        /// <summary>
        /// Referencia interna ao serviço 
        /// </summary>
        /// 

        private readonly IAccountService _accountService;


        #endregion

        #region Construtor
        /// <summary>
        /// Construtor
        /// </summary>
        public HealthController(IApiContext apiContext, IAccountService accountService) : base(apiContext)
        {
            _accountService = accountService;
        }

        #endregion

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<string>))]

        public IActionResult Get([FromBody]string password)
        {
            var response = this.ServiceInvoke(
            () =>
            {
                return (
                new HealthResponseViewModel()
                {
                    message = "Oi",
                    code = 1
                });
            });
            return response;
        }

        [HttpPost]
        [Route("Encrypt")]
        [ProducesDefaultResponseType(typeof(ApiResponse<AuthorizationViewModel>))]

        public IActionResult Encrypt([FromBody] string password)
        {

            var response = this.ServiceInvoke(_accountService.Encrypt, password);
            return response;
        }
    }

    public class HealthResponseViewModel
    {
        public string message { get; set; }
        public int code { get; set; }
    }
   
}
