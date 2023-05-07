using GreenerGrain.Domain.Payloads;
using GreenerGrain.Domain.ViewModels;
using System;

namespace GreenerGrain.Service.Interfaces
{
    public interface IAccountService
    {

        AuthorizationViewModel Authorization(AuthorizationPayload payload);

        AuthorizationViewModel RefreshToken();

    }
}