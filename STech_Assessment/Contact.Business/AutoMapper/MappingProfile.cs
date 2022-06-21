using AutoMapper;
using Report.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Report.Entity.Models;

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
            CreateMap<ReportModel, Report.Entity.Models.Report>().ReverseMap();
        }
    }
}
