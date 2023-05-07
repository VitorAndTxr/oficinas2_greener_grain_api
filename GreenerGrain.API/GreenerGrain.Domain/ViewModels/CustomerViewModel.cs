using System;

namespace GreenerGrain.Domain.ViewModels
{
    public class CustomerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }
    }
}
