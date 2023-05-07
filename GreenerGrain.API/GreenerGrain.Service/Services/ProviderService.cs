using API.Framework.Interfaces;
using AutoMapper;
using GreenerGrain.Framework.Exceptions;
using GreenerGrain.Framework.Services;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Enumerators;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace GreenerGrain.Service.Services
{
    public class ProviderService : ServiceBase, IProviderService
    {

        private readonly IProviderRepository _providerRepository;
        private readonly IMapper _mapper;

        public ProviderService(IApiContext apiContext
            , IProviderRepository providerRepository
            , IMapper mapper
            ) : base(apiContext)
        {
            _providerRepository = providerRepository;
            _mapper = mapper;            
        }

        public ProviderViewModel GetByCode(string code)
        {
            var provider = Task.Run(() => _providerRepository.GetByCode(code)).Result;
            if (provider == null)
            {
                throw new BadRequestException(ProviderErrors.ProviderNotFound);
            }

            var providerViewModel = _mapper.Map<ProviderViewModel>(provider);
            return providerViewModel;
        }

        public ProviderViewModel GetById(Guid id)
        {
            var provider = Task.Run(() => _providerRepository.GetByIdAsync(id)).Result;
            if (provider == null)
            {
                throw new BadRequestException(ProviderErrors.ProviderNotFound);
            }

            var providerViewModel = _mapper.Map<ProviderViewModel>(provider);
            return providerViewModel;
        }

    }
}
