using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

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
            CreateMap<ReportModel, Report>().ReverseMap();
        }
    }
}
