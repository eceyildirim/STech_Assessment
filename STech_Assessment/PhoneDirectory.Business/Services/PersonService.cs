using Microsoft.AspNetCore.Http;
using PhoneDirectory.Business.Base;
using PhoneDirectory.Business.Interfaces;
using PhoneDirectory.Business.Models;
using PhoneDirectory.Business.Responses;
using PhoneDirectory.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static PhoneDirectory.DAL.Interfaces.IMongoRepository;

namespace PhoneDirectory.Business.Services
{
    public class PersonService : BaseService<PersonService>, IPersonService
    {
        private readonly IMongoRepository<Person> _personRepository;

        public PersonService(
            IMongoRepository<Person> personRepository,
            IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
            _personRepository = personRepository;
        }
        public ServiceResponse<PersonModel> CreatePerson(PersonModel person)
        {
            var res = new ServiceResponse<PersonModel>();

            return res;
        }

        public ServiceResponse<PersonModel> DeletePerson(string id)
        {
            var res = new ServiceResponse<PersonModel>();

            return res;
        }

        public List<PersonModel> GetAllPersons()
        {
            throw new NotImplementedException();
        }
    }
}
