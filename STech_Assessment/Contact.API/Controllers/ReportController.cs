using Microsoft.AspNetCore.Mvc;
using Report.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report.API.Controllers
{
    [Route("api/v1/reports")]
    [ApiController]
    public class ReportController : BaseController<ReportController>
    {
        public readonly IReportService _ReportService;

        public ReportController(IReportService ReportService)
        {
            _ReportService = ReportService;
        }

    }
}
