using GreenerGrain.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace GreenerGrain.Service.Interfaces
{
    public interface IProfileService 
    {        
        IList<ProfileViewModel> GetProfilesByAccountIdApplicationId(Guid accountId);
    }
}