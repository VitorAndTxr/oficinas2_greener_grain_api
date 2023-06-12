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
    public class AccountWalletRepository : RepositoryBase<AccountWallet>, IAccountWalletRepository
    {

        public AccountWalletRepository(IDbFactory dbFactory, IApiContext apiContext)
                   : base(dbFactory, apiContext)
        {
        }

        public async Task<AccountWallet> GetByAccountId(Guid accountId)
        {
            var result = await GetAsync(x => x.AccountId == accountId,
                includeProperties: "");

            return result.FirstOrDefault();
        }

    }
}