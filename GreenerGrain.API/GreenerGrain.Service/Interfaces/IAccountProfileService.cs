using GreenerGrain.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace GreenerGrain.Service.Interfaces
{
    public interface IAccountProfileService
    {
        List<ProfileViewModel> GetAccountProfiles(Guid accountId);
   }
}