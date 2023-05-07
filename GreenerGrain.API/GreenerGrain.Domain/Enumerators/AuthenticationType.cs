using GreenerGrain.Framework.Enumerators;

namespace GreenerGrain.Domain.Enumerators
{
    public class ProviderType : Enumeration
    {
        public ProviderType(int id, string code, string name) : base(id, code, name) { }

        public static ProviderType Internal = new ProviderType(1, "internal", "Internal");
        public static ProviderType Microsoft = new ProviderType(2, "microsoft", "Microsoft");
        public static ProviderType Google = new ProviderType(3, "google", "Google");
        public static ProviderType Application = new ProviderType(4, "application", "Application");
    }

    public enum AuthenticationType : int
    {
        Internal = 1,
        Microsoft = 2,
        Google = 3,
        Application = 4
    }
}