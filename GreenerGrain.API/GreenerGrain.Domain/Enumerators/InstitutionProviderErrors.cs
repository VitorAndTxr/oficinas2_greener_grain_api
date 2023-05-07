using GreenerGrain.Framework.Enumerators;

namespace GreenerGrain.Domain.Enumerators
{
    public class InstitutionProviderErrors : Enumeration
    {
        public InstitutionProviderErrors(int id, string code, string name) : base(id, code, name) { }
        public static InstitutionProviderErrors ApplicationIdNotFound = new(1, "INPV001", "Application id can't be null.");
        public static InstitutionProviderErrors TenantIdNotFound = new(2, "INPV002", "Tenant id can't be null.");
        public static InstitutionProviderErrors ClientSecretNotFound = new(3, "INPV003", "Client secret can't be null or empty.");
        public static InstitutionProviderErrors CredentialEmailNotFound = new(4, "INPV004", "CredentialEmail can't be null.");
        public static InstitutionProviderErrors JsonKeyNotFound = new(5, "INPV005", "JsonKey can't be null.");
    }
}
