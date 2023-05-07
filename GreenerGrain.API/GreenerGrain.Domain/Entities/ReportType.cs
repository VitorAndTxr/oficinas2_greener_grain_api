using GreenerGrain.Domain.Enumerators;
using System.Collections.Generic;

namespace GreenerGrain.Domain.Entities
{
    public class ReportType
    {
        public ReportTypeEnum Id { get; private set; }
        public string Description { get; private set; }

        public virtual ICollection<InstitutionReport> InstitutionReports { get; private set; } = new List<InstitutionReport>();

        protected ReportType() { }
    }
}
