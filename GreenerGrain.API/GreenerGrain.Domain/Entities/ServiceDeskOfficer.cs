using GreenerGrain.Framework.Database.EfCore.Model;
using System;
using System.Collections.Generic;

namespace GreenerGrain.Domain.Entities
{
    public class ServiceDeskOfficer : AuditEntity<Guid>
    {
        public Guid ServiceDeskId { get; private set; }
        public Guid OfficerId { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public virtual ServiceDesk ServiceDesk { get; set; }        
        public virtual ICollection<OfficerPause> OfficerPauses { get; private set; } = new List<OfficerPause>();
        public virtual ICollection<OfficerHour> OfficerHours { get; private set; } = new List<OfficerHour>();

        protected ServiceDeskOfficer() { }
        public ServiceDeskOfficer(Guid serviceDeskId, Guid officerId, string name, string email)
        {
            SetId(Guid.NewGuid());
            Activate();

            ServiceDeskId = serviceDeskId;
            OfficerId = officerId;
            Name = name;
            Email = email;
        }
    }

}