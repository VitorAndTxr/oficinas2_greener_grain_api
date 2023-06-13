using GreenerGrain.Domain.Enumerators;
using System;
using System.Collections.Generic;

namespace GreenerGrain.Domain.ViewModels
{
    public class UnitViewModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; }
        public string MAC { get; set; }

        public string Ip { get; set; }
        public UnitStateEnum State { get; set; }

        public Guid OwnerId { get; set; }

        public virtual AccountViewModel Owner { get;  set; }
        public virtual List<ModuleViewModel> Modules { get;  set; } = new List<ModuleViewModel>();

    }
}
