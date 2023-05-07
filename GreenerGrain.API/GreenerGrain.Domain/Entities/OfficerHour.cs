using GreenerGrain.Framework.Database.EfCore.Model;
using System;

namespace GreenerGrain.Domain.Entities
{
    public class OfficerHour : AuditEntity<Guid>
    {
        public Guid ServiceDeskOfficerId { get; private set; }
        public Guid InstitutionServiceLocationId { get; private set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }

        public virtual ServiceDeskOfficer ServiceDeskOfficer { get; set; }
        public virtual InstitutionServiceLocation InstitutionServiceLocation { get; set; }

        protected OfficerHour() { }
        public OfficerHour(Guid serviceDeskOfficerId, Guid institutionServiceLocationId, DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            SetId(Guid.NewGuid());
            Activate();

            ServiceDeskOfficerId = serviceDeskOfficerId;
            InstitutionServiceLocationId = institutionServiceLocationId;
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
        }

        public void Edit(Guid institutionServiceLocationId, DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            UpdateDate = DateTime.Now;

            InstitutionServiceLocationId = institutionServiceLocationId;
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
