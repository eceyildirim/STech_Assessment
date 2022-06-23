using PhoneDirectory.Business.Models;
using PhoneDirectory.Business.Responses;
using PhoneDirectory.Core.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneDirectory.Business.Interfaces
{
    public interface IPersonService
    {
        ServiceResponse<PersonModel> CreatePerson(PersonModel person);
        ServiceResponse<PersonModel> DeletePerson(string id);
        ServiceResponse<List<PersonModel>> GetAllPersons();
        //ServiceResponse<PersonModel> AddContact(PersonModel person);
        //ServiceResponse<PersonModel> DeleteContact(string id);
        ServiceResponse<PersonModel> GetPersonById(string id);
        ServiceResponse<ReportRequest> GetReportByLocation(ReportRequest reportRequest);
        //ServiceResponse<List<PersonModel>> GetAllPersonsGroupByLocation();
    }
}
