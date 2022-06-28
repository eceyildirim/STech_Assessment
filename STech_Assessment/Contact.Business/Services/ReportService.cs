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
using MassTransit;
using Shared.Models;

namespace Report.Business.Services
{
    public class ReportService : BaseService<ReportService>, IReportService, IConsumer<SharedReport>
    {
        private readonly IMongoRepository<ReportM> _reportRepository;

        public ReportService(
                IMongoRepository<ReportM> reportRepository,
                IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
            _reportRepository = reportRepository;
        }

        public async Task Consume(ConsumeContext<SharedReport> context)
        {
            var msg = context.Message;

            var report = new ReportModel
            {
                ReportRequestDate = msg.ReportRequestDate,
                Location = msg.Location,
                NumberOfRegisteredPersons = msg.NumberOfRegisteredPersons,
                NumberOfRegisteredPhones = msg.NumberOfRegisteredPersons,
                ReportStatus = (Core.ReportStatus)msg.ReportStatus 
            };

            GenerateReport(report);
        }

        public ServiceResponse<ReportModel> GenerateReport(ReportModel report)
        {
            var res = new ServiceResponse<ReportModel>();

            var reportEntity = new ReportM
            {
                CreatedAt = report.CreatedAt,
                UUID = report.UUID,
                UpdatedAt = report.UpdatedAt,
                DeletedAt = report.DeletedAt,
                ReportRequestDate = report.ReportRequestDate,
                Location = report.Location,
                NumberOfRegisteredPersons = report.NumberOfRegisteredPersons,
                NumberOfRegisteredPhones = report.NumberOfRegisteredPhones,
                ReportStatus = report.ReportStatus
            };

            var reportState = _reportRepository.FilterBy(x => x.Location == report.Location && x.ReportRequestDate == report.ReportRequestDate && report.ReportStatus == x.ReportStatus && report.ReportStatus == Core.ReportStatus.Prepare).Result;

            if(reportState.Count == 0)
            {
                //insert report
                var createPersonRes = _reportRepository.InsertOne(reportEntity);

                if (!createPersonRes.Successed || createPersonRes.Result == null)
                {
                    res.Code = StatusCodes.Status500InternalServerError;
                    res.Message = SystemMessage.Feedback_UnexpectedError;
                    res.Successed = false;
                    res.Errors = createPersonRes.Message;
                }

                var rModel = new ReportModel
                {
                    ReportRequestDate = reportEntity.ReportRequestDate,
                    Location = reportEntity.Location,
                    NumberOfRegisteredPersons = reportEntity.NumberOfRegisteredPersons,
                    NumberOfRegisteredPhones = reportEntity.NumberOfRegisteredPhones,
                    ReportStatus = reportEntity.ReportStatus
                };

                res.Result = rModel;

                return res;
            }

            var completeReport = _reportRepository.ReplaceOne(reportEntity);

            if (completeReport.Result == null)
            {
                res.Code = StatusCodes.Status500InternalServerError;
                res.Message = SystemMessage.Feedback_UnexpectedError;
                res.Successed = false;
                return res;
            }

            return res;
        }

        public ServiceResponse<List<ReportModel>> GetReports()
        {
            var res = new ServiceResponse<List<ReportModel>>();
            #region [Get Data]

            var match = Builders<ReportM>.Filter.Where(x => x.DeletedAt == null);

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

            //Control report id
            #region [Validation]
            if (string.IsNullOrEmpty(id))
            {
                res.Code = StatusCodes.Status400BadRequest;
                res.Message = CustomMessage.PleaseFillInTheRequiredFields;
                res.Successed = false;

                return res;
            }
            #endregion

            var match = Builders<ReportM>.Filter.Where(x => x.UUID == id);

            var lookedUps = _reportRepository.Aggregate()
            .Match(match)
            .As<ReportM>()
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
