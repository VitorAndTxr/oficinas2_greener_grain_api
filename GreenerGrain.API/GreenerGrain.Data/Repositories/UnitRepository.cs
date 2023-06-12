using API.Framework.Interfaces;
using GreenerGrain.Framework.Database.EfCore.Factory;
using GreenerGrain.Framework.Database.EfCore.Repository;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Entities;
using System.Threading.Tasks;
using System.Linq;

namespace GreenerGrain.Data.Repositories
{
    public class UnitRepository : RepositoryBase<Unit>, IUnitRepository
    {
        public UnitRepository(IDbFactory dbFactory, IApiContext apiContext)
                   : base(dbFactory, apiContext)
        {
        }

        public async Task<Unit> GetByUnitCode(string code)
        {
            var result = await GetAsync(x => x.Code == code,
                includeProperties: "Modules.Grain");

            return result.FirstOrDefault();
        }
    }
}