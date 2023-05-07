using API.Framework.Interfaces;
using AutoMapper;
using GreenerGrain.Framework.Services;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace GreenerGrain.Service.Services
{
    public class InstitutionServiceLocationService : ServiceBase, IInstitutionServiceLocationService
    {
        #region Fields

        private readonly IApiContext _apiContext;
        private readonly IMapper _mapper;
        private readonly IInstitutionServiceLocationRepository _institutionServiceLocationRepository;
        #endregion

        #region Constructor

        public InstitutionServiceLocationService(
            IApiContext apiContext
            , IMapper mapper            
            , IInstitutionServiceLocationRepository institutionServiceLocationRepository) : base(apiContext)
        {
            _apiContext = apiContext;
            _mapper = mapper;
            _institutionServiceLocationRepository = institutionServiceLocationRepository;
        }

        #endregion

        public IList<InstitutionServiceLocationViewModel> List(Guid institutionId)
        {
            var places = _institutionServiceLocationRepository
                .GetAsync(x => x.InstitutionId == institutionId).Result;

            var result = _mapper.Map<IList<InstitutionServiceLocationViewModel>>(places);
            return result;
        }
    }
}
