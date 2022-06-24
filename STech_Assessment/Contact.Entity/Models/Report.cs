using Report.Core;
using Report.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Entity.Models
{
    [BsonCollection("reports")]
    public class Report : Document
    {
        public DateTime ReportRequestDate { get; set; }
        public string Location { get; set; }
        public int NumberOfRegisteredPersons { get; set; }
        public int NumberOfRegisteredPhones { get; set; }
        public ReportStatus ReportStatus { get; set; }
    }
}
