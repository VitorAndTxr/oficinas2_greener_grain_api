using GreenerGrain.Framework.Database.EfCore.Model;
using System;

namespace GreenerGrain.Domain.Entities
{
    public class AppointmentQueue : AuditEntity<Guid>
    {
        public Guid AppointmentId { get; private set; }
        public virtual Appointment Appointment { get; set; }

        public DateTime QueueEntranceHour { get; private set; }
        public DateTime? QueueAttendHour { get; private set; }
        public DateTime? AttendCloseHour { get; private set; }
        public int StartPosition { get; private set; }

        protected AppointmentQueue() { }
        public AppointmentQueue(Guid appointmentId, DateTime queueEntranceHour, int startPosition)
        {
            SetId(Guid.NewGuid());
            Activate();            
            AppointmentId = appointmentId;
            QueueEntranceHour = queueEntranceHour;
            StartPosition = startPosition;
        }

        public void SetQueueAttendHour(DateTime queueAttendHour)
        {
            QueueAttendHour = queueAttendHour;
        }
        public void SetAttendCloseHour(DateTime attendCloseHour)
        {
            AttendCloseHour = attendCloseHour;
        }
    }
}
