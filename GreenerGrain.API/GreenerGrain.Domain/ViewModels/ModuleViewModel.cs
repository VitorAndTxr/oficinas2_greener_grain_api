using System;

namespace GreenerGrain.Domain.ViewModels
{
    public class ModuleViewModel
    {
        public Guid Id { get; set; }

        public float ContentLevel { get; set; }
        public int Order { get; set; }

        public Guid UnitId { get; set; }
        public Guid GrainId { get; set; }
        public virtual GrainViewModel Grain { get; set; }
    }
}
