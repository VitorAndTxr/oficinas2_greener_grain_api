using System;

namespace GreenerGrain.Domain.ViewModels
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }

        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

    }
}
