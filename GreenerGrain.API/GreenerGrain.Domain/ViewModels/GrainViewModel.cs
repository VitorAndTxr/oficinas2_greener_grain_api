using System;

namespace GreenerGrain.Domain.ViewModels
{
    public class GrainViewModel
    {
        public Guid Id { get; set; }
        public string Name { get;  set; }
        public float Price { get;  set; }
        public string ImageUrl { get;  set; }
        public Guid CreatorId { get;  set; }
        public AccountViewModel Creator { get;  set; }
    }
}
