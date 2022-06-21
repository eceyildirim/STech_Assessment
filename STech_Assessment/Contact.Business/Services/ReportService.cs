using Microsoft.AspNetCore.Http;
using PhoneDirectory.Business.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Business.Services
{
    public class ReportService : BaseService<ReportService>, IReportService
    {
        private readonly IMongoRepository<Report> _reportRepository;

        public PersonService(
                IMongoRepository<Report> reportRepository,
                IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
            _reportRepository = reportRepository;
        }

    }
}
