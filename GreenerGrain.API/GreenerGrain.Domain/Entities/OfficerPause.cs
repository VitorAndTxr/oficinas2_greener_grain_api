using GreenerGrain.Framework.Database.EfCore.Model;
using System;

namespace GreenerGrain.Domain.Entities
{
    public  class OfficerPause : AuditEntity<Guid>
    {
        public Guid ServiceDeskOfficerId { get; private set; }
        public bool EntireDay { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string Reason { get; private set; }

        public virtual ServiceDeskOfficer ServiceDeskOfficer { get; set; }

        protected OfficerPause() { }
        public OfficerPause(Guid serviceDeskOfficerId, bool entireDay, DateTime startDate, DateTime? endDate, string reason)
        {
            SetId(Guid.NewGuid());
            Activate();
            ServiceDeskOfficerId = serviceDeskOfficerId;
            EntireDay = entireDay;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }
    }
}
