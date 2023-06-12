using GreenerGrain.Domain.ViewModels;

namespace GreenerGrain.Service.Interfaces
{
    public interface IUnitService
    {
        UnitViewModel GetByUnitCode(string unitCode);
    }
}