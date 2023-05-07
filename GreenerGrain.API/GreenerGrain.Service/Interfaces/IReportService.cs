using GreenerGrain.Domain.Enumerators;
using GreenerGrain.Domain.Payloads;
using GreenerGrain.Domain.ViewModels;

namespace GreenerGrain.Service.Interfaces
{
    public interface IReportService
    {
        ReportLinkViewModel GetReportLink(ReportTypeEnum reportType);
    }
}
