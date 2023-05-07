using System;
using System.Collections.Generic;

namespace GreenerGrain.Framework.Security
{
    public class Application
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<Profile> Profiles { get; set; }
        public IList<Role> Roles { get; set; }

        public Application()
        {
            Profiles = new List<Profile>();
            Roles = new List<Role>();
        }
    }


}
