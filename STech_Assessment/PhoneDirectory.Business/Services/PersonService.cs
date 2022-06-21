using Microsoft.AspNetCore.Http;
using PhoneDirectory.Business.Base;
using PhoneDirectory.Business.Interfaces;
using PhoneDirectory.Business.Models;
using PhoneDirectory.Business.Responses;
using PhoneDirectory.Entity.Models;
using PhoneDirectory.Resources;
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
            var personEntity = Mapper.Map<Person>(person);

            var createPersonRes = _personRepository.InsertOne(personEntity);

            if(!createPersonRes.Successed || createPersonRes.Result == null)
            {
                res.Code = StatusCodes.Status500InternalServerError;
                res.Message = SystemMessage.Feedback_UnexpectedError;
                res.Successed = false;
                res.Errors = createPersonRes.Message;
            }

            res.Result = Mapper.Map<PersonModel>(createPersonRes.Result);

            return res;
        }

        public ServiceResponse<PersonModel> DeletePerson(string id)
        {
            var res = new ServiceResponse<PersonModel>();

            #region [Validation]
            if (string.IsNullOrEmpty(id))
            {
                res.Code = StatusCodes.Status400BadRequest;
                res.Message = CustomMessage.PleaseFillInTheRequiredFields;
                res.Successed = false;

                return res;
             }
            #endregion

            #region [Get Person]
            var personRes = _personRepository.FindById(id);
            
            if(!personRes.Successed)
            {
                res.Code = StatusCodes.Status500InternalServerError;
                res.Message = SystemMessage.Feedback_UnexpectedError;
                res.Successed = false;

                return res;
            }
            
            if(personRes.Result == null)
            {
                res.Code = StatusCodes.Status400BadRequest;
                res.Message = CustomMessage.InvalidID;
                res.Successed = false;

                return res;
            }
            #endregion

            #region [Delete Person]
            var person = personRes.Result;
            person.DeletedAt = DateTime.UtcNow;

            var deletePersonRes = _personRepository.ReplaceOne(person);

            if(!deletePersonRes.Successed)
            {
                res.Code = StatusCodes.Status500InternalServerError;
                res.Message = SystemMessage.Feedback_UnexpectedError;
                res.Successed = false;

                return res;
            }
            #endregion

            res.Result = Mapper.Map<PersonModel>(person);

            return res;
        }

        public List<PersonModel> GetAllPersons()
        {
            var persons = _personRepository.FilterBy(x => x.DeletedAt == null).Result;

            return Mapper.Map<List<PersonModel>>(persons);
        }
    }
}
