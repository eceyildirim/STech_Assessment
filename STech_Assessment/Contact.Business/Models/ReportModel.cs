using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Report.Business.Base;
using Report.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Report.Business.Models
{
    public class ReportModel : BaseModel
    {
        public DateTime ReportRequestDate { get; set; }
        public string Location { get; set; }
        public int NumberOfRegisteredPersons { get; set; }
        public int NumberOfRegisteredPhones { get; set; }
        public ReportStatus ReportStatus { get; set; }
    }
}
