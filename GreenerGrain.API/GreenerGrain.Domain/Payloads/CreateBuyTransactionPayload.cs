using System;

namespace GreenerGrain.Domain.Payloads
{
    public class CreateBuyTransactionPayload
    {
        public int Quantity { get; set; }
        public Guid ModuleId { get; set; }
        public Guid GrainId { get; set; }

    }
}