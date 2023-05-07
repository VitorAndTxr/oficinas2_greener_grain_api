using GreenerGrain.Framework.Database.EfCore.Model;
using System;

namespace GreenerGrain.Domain.Entities
{
    public class ServiceDeskOpeningHours : AuditEntity<Guid>
    {
        public Guid ServiceDeskId { get; private set; }
        public DayOfWeek DayOfWeek { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }

        public virtual ServiceDesk ServiceDesk { get; set; }

        protected ServiceDeskOpeningHours() { }
        public ServiceDeskOpeningHours(Guid serviceDeskId, DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            SetId(Guid.NewGuid());
            Activate();

            ServiceDeskId = serviceDeskId;
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
