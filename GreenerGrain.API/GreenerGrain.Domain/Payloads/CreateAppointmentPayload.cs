using System;

namespace GreenerGrain.Domain.Payloads
{
    public class CreateAppointmentPayload
    {
        public Guid ServiceDeskId { get; set; }        
        public DateTime Date { get; set; }
        public string Note { get; set; }

        public CustomerPayload Customer { get; set; }
    }
}
