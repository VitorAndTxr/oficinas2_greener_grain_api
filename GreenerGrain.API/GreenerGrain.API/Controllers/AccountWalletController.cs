using API.Framework.Interfaces;
using GreenerGrain.Framework.Controllers;
using GreenerGrain.Framework.Result;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GreenerGrain.Framework.Security.Authorization;

namespace Google.API.Account.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountWalletController : ApiBaseController
    {
        #region Fields

        /// <summary>
        /// Referencia interna ao serviço 
        /// </summary>
        private readonly IAccountWalletService _accountWalletService = null;

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor
        /// </summary>

        public AccountWalletController(IApiContext apiContext, IAccountWalletService accountWalletService) : base(apiContext)
        {
            _accountWalletService = accountWalletService;
        }

        #endregion
        #region Controller Methods

        /// <summary>
        /// Realiza a autorização (login) de uma conta de usuárioo
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [Route("")]
        [Authorize("sub")]
        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<AccountWalletViewModel>))]
        public IActionResult GetAccountWallet()
        {
            var response = this.ServiceInvoke(_accountWalletService.GetByUserWallet);
            return response;
        }
        #endregion

    }
}