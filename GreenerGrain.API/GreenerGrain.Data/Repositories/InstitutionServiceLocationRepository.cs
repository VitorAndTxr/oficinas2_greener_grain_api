using API.Framework.Interfaces;
using GreenerGrain.Framework.Database.EfCore.Factory;
using GreenerGrain.Framework.Database.EfCore.Repository;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Entities;

namespace GreenerGrain.Data.Repositories
{
    public class InstitutionServiceLocationRepository : RepositoryBase<InstitutionServiceLocation>, IInstitutionServiceLocationRepository
    {
        public InstitutionServiceLocationRepository(IDbFactory dbFactory, IApiContext apiContext)
                    : base(dbFactory, apiContext)
        {
        }
    }

}