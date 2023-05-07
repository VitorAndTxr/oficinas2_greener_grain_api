using System;
using System.Collections.Generic;

namespace GreenerGrain.Domain.ViewModels
{
    public class ProfileViewModel
    {
        public ProfileViewModel() { }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
