using System;

namespace GreenerGrain.Domain.ViewModels
{
    public class OfficerPauseViewModel
    {
        public Guid Id { get; set; }
        public Guid AttendantServiceId { get; private set; }
        public bool EntireDay { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string Reason { get; private set; }

        public virtual ServiceDeskOfficerViewModel AttendantService { get; set; }

    }
}
