using GreenerGrain.Domain.ViewModels;
using System;

namespace GreenerGrain.Service.Interfaces
{
    public interface IProviderService
    {
        ProviderViewModel GetByCode(string code);
        ProviderViewModel GetById(Guid id);
    }
}
