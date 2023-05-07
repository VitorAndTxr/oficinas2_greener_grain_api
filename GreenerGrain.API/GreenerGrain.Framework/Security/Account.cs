using System;
using System.Collections.Generic;

namespace GreenerGrain.Framework.Security
{
    public class Account
    {
        public Guid Id { get; set; }
        public Guid InstitutionId { get; set; }
        public Guid HolderId { get; set; }
        public string ProviderCode { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        
        public IList<Application> Applications { get; set; }
    }
}
