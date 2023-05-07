using System;


namespace GreenerGrain.Domain.Entities
{
    public  class InstitutionProviderSettings
    {
        public Guid Id { get; set; }
        public Guid ProviderId { get; set; }
        public Guid InstitutionId { get; set; }
        public string ProviderCode { get; set; }
        public string CredentialEmail { get; set; }
        public string JsonKey { get; set; }
    }
}
