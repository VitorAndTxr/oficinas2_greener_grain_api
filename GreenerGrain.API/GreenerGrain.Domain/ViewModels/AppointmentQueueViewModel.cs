using System;

namespace GreenerGrain.Domain.ViewModels
{
    public class AppointmentQueueViewModel
    {
        public Guid AppointmentId { get; private set; }
        public DateTime QueueEntranceHour { get; private set; }
        public DateTime QueueAttendHour { get; private set; }
        public DateTime AttendCloseHour { get; private set; }
        public int StartPosition { get; private set; }
    }
}
