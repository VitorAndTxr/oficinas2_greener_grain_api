using API.Framework.Interfaces;
using AutoMapper;
using GreenerGrain.Framework.Services;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;

namespace GreenerGrain.Service.Services
{
    public class UnitService : ServiceBase, IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;


        public UnitService(
            IApiContext apiContext
            , IUnitRepository unitRepository
            , IMapper mapper)
                : base(apiContext)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public UnitViewModel GetByUnitCode(string unitCode)
        {
            var unit = _unitRepository.GetByUnitCode(unitCode).Result;

            var unitViewModel = _mapper.Map<UnitViewModel>(unit);
            return unitViewModel;
        }
    }
}
