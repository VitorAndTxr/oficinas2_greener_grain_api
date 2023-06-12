using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GreenerGrain.Data.Interfaces
{
    public interface IUnitRepository : IRepositoryBase<Unit>
    {
        Task<Unit> GetByUnitCode(string code);
        Task<List<Unit>> ListAll();
        Task<Unit> GetByIdAsync(string id);
        Task<List<Unit>> ListUnitGettingOffline();
    }
}