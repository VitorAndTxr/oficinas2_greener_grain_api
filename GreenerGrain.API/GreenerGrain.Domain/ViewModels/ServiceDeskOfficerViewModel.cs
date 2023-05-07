using System;
using System.Collections.Generic;

namespace GreenerGrain.Domain.ViewModels
{
    public class ServiceDeskOfficerViewModel
    {
        public Guid Id { get; set; }
        public Guid ServiceDeskId { get; set; }
        public Guid OfficerId { get; set; }

        public virtual ServiceDeskViewModel ServiceDesk { get; set; }
        public virtual List<OfficerPauseViewModel> OfficerPauses { get; set; } = new List<OfficerPauseViewModel>();
        public virtual List<OfficerHourViewModel> OfficerHours { get; set; } = new List<OfficerHourViewModel>();

    }
}
