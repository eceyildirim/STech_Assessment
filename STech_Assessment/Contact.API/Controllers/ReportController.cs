using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Report.Business.Interfaces;
using Report.Business.Models;
using Report.Business.Validators;
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
        public readonly IReportService _reportService;
        public readonly IQueueService _queueService;
        private readonly ReportValidator reportValidator = new ReportValidator();

        public ReportController(IReportService reportService, IQueueService queueService)
        {
            _reportService = reportService;
            _queueService = queueService;
        }

        [HttpGet, Route("{id}")]
        public IActionResult GetReportById(string id)
        {
            var res = _reportService.GetReportsById(id);
            if (!res.Successed)
            {
                return APIResponse(res);
            }

            return Ok(res.Result);
        }

        [HttpGet, Route("")]
        public IActionResult GetAllReports()
        {
            return Ok(_reportService.GetReports());
        }


        [HttpPost, Route("create")]
        public IActionResult CreateReport(ReportModel report)
        {
            var created = _reportService.GenerateReport(report);

            return Ok(created);
        }

    }
}
