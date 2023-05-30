using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace GreenerGrain.Data.Interfaces
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Task<Account> GetByLogin(string login);     
    }
}