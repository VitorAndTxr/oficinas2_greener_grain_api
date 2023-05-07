using GreenerGrain.Domain.Enumerators;
using System;

namespace GreenerGrain.Domain.Payloads
{
    public class CloseAppointmentQueuePayload
    {
        public AppointmentStatusEnum AppointmentStatusId { get; set; }
    }
}
