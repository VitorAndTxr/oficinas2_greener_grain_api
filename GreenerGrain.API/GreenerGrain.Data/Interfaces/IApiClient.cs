using System;

namespace GreenerGrain.Data.Interfaces
{
    public interface IApiClient
    {
        Uri BaseEndpoint { get; set; }

        string Token { get; set; }
    }
}
