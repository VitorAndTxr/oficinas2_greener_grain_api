using System.Collections.Generic;

namespace GreenerGrain.Domain.ViewModels
{
    public class ServiceDeskOfficerAvaliabilityViewModel
    {
        public int Order { get; set; }
        public string DateLabel { get; set; }
        public List<string> AvaliableTimes{ get; set; }

    }
}
