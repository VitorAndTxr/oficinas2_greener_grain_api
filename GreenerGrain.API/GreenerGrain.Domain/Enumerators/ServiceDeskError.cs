using GreenerGrain.Framework.Enumerators;

namespace GreenerGrain.Domain.Enumerators
{
    public class ServiceDeskError : Enumeration
    {
        public ServiceDeskError(int id, string code, string name) : base(id, code, name) { }

        public static ServiceDeskError ServiceDeskIsNotOpen = new(1, "SDK001", "O balcão está fechado");
        public static ServiceDeskError ServiceDeskDoesNotExist = new(2, "SDK002", "O balcão selecionado não existe");
        public static ServiceDeskError ServiceDeskNoTimeAvailable = new(3, "SDK003", "Sem horários disponíveis no momento");
    }
}