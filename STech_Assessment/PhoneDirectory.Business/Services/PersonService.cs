using Microsoft.AspNetCore.Http;
using PhoneDirectory.Business.Base;
using PhoneDirectory.Business.Interfaces;
using PhoneDirectory.Business.Models;
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
        public Responses.ServiceResponse<PersonModel> CreatePerson(PersonModel person)
        {
            throw new NotImplementedException();
        }

        public Responses.ServiceResponse<PersonModel> DeletePerson(string id)
        {
            throw new NotImplementedException();
        }

        public List<PersonModel> GetAllPersons()
        {
            throw new NotImplementedException();
        }
    }
}
