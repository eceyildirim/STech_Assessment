using PhoneDirectory.Business.Base;
using PhoneDirectory.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Business.Models
{
    public class ReportModel : BaseModel
    {
        public DateTime ReportRequestDate { get; set; }
        public string Location { get; set; }
        public int NumberOfRegisteredPersons { get; set; }
        public int NumberOfRegisteredPhones { get; set; }
        public ReportStatus ReportStatus { get; set; }

        public string Details { get; set; }
    }
}
