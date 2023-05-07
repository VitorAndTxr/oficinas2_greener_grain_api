using API.Framework.Interfaces;
using GreenerGrain.Framework.Database.EfCore.Factory;
using GreenerGrain.Framework.Database.EfCore.Repository;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Entities;

namespace GreenerGrain.Data.Repositories
{
    public class OfficerPauseRepository : RepositoryBase<OfficerPause>, IOfficerPauseRepository
    {
        public OfficerPauseRepository(IDbFactory dbFactory, IApiContext apiContext)
                    : base(dbFactory, apiContext)
        {
        }
    }

}