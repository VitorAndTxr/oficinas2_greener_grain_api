using GreenerGrain.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenerGrain.Service.Interfaces
{
    public interface IInstitutionServiceLocationService
    {
        IList<InstitutionServiceLocationViewModel> List(Guid institutionId);
    }
}
