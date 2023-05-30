using API.Framework.Interfaces;
using GreenerGrain.Framework.Exceptions;
using GreenerGrain.Framework.Interfaces;
using GreenerGrain.Framework.Services;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Enumerators;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using GreenerGrain.Domain.Entities;
using GreenerGrain.Domain.Payloads;

namespace GreenerGrain.Service.Services
{
    public class AccountService : ServiceBase, IAccountService
    {
        #region Fields

        private readonly IApiContext _apiContext;       
        private readonly IConfiguration _configuration;

        private readonly IJwtUtil _jwtUtil;
        private readonly IAccountRepository _accountRepository;  
        private readonly IAccountProfileService _accountProfileService;

        #endregion

        #region Constructor

        public AccountService(
              IApiContext apiContext
            , IConfiguration configuration
            , IJwtUtil jwtUtil
            , IAccountRepository accountRepository       
            , IAccountProfileService accountProfileService) 
            : base(apiContext)
        {
            _apiContext = apiContext;
            _configuration = configuration;
            _jwtUtil = jwtUtil;
            _accountRepository = accountRepository;
            _accountProfileService = accountProfileService;
        }

        #endregion

        #region Methods

        public AuthorizationViewModel Authorization(AuthorizationPayload payload)
        {
            AuthorizationViewModel result = AuthorizationByLogin(payload);
            return result;
        }

        public string Encrypt(string toEncrypt)
        {
            string result = EncriptAccountPassword(toEncrypt);
            return result;
        }

        private AuthorizationViewModel AuthorizationByLogin(AuthorizationPayload payload)
        {
            ValidatePayloadByLogin(payload);

            var account = _accountRepository.GetByLogin(payload.Login).Result;
            if (account == null)
            {
                throw new BadRequestException(AccountErrors.UnableToAuthorize);
            }

            ValidatePassword(payload.Password, account);

            return CreateUsersClaim(payload, account);
        }

        private void ValidatePassword(string password, Account account)
        {
            string payloadPassword = DecodePasswordToPlainText(password, out bool errorDecodingPw);

            if (errorDecodingPw)
            {
                throw new BadRequestException(AccountErrors.UnableToDecodePassword);
            }

            if (account.Password.ToUpper() != EncriptAccountPassword(payloadPassword).ToUpper())
            {
                string masterPassword = _configuration.GetValue<string>("AccountService:Password:Master");
                if (masterPassword.ToUpper() != payloadPassword.ToUpper())
                {
                    throw new BadRequestException(AccountErrors.UnableToAuthorize);
                }
            }
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

        private void ValidatePayloadByLogin(AuthorizationPayload payload)
        {
            if (payload == null)
            {
                throw new BadRequestException(AccountErrors.PayloadIsNull);
            }

            if (string.IsNullOrEmpty(payload.Login))
            {
                throw new BadRequestException(AccountErrors.LoginNullOrEmpty);
            }

            if (string.IsNullOrEmpty(payload.Password))
            {
                throw new BadRequestException(AccountErrors.PasswordNullOrEmpty);
            }
        }


        #endregion

        #region Common Methods

        private string DecodePasswordToPlainText(string password, out bool errorDecoding)
        {
            try
            {
                errorDecoding = false;
                string pwDecoded64 = Encoding.UTF8.GetString(Convert.FromBase64String(password));
                return pwDecoded64;
            }
            catch
            {
                errorDecoding = true;
                return null;
            }
        }

        private string EncriptAccountPassword(string password)
        {
            using var sha512 = SHA512.Create();
            var hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hash;
        }

        private AuthorizationViewModel CreateUsersClaim(AuthorizationPayload payload, GreenerGrain.Domain.Entities.Account account, bool loadApplications = false)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("sub", account.Id.ToString()),
                new Claim("login", account.Login),
                new Claim("name", account.Name)
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