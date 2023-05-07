using GreenerGrain.Framework.Database.EfCore.Model;
using GreenerGrain.Domain.Enumerators;
using System;

namespace GreenerGrain.Domain.Entities
{
    public class InstitutionReport : AuditEntity<Guid>
    {
        public Guid InstitutionId { get; private set; }
        public ReportTypeEnum ReportTypeId { get; private set; }
        public string ReportLink { get; private set; }
        
        public virtual ReportType ReportType { get; private set; }

        protected InstitutionReport() { }
        public InstitutionReport(Guid institutionId, ReportTypeEnum reportTypeId, string reportLink)
        {
            SetId(Guid.NewGuid());
            Activate();

            InstitutionId = institutionId;
            ReportTypeId = reportTypeId;
            ReportLink = reportLink;
        }
    }
}
