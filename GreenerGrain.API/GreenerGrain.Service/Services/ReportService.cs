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

namespace GreenerGrain.Service.Services
{
    public class ReportService : ServiceBase, IReportService
    {
        private readonly IApiContext _apiContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IInstitutionReportRepository _institutionReportRepository;
      
        public ReportService(
            IApiContext apiContext
            , IMapper mapper
            , IConfiguration configuration
            , IInstitutionReportRepository institutionReportRepository)
            : base(apiContext)
        {
            _apiContext = apiContext;
            _mapper = mapper;
            _configuration = configuration;
            _institutionReportRepository = institutionReportRepository; 
        }

        public ReportLinkViewModel GetReportLink(ReportTypeEnum reportType)
        {
            var result = Task.Run(() => _institutionReportRepository
                .GetAsync(x => x.ReportTypeId == reportType
                    && x.InstitutionId == _apiContext.SecurityContext.Account.InstitutionId)).Result;
            
            if (!result.Any())
            {
                throw new BadRequestException(InstitutionReportErrors.ReportNotFound);
            }

            var t = result.FirstOrDefault();
            var model = _mapper.Map<ReportLinkViewModel>(t);
            return model;
        }
    }
}
