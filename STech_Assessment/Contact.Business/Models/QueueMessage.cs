using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Business.Models
{
    public class QueueMessage
    {
        public ReportModel Message { get; set; }
        public string From { get; set; }
        public string To { get; set; } = "https://localhost:44381/api/v1/reports/";
        public string WebHookActionId { get; set; }
        public string ActionName { get; set; }

    }
}
