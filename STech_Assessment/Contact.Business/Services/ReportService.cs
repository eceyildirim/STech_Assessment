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
using MongoDB.Driver;
using System.Linq;
using Report.Resources;

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

        public ServiceResponse<ReportModel> CreateReport(ReportModel report)
        {
            var res = new ServiceResponse<ReportModel>();

            var reportEntity = Mapper.Map<Report.Entity.Models.Report>(report);

            var createPersonRes = _reportRepository.InsertOne(reportEntity);

            if (!createPersonRes.Successed || createPersonRes.Result == null)
            {
                res.Code = StatusCodes.Status500InternalServerError;
                res.Message = SystemMessage.Feedback_UnexpectedError;
                res.Successed = false;
                res.Errors = createPersonRes.Message;
            }

            res.Result = Mapper.Map<ReportModel>(createPersonRes.Result);

            return res;
        }

        public ServiceResponse<List<ReportModel>> GetReports()
        {
            var res = new ServiceResponse<List<ReportModel>>();
            #region [Get Data]

            var match = Builders<Report.Entity.Models.Report>.Filter.Where(x => x.DeletedAt == null);

            var reports = _reportRepository.Aggregate()
                                           .Match(match)
                                           .ToList();

            #endregion

            res.Result = Mapper.Map<List<ReportModel>>(reports);

            return res;

        }

        public ServiceResponse<ReportModel> GetReportsById(string id)
        {
            var res = new ServiceResponse<ReportModel> { };

            var match = Builders<Report.Entity.Models.Report>.Filter.Where(x => x.UUID == id);

            var lookedUps = _reportRepository.Aggregate()
            .Match(match)
            .As<Report.Entity.Models.Report>()
            .ToList();

            var report = lookedUps.FirstOrDefault();

            if (report == null)
            {
                res.Code = StatusCodes.Status404NotFound;
                res.Message = CustomMessage.ReportNotFound;
                res.Successed = false;

                return res;

            }

            res.Result = Mapper.Map<ReportModel>(report);

            return res;
        }
    }
}
