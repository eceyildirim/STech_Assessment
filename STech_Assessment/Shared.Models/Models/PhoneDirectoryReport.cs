using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Models
{
    public class PhoneDirectoryReport
    {
        public DateTime ReportRequestDate { get; set; }
        public string Location { get; set; }
        public int NumberOfRegisteredPersons { get; set; }
        public int NumberOfRegisteredPhones { get; set; }
        public int ReportStatus { get; set; }
    }
}
