using API.Framework.Interfaces;
using GreenerGrain.Framework.Database.EfCore.Factory;
using GreenerGrain.Framework.Database.EfCore.Repository;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Entities;
using System.Threading.Tasks;
using System.Linq;

namespace GreenerGrain.Data.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(IDbFactory dbFactory, IApiContext apiContext)
                    : base(dbFactory, apiContext)
        {
        }

        public async Task<Customer> GetByEmailAsync(string email)
        {
            var result =  await GetAsync(x => x.Email == email);

            return result.FirstOrDefault();
        }

    }

}