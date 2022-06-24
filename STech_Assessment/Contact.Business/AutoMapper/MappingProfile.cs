using AutoMapper;
using Report.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Report.Entity.Models;
using Shared.Models;

namespace Report.Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateReportMappings();
        }

        public void CreateReportMappings()
        {
            CreateMap<ReportModel, ReportM>().ReverseMap();
            CreateMap<ReportModel, SharedReport>().ReverseMap();
            CreateMap<SharedReport, ReportModel>().ReverseMap();
        }
    }
}
