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

        public ServiceResponse<ReportModel> CreatePerson(ReportModel person)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<ReportModel> DeletePerson(string id)
        {
            throw new NotImplementedException();
        }

        public List<ReportModel> GetAllPersons()
        {
            throw new NotImplementedException();
        }
    }
}
