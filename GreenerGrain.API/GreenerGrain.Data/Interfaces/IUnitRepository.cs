using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Domain.Entities;
using System.Threading.Tasks;

namespace GreenerGrain.Data.Interfaces
{
    public interface IUnitRepository : IRepositoryBase<Unit>
    {
        Task<Unit> GetByUnitCode(string code);
    }
}