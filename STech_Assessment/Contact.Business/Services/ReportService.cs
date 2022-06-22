using Microsoft.AspNetCore.Http;
using Report.Business.Base;
using Report.Business.Interfaces;
using Report.Business.Models;
using Report.Business.Responses;
using Report.DAL.Interfaces;
using System;
using Report.Entity.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Report.Business.Services
{
    public class ReportService : BaseService<ReportService>, IReportService
    {
        private readonly IMongoRepository<Report.Entity.Models.Report> _reportRepository;

        public ReportService(
                IMongoRepository<Report.Entity.Models.Report> reportRepository,
                IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
            _reportRepository = reportRepository;
        }

        public async Task<ServiceResponse<ReportModel>> CreateReport(ReportModel person)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<ReportModel>>> GetReports()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<ReportModel>> GetReportsByLocation(string location)
        {
            throw new NotImplementedException();
        }
    }
}
