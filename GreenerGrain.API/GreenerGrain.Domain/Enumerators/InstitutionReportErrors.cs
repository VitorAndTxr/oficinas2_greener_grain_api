using GreenerGrain.Framework.Enumerators;

namespace GreenerGrain.Domain.Enumerators
{
    public class InstitutionReportErrors : Enumeration
    {
        public InstitutionReportErrors(int id, string code, string name) : base(id, code, name) { }
        public static InstitutionReportErrors ReportNotFound = new(1, "REP001", "Report not found.");        
    }
}
