using API.Framework.Interfaces;
using GreenerGrain.Framework.Database.EfCore.Factory;
using GreenerGrain.Framework.Database.EfCore.Repository;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GreenerGrain.Data.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {

        public AccountRepository(IDbFactory dbFactory, IApiContext apiContext)
                   : base(dbFactory, apiContext)
        {
        }

        public async Task<Account> GetByLogin(string login)
        {
            var result = await GetAsync(x => x.Login == login, 
                includeProperties: "Institution,Provider");

            return result.FirstOrDefault();
        }

        public async Task<Account> GetByLoginProvider(string login, Guid providerId)
        {
            var result = await GetAsync(x => x.Login == login && x.ProviderId == providerId, 
                includeProperties: "Institution,Provider");

            return result.FirstOrDefault();            
        }        
    }
}