using API.Framework.Interfaces;
using AutoMapper;
using GreenerGrain.Framework.Services;
using GreenerGrain.Domain.Payloads;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GreenerGrain.Service.Services
{
    public class RecaptchaService : ServiceBase, IRecaptchaService
    {
        private readonly IApiContext _apiContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public RecaptchaService(
            IApiContext apiContext
            , IMapper mapper
            , IConfiguration configuration)
            : base(apiContext)
        {
            _apiContext = apiContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public ReCaptchaViewModel Verify(ReCaptchaPayload payload)
        {
            var reCaptchaSecretKey = _configuration.GetValue<string>("Recaptcha:SecretKey");

            var dictionary = new Dictionary<string, string>
            {
                { "secret",  reCaptchaSecretKey },
                { "response", payload.Token }
            };

            var postContent = new FormUrlEncodedContent(dictionary);
            HttpResponseMessage recaptchaResponse = null;
            string stringContent = "";

            // Call recaptcha api and validate the token
            using (var http = new HttpClient())
            {
                recaptchaResponse = http.PostAsync("https://www.google.com/recaptcha/api/siteverify", postContent).Result;
                stringContent = recaptchaResponse.Content.ReadAsStringAsync().Result;
            }

            if (!recaptchaResponse.IsSuccessStatusCode)
            {
                return new ReCaptchaViewModel() { Success = false, ErrorCodes = new string[] { "S03" } };
            }

            if (string.IsNullOrEmpty(stringContent))
            {
                return new ReCaptchaViewModel() { Success = false, ErrorCodes = new string[] { "S04" } };
            }

            var googleReCaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaViewModel>(stringContent);


            if (!googleReCaptchaResponse.Success)
            {
                var errors = string.Join(",", googleReCaptchaResponse.ErrorCodes);
                return new ReCaptchaViewModel() { Success = false, ErrorCodes = new string[] { errors } };
            }

            //if (!googleReCaptchaResponse.Action.Equals("enterQueue", StringComparison.OrdinalIgnoreCase))
            //{
            //    // This is important just to verify that the exact action has been performed from the UI
            //    return new ReCaptchaViewModel() { Success = false, ErrorCodes = new string[] { "S06" } };
            //}

            //// Captcha was success , let's check the score, in our case, for example, anything less than 0.5
            //// is considered as a bot user which we would not allow ...
            //// the passing score might be higher or lower according to the sensitivity of your action

            //if (googleReCaptchaResponse.Score < 0.5)
            //{
            //    return new ReCaptchaViewModel() { Success = false, ErrorCodes = new string[] { "S07" } };
            //}

            //TODO: Continue with doing the actual signup process, since now we know the request was done by potentially really human
            
            return googleReCaptchaResponse;

        }

    }
}
