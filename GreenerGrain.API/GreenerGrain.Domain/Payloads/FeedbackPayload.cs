using System;

namespace GreenerGrain.Domain.Payloads
{
    public class FeedbackPayload
    {
        public Guid AppointmentId { get; set; }        
        public int CustomerRate { get; set; }
        public string CustomerNote { get; set; }
    }
}
