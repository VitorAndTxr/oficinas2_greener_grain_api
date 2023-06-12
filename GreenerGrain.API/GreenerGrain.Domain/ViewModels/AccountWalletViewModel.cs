using System;

namespace GreenerGrain.Domain.ViewModels
{
    public class AccountWalletViewModel
    {
        public Guid Id { get; set; }
        public float Credits { get; set; }
        public Guid AccountId { get; set; }
        public AccountViewModel Account { get; set; }
    }
}
