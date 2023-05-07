using API.Framework.Interfaces;
using AutoMapper;
using GreenerGrain.Framework.Services;
using GreenerGrain.Data.Interfaces;
using GreenerGrain.Domain.ViewModels;
using GreenerGrain.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenerGrain.Service.Services
{
    public class ProfileService : ServiceBase, IProfileService
    {
        private readonly IAccountProfileRepository _profileRepository;
        private readonly IMapper _mapper;


        public ProfileService(
            IApiContext apiContext
            , IAccountProfileRepository profileRepository
            , IMapper mapper)
                : base(apiContext)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        public IList<ProfileViewModel> GetProfilesByAccountIdApplicationId(Guid accountId)
        {
            var accountProfiles = Task.Run(() => _profileRepository.GetAccounProfiles(accountId)).Result;

            var profileViewModel = _mapper.Map<List<ProfileViewModel>>(accountProfiles.Select(c => c.Profile));

            return profileViewModel;
        }

    }
}
