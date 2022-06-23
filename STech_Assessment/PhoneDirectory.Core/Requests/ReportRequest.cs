using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Core.Requests
{
    public class ReportRequest : BaseQueryRequest
    {
        public DateTime RequestDate { get; set; }
        public string Location { get; set; }
        public int NumberOfRegisteredPersons { get; set; }
        public int NumberOfRegisteredPhones { get; set; }
        public ReportStatus ReportStatus { get; set; } = ReportStatus.Prepare;
    }
}
