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
    public class ServiceDeskOpeningHoursRepository : RepositoryBase<ServiceDeskOpeningHours>, IServiceDeskOpeningHoursRepository
    {
        public ServiceDeskOpeningHoursRepository(IDbFactory dbFactory, IApiContext apiContext)
                    : base(dbFactory, apiContext)
        {
        }       

        public async Task<IList<ServiceDeskOpeningHours>> GetByServiceDeskAndDayOfWeek(Guid serviceDeskId, DayOfWeek dayOfWeek) 
        {
            var result = await GetAsync(x => x.ServiceDeskId == serviceDeskId && x.DayOfWeek == dayOfWeek);

            return result.ToList();
        }

    }
}