using API.Framework.Interfaces;
using GreenerGrain.Framework.Exceptions;
using GreenerGrain.Framework.Interfaces;
using GreenerGrain.Framework.Services;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Enumerators;
using GreenerGrain.Domain.Payloads;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using Google.Apis.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GreenerGrain.Service.Services
{
    public class AccountService : ServiceBase, IAccountService
    {
        #region Fields

        private readonly IApiContext _apiContext;        
        private readonly IJwtUtil _jwtUtil;
        private readonly IAccountRepository _accountRepository;  
        private readonly IAccountProfileService _accountProfileService;

        #endregion

        #region Constructor

        public AccountService(
              IApiContext apiContext            
            , IJwtUtil jwtUtil
            , IAccountRepository accountRepository       
            , IAccountProfileService accountProfileService) 
            : base(apiContext)
        {
            _apiContext = apiContext;
            _jwtUtil = jwtUtil;
            _accountRepository = accountRepository;
            _accountProfileService = accountProfileService;
        }

        #endregion

        #region Methods

        public AuthorizationViewModel Authorization(AuthorizationPayload payload)
        {
            AuthorizationViewModel result = AuthorizationByGoogle(payload);
            return result;
        }

        public AuthorizationViewModel RefreshToken()
        {
            string sub = _apiContext.SecurityContext.Account.Id.ToString();
            string login = _apiContext.SecurityContext.Account.Login;

            if (string.IsNullOrEmpty(sub) || string.IsNullOrEmpty(login))
            {
                throw new BadRequestException(AccountErrors.PayloadIsNull);
            }

            AuthorizationPayload payload = new AuthorizationPayload
            {
                Login = login
            };

            return AuthorizationByRefreshToken(payload);
        }

        #endregion

        #region Private Methods

        private AuthorizationViewModel AuthorizationByRefreshToken(AuthorizationPayload payload)
        {
            var account = Task.Run(() => _accountRepository.GetByLogin(payload.Login)).Result;
            if (account == null)
            {
                throw new BadRequestException(AccountErrors.UnableToAuthorize);
            }

            return CreateUsersClaim(payload, account, true);
        }

        private AuthorizationViewModel AuthorizationByGoogle(AuthorizationPayload payload)
        {
            ValidatePayloadByGoogle(payload);

            GoogleJsonWebSignature.Payload decodedToken = GoogleJsonWebSignature.ValidateAsync(payload.AccessToken).Result;

            if (decodedToken.Email != payload.Login)
            {
                throw new BadRequestException(AccountErrors.GoogleUserDoestMachToken);
            }

            var account = Task.Run(() => _accountRepository.GetByLogin(decodedToken.Email)).Result;

            if (account == null)
            {
                throw new BadRequestException(AccountErrors.UnableToAuthorize);
            }

            return CreateUsersClaim(payload, account);
        }          

        private void ValidatePayloadByGoogle(AuthorizationPayload payload)
        {
            if (payload.AccessToken == null)
            {
                throw new BadRequestException(AccountErrors.GoogleTokenEmpty);
            }
        }      

        #endregion

        #region Common Methods
        
        private AuthorizationViewModel CreateUsersClaim(AuthorizationPayload payload, GreenerGrain.Domain.Entities.Account account, bool loadApplications = false)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("sub", account.Id.ToString()),
                new Claim("login", account.Login),
                new Claim("name", account.Name),
                new Claim("institutionId", account.InstitutionId.ToString()),
                new Claim("providerCode", account.Provider.Code.ToString())
            };

            var userProfiles = _accountProfileService.GetAccountProfiles(account.Id);

            AddProfilesToClaims(claims, userProfiles);

            AuthorizationViewModel authorizationResult = new AuthorizationViewModel
            {
                Token = _jwtUtil.CreateJwt(claims),
                Profiles= userProfiles,
            };

            return authorizationResult;
        }

        private void AddProfilesToClaims(List<Claim> claims, List<ProfileViewModel> profiles)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var applicationSerializable = JsonConvert.SerializeObject(profiles, settings);
            claims.Add(new Claim("profiles", applicationSerializable));
        }

        #endregion
    }
}