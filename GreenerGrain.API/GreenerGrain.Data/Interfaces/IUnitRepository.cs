using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace GreenerGrain.Data.Interfaces
{
    public interface IUnitRepository : IRepositoryBase<Unit>
    {
        Task<Unit> GetByIdAsync(string id);
        Task<Unit> GetByModuleIdAsync(Guid id);

        Task<Unit> GetByUnitCode(string code);
        Task<List<Unit>> ListAll();
        Task<List<Unit>> ListUnitGettingOffline();
    }
}