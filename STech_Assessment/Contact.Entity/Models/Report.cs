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
    }
}
