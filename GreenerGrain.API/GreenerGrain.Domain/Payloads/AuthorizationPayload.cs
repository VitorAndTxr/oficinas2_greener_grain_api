using System;

namespace GreenerGrain.Domain.Payloads
{
    public class AuthorizationPayload
    {
        public string Login { get; set; }
        public string AccessToken { get; set; }
        public string Password { get; set; }

    }
}