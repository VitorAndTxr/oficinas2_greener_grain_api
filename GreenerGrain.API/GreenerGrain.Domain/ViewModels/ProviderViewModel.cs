using System;

namespace GreenerGrain.Domain.ViewModels
{
    public class ProviderViewModel
    {
        public ProviderViewModel() { }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
