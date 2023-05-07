using System;

namespace GreenerGrain.Domain.ViewModels
{
    public class CalendarEventViewModel
    {
        public string Id { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Summary { get; set; }
        public string HtmlLink { get; set; }
        public string HangoutLink { get; set; }
    }
}
