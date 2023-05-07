using GreenerGrain.Framework.Database.EfCore.Model;
using System;
using System.Collections.Generic;

namespace GreenerGrain.Domain.Entities
{
    public class InstitutionServiceLocation : AuditEntity<Guid>
    {      
        public string Description { get; private set; }
        public Guid InstitutionId { get; private set; }
        public bool Remote { get; private set; }

        public virtual ICollection<OfficerHour> OfficerHours { get; private set; } = new List<OfficerHour>();        
        public virtual ICollection<AppointmentSchedule> AppointmentSchedules { get; private set; } = new List<AppointmentSchedule>();

        protected InstitutionServiceLocation() { }
        public InstitutionServiceLocation(string description, Guid institutionId, bool remote)
        {
            SetId(Guid.NewGuid());
            Activate();

            Description = description;
            InstitutionId = institutionId;
            Remote = remote;
        }
    }
}
