using Report.Business.Models;
using Report.Business.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Business.Interfaces
{
    public interface IReportService
    {
        ServiceResponse<ReportModel> CreatePerson(ReportModel person);
        ServiceResponse<ReportModel> DeletePerson(string id);
        List<ReportModel> GetAllPersons();
    }
}
