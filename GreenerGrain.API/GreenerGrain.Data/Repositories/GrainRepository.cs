using API.Framework.Interfaces;
using GreenerGrain.Framework.Database.EfCore.Factory;
using GreenerGrain.Framework.Database.EfCore.Repository;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Entities;

namespace GreenerGrain.Data.Repositories
{
    public class GrainRepository : RepositoryBase<Grain>, IGrainRepository
    {
        public GrainRepository(IDbFactory dbFactory, IApiContext apiContext)
                   : base(dbFactory, apiContext)
        {
        }
    }
}