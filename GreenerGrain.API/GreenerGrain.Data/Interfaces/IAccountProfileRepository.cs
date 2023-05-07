using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenerGrain.Data.Interfaces
{
    public interface IAccountProfileRepository : IRepositoryBase<AccountProfile>
    {
        Task<IList<AccountProfile>> GetAccounProfiles(Guid accountId);
        Task<IList<AccountProfile>> GetByProfileId(Guid profileId);
    }
}