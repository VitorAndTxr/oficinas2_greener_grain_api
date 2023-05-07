using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Domain.Entities;
using System.Threading.Tasks;

namespace GreenerGrain.Data.Interfaces
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<Customer> GetByEmailAsync(string email);
    }
}
