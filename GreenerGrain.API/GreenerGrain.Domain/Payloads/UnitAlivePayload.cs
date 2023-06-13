using GreenerGrain.Domain.Enumerators;

namespace GreenerGrain.Domain.Payloads
{
    public class UnitAlivePayload
    {
        public string Id { get; set; }
        public string Ip { get; set; }
        public UnitStateEnum Status { get; set; }


    }
}