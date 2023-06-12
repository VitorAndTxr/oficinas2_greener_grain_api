using API.Framework.Interfaces;
using GreenerGrain.Framework.Controllers;
using GreenerGrain.Framework.Result;
using GreenerGrain.Domain.Payloads;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GreenerGrain.Framework.Security.Authorization;
using System;
using GreenerGrain.Service.Services;

namespace Google.API.Account.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ApiBaseController
    {
        #region Fields

        /// <summary>
        /// Referencia interna ao serviço 
        /// </summary>
        private readonly IAccountService _accountService = null;

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor
        /// </summary>

        public AccountController(IApiContext apiContext, IAccountService accountService) : base(apiContext)
        {
            _accountService = accountService;
        }

        #endregion

        #region Controller Methods

        /// <summary>
        /// Realiza a autorização (login) de uma conta de usuárioo
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [Route("Authorization")]
        [HttpPost]
        [ProducesDefaultResponseType(typeof(ApiResponse<AuthorizationViewModel>))]
        public IActionResult Authorization(AuthorizationPayload payload)
        {
            var response = this.ServiceInvoke(_accountService.Authorization, payload);
            return response;
        }

        /// <summary>
        /// Efetua o refresh token de autorização de uma conta de usuário e retorna permissões de acesso de uma aplicação específica
        /// </summary>
        [Route("Authorization/RefreshToken")]
        [Authorize("sub")]
        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<AuthorizationViewModel>))]
        public IActionResult RefreshToken()
        {
            var response = this.ServiceInvoke(_accountService.RefreshToken);
            return response;
        }

        #endregion
    }
}