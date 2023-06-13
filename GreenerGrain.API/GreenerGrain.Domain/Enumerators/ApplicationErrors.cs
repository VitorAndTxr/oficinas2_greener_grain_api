using GreenerGrain.Framework.Enumerators;

namespace GreenerGrain.Domain.Enumerators
{
    public class ApplicationErrors : Enumeration
    {
        public ApplicationErrors(int id, string code, string name) : base(id, code, name) { }

        public static ApplicationErrors ApplicationNotExistis = new ApplicationErrors(1, "AP001", "Aplicação não existe.");
        
    }
}
