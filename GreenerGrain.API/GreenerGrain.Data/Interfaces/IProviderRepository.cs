using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Domain.Entities;
using System.Threading.Tasks;

namespace GreenerGrain.Data.Interfaces
{
    public interface IProviderRepository : IRepositoryBase<Provider>
    {
        Task<Provider> GetByCode(string code);
    }
}
