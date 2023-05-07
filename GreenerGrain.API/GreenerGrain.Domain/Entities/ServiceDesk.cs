using GreenerGrain.Framework.Database.EfCore.Model;
using GreenerGrain.Domain.Enumerators;
using System;
using System.Collections.Generic;

namespace GreenerGrain.Domain.Entities
{
    public class ServiceDesk : AuditEntity<Guid>
    {
        public ServiceDeskTypeEnum ServiceDeskTypeId { get; private set; }
        public Guid InstitutionId { get; private set; }
        public string Description { get; private set; }
        public string Code { get; private set; }
        public string CalendarName { get; private set; }
        public string CalendarTimeZone { get; set; }
        public int MeetDurationTime { get; private set; }        

        public virtual ServiceDeskType ServiceDeskType { get; set; }
        public virtual ICollection<ServiceDeskOfficer> ServiceDeskOfficers { get; set; } = new List<ServiceDeskOfficer>();
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<ServiceDeskOpeningHours> ServiceDeskOpeningHours { get; set; } = new List<ServiceDeskOpeningHours>();

        protected ServiceDesk() { }
        public ServiceDesk(ServiceDeskTypeEnum serviceDeskTypeId, Guid institutionId, string description, string code, string calendarName, string calendarTimeZone, int meetDurationTime = 30)
        {
            SetId(Guid.NewGuid());
            Activate();

            this.ServiceDeskTypeId = serviceDeskTypeId;
            InstitutionId = institutionId;
            Description = description;
            Code = code;
            CalendarName = calendarName;
            CalendarTimeZone = calendarTimeZone;
            MeetDurationTime = meetDurationTime;            
        }

        public long GetMeetDuration() 
        {
            TimeSpan meetDurationTime = TimeSpan.FromMinutes(this.MeetDurationTime);
            return meetDurationTime.Ticks;
        }

    }
}