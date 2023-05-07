using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Domain.Entities;
using GreenerGrain.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenerGrain.Data.Interfaces
{
    public interface IServiceDeskRepository : IRepositoryBase<ServiceDesk>
    {
        Task<ServiceDesk> GetByIdAndTypeAsync(Guid serviceDeskId, ServiceDeskTypeEnum serviceDeskType);
        Task<ServiceDesk> GetByCodeAsync(string code);
        Task<IList<ServiceDesk>> GetByInstitutionAndServiceTypeAsync(Guid institutionId, ServiceDeskTypeEnum serviceDeskType);
    }   
}
