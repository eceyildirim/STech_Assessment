using Report.Business.Models;
using Report.Business.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Report.Business.Interfaces
{
    public interface IReportService
    {
        Task<ServiceResponse<ReportModel>> CreateReport(ReportModel person);
        Task<ServiceResponse<ReportModel>> GetReportsByLocation(string location);
        Task<ServiceResponse<List<ReportModel>>> GetReports();
    }
}
