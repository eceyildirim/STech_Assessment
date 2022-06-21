using PhoneDirectory.Business.Models;
using PhoneDirectory.Business.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Business.Interfaces
{
    public interface IPersonService
    {
        ServiceResponse<PersonModel> CreatePerson(PersonModel person);
        ServiceResponse<PersonModel> DeletePerson(string id);
        List<PersonModel> GetAllPersons();
    }
}
