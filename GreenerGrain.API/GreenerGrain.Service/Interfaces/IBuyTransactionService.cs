using GreenerGrain.Domain.Payloads;
using GreenerGrain.Domain.ViewModels;

namespace GreenerGrain.Service.Interfaces
{
    public interface IBuyTransactionService
    {
        BuyTransactionViewModel CreateTransaction(CreateBuyTransactionPayload payload);
    }
}