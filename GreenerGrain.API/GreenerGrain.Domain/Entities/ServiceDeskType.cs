using GreenerGrain.Domain.Enumerators;
using System.Collections.Generic;

namespace GreenerGrain.Domain.Entities
{
    public class ServiceDeskType
    {
        public ServiceDeskTypeEnum Id { get; private set; }
        public string Description { get; private set; }

        public virtual ICollection<ServiceDesk> ServiceDesks { get; private set; } = new List<ServiceDesk>();

        protected ServiceDeskType() { }
    }

}
