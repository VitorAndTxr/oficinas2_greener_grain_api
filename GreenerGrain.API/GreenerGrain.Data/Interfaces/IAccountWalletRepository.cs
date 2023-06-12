using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace GreenerGrain.Data.Interfaces
{
    public interface IAccountWalletRepository : IRepositoryBase<AccountWallet>
    {
        Task<AccountWallet> GetByAccountId(Guid accountId);
    }
}