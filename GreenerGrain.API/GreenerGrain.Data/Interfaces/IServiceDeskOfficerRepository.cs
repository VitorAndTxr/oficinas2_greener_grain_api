using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenerGrain.Data.Interfaces
{
    public interface IServiceDeskOfficerRepository : IRepositoryBase<ServiceDeskOfficer>
    {
        Task<List<ServiceDeskOfficer>> GetByOfficerIdAsync(Guid officerId);
        Task<ServiceDeskOfficer> GetOfficerByIdAndServiceDeskAsync(Guid officerId, Guid serviceDeskId);
    }
}
