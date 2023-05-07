using API.Framework.Interfaces;
using GreenerGrain.Framework.Database.EfCore.Factory;
using GreenerGrain.Framework.Database.EfCore.Repository;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GreenerGrain.Data.Repositories
{
    public class ProviderRepository : RepositoryBase<Provider>, IProviderRepository
    {
        public ProviderRepository(IDbFactory dbFactory, IApiContext apiContext)
                    : base(dbFactory, apiContext)
        {
        }
        
        public async Task<Provider> GetByCode(string code)
        {
            var result = await GetAsync(x => x.Code == code);

            return result.FirstOrDefault();
        }

    }
}
