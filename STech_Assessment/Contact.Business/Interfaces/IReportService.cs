﻿using Report.Business.Models;
using Report.Business.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Report.Business.Interfaces
{
    public interface IReportService
    {
        ServiceResponse<ReportModel> CreateReport(ReportModel person);
        ServiceResponse<ReportModel> GetReportsById(string id);
        ServiceResponse<List<ReportModel>> GetReports();
    }
}
