using API.Framework.Interfaces;
using GreenerGrain.Framework.Database.EfCore.Factory;
using GreenerGrain.Framework.Database.EfCore.Repository;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenerGrain.Data.Repositories
{
    public class AccountProfileRepository : RepositoryBase<AccountProfile>, IAccountProfileRepository
    {
        public AccountProfileRepository(IDbFactory dbFactory, IApiContext apiContext)
                   : base(dbFactory, apiContext)
        {
        }     

        public async Task<IList<AccountProfile>> GetAccounProfiles(Guid accountId) 
        {
            var result = await GetAsync(x => x.AccountId == accountId, includeProperties: "Profile");

            return result.ToList();
        }

        public async Task<IList<AccountProfile>> GetByProfileId(Guid profileId)
        {
            var result = await GetAsync(x => x.ProfileId == profileId, includeProperties: "Profile");

            return result.ToList();
        }

    }
}