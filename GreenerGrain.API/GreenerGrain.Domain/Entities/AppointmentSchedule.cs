using GreenerGrain.Framework.Database.EfCore.Model;
using System;

namespace GreenerGrain.Domain.Entities
{
    public class AppointmentSchedule : AuditEntity<Guid>
    {
        public Guid InstitutionServiceLocationId { get; private set; }
        public Guid AppointmentId { get; private set; }
        public DateTime ScheduledDate { get; private set; }

        public virtual Appointment Appointment { get; set; }
        public virtual InstitutionServiceLocation InstitutionServiceLocation { get; set; }

        protected AppointmentSchedule() { }
        public AppointmentSchedule(Guid institutionServiceLocationId, Guid appointmentId, DateTime scheduledDate)
        {
            SetId(Guid.NewGuid());
            Activate();

            InstitutionServiceLocationId = institutionServiceLocationId;
            AppointmentId = appointmentId;
            ScheduledDate = scheduledDate;
        }
    }
}
