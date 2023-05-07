using API.Framework.Interfaces;
using AutoMapper;
using GreenerGrain.Framework.Exceptions;
using GreenerGrain.Framework.Services;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.Enumerators;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace GreenerGrain.Service.Services
{
    public class AccountProfileService : ServiceBase, IAccountProfileService
    {
        private readonly IApiContext _apiContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IAccountProfileRepository _accountProfileRepository;
        private readonly IProfileRepository _profileRepository;


        public AccountProfileService(
            IApiContext apiContext
            , IMapper mapper
            , IConfiguration configuration
            ,IProfileRepository profileRepository
            , IAccountProfileRepository accountProfileRepository)
            : base(apiContext)
        {
            _apiContext = apiContext;
            _mapper = mapper;
            _configuration = configuration;
            _accountProfileRepository = accountProfileRepository;
            _profileRepository = profileRepository;
        }

        public List<ProfileViewModel> GetAccountProfiles(Guid accountId)
        {
            var result = Task.Run(() => _accountProfileRepository
                .GetAsync(x => x.AccountId == accountId)).Result;

            if (!result.Any())
            {
                throw new BadRequestException(AccountErrors.WithoutPermissions);
            }

            var profileIds = result.Select(x=> x.ProfileId).ToList();
            var profiles = Task.Run(()=>_profileRepository.GetAsync(x => profileIds.Contains(x.Id))).Result;
            var model = _mapper.Map<List<ProfileViewModel>>(profiles);
            return model;
        }
    }


}
