using API.Framework.Interfaces;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Framework.Controllers;
using GreenerGrain.Framework.Result;
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

        #endregion

        #region Construtor
        /// <summary>
        /// Construtor
        /// </summary>
        public HealthController(IApiContext apiContext) : base(apiContext)
        {
        }

        #endregion

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<AuthorizationViewModel>))]

        public IActionResult Get()
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
    }

    public class HealthResponseViewModel
    {
        public string message { get; set; }
        public int code { get; set; }
    }
   
}
