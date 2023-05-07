using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenerGrain.Data.Interfaces
{
    public interface IServiceDeskOpeningHoursRepository : IRepositoryBase<ServiceDeskOpeningHours>
    {
        Task<IList<ServiceDeskOpeningHours>> GetByServiceDeskAndDayOfWeek(Guid serviceDeskId, DayOfWeek dayOfWeek);
    }    
}
