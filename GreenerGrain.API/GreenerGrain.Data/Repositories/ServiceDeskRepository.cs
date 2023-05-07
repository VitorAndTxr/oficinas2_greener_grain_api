using API.Framework.Interfaces;
using GreenerGrain.Framework.Database.EfCore.Factory;
using GreenerGrain.Framework.Database.EfCore.Repository;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Entities;
using GreenerGrain.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenerGrain.Data.Repositories
{
    public class ServiceDeskRepository : RepositoryBase<ServiceDesk>, IServiceDeskRepository
    {
        public ServiceDeskRepository(IDbFactory dbFactory, IApiContext apiContext)
                    : base(dbFactory, apiContext)
        {
        }

        public async Task<ServiceDesk> GetByIdAndTypeAsync(Guid serviceDeskId, ServiceDeskTypeEnum serviceDeskType)
        {
            var result = await
            GetAsync(x => x.Id == serviceDeskId && x.ServiceDeskTypeId == serviceDeskType);

            return result.FirstOrDefault();
        }

        public async Task<ServiceDesk> GetByCodeAsync(string code)
        {
            var result = await GetAsync(x => x.Code == code);

            return result.FirstOrDefault();
        }

        public async Task<IList<ServiceDesk>> GetByInstitutionAndServiceTypeAsync(Guid institutionId, ServiceDeskTypeEnum serviceDeskType)
        {
            var result = await GetAsync(x => x.InstitutionId == institutionId && x.ServiceDeskTypeId == serviceDeskType);

            return result.ToList();
        }        

    }
}