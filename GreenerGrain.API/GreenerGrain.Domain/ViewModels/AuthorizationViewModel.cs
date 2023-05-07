using System.Collections.Generic;

namespace GreenerGrain.Domain.ViewModels
{
    public class AuthorizationViewModel
    {
        public AuthorizationViewModel() { }

        public string Token { get; set; }
        public List<ProfileViewModel> Profiles { get; set; }
    }
}
