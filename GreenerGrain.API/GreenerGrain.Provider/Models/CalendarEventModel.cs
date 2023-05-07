using System;

namespace GreenerGrain.Provider.Models
{
    public class CalendarEventModel
    {
        public string Id { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Summary { get; set; }
        public string HtmlLink { get; set; }
        public string HangoutLink { get; set; }
    }
}
