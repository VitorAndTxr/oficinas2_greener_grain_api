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
    public class ServiceDeskOfficerRepository : RepositoryBase<ServiceDeskOfficer>, IServiceDeskOfficerRepository
    {
        public ServiceDeskOfficerRepository(IDbFactory dbFactory, IApiContext apiContext)
                    : base(dbFactory, apiContext)
        {
        }

        public async Task<List<ServiceDeskOfficer>> GetByOfficerIdAsync(Guid officerId) 
        {
            var result = await GetAsync(x => x.OfficerId == officerId,null, includeProperties: "ServiceDesk");
            
            return result.ToList();
        }

        public async Task<ServiceDeskOfficer> GetOfficerByIdAndServiceDeskAsync(Guid officerId, Guid serviceDeskId)
        {
            var result = await GetAsync(x => x.OfficerId == officerId && x.ServiceDeskId == serviceDeskId);

            return result.FirstOrDefault();
        }



    }

}