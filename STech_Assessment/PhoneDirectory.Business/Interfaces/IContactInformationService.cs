using PhoneDirectory.Business.Models;
using PhoneDirectory.Business.Responses;
using PhoneDirectory.Core.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Business.Interfaces
{
    public interface IContactInformationService
    {
        ServiceResponse<ContactInformationModel> AddContact(ContactInformationModel contactInformationModel);
        ServiceResponse<ContactInformationModel> DeleteContactById(string id);
        ServiceResponse<ReportRequest> GetReportByLocation(ReportRequest reportRequest);

    }
}
