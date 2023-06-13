using System;

namespace GreenerGrain.Domain.ViewModels
{
    public class BuyTransactionViewModel
    {
        public BuyTransactionViewModel() { }

        public float Quantity { get; set; }
        public float Value { get; set; }

        public Guid BuyerId { get; set; }
        public Guid ModuleId { get; set; }
        public Guid GrainId { get; set; }

        public AccountViewModel Buyer { get; set; }
        public ModuleViewModel Module { get; set; }
        public GrainViewModel Grain { get; set; }

    }
}
