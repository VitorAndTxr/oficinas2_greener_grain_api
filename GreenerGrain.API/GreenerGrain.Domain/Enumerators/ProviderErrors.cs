using GreenerGrain.Framework.Enumerators;

namespace GreenerGrain.Domain.Enumerators
{
    public class ProviderErrors : Enumeration
    {
        public ProviderErrors(int id, string code, string name) : base(id, code, name) { }

        public static ProviderErrors ProviderNotFound = new(1, "PV001", "The provider not found.");
    }
}
