using GreenerGrain.Domain.ViewModels;

namespace GreenerGrain.Service.Interfaces
{
    public interface IAccountWalletService
    {
        AccountWalletViewModel GetByUserWallet();
    }
}