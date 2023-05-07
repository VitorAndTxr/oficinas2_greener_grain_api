using GreenerGrain.Framework.Enumerators;

namespace GreenerGrain.Domain.Enumerators
{
    public class OfficerError : Enumeration
    {
        public OfficerError(int id, string code, string name) : base(id, code, name) { }

        public static OfficerError OfficerDoesNotExist = new(1, "OFF001", "O Atendente não existe.");

    }

}