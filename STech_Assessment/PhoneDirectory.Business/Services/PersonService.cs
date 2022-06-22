using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using PhoneDirectory.Business.Base;
using PhoneDirectory.Business.Interfaces;
using PhoneDirectory.Business.Models;
using PhoneDirectory.Business.Responses;
using PhoneDirectory.Core;
using PhoneDirectory.DAL.Interfaces;
using PhoneDirectory.Entity.Models;
using PhoneDirectory.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        //public ServiceResponse<PersonModel> AddContact(PersonModel model)
        //{
        //    var response = new ServiceResponse<PersonModel> { };

        //    var findPerson = _personRepository.FindById(model.UUID);
        //    if (!findPerson.Successed || findPerson.Result == null)
        //    {
        //        response.Code = StatusCodes.Status404NotFound;
        //        response.Message = CustomMessage.UserNotFound;
        //        response.Successed = false;
        //        response.Errors = findPerson.Message;
        //        return response;
        //    }

        //    var person = findPerson.Result;

        //    person.InformationType = model.InformationType;
        //    person.InformationContent = model.InformationContent;
        //    person.UpdatedAt = DateTime.UtcNow;

        //    var updatePerson = _personRepository.ReplaceOne(person);
        //    if (!updatePerson.Successed)
        //    {
        //        response.Code = StatusCodes.Status500InternalServerError;
        //        response.Message = SystemMessage.Feedback_UnexpectedError;
        //        response.Successed = false;
        //        response.Errors = updatePerson.Message;
        //        return response;
        //    }

        //    response.Result = Mapper.Map<PersonModel>(updatePerson.Result);
        //    return response;
        //}

        //public ServiceResponse<PersonModel> DeleteContact(string id)
        //{
        //    var res = new ServiceResponse<PersonModel> { };

        //    #region [Validation]
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        res.Code = StatusCodes.Status400BadRequest;
        //        res.Message = CustomMessage.PleaseFillInTheRequiredFields;
        //        res.Successed = false;

        //        return res;
        //    }

        //    #endregion

        //    #region [Get Person]
        //    var personRes = _personRepository.FindById(id);
        //    if (!personRes.Successed)
        //    {
        //        res.Code = StatusCodes.Status500InternalServerError;
        //        res.Message = SystemMessage.Feedback_UnexpectedError;
        //        res.Successed = false;

        //        return res;
        //    }

        //    if (personRes.Result == null)
        //    {
        //        res.Code = StatusCodes.Status400BadRequest;
        //        res.Message = CustomMessage.UserNotFound;
        //        res.Successed = false;

        //        return res;
        //    }
        //    #endregion

        //    #region [Delete Contact]
        //    var person = personRes.Result;

        //    person.InformationType = ContactInformationType.NotSelect;
        //    person.InformationContent = null;

        //    var deleteRes = _personRepository.ReplaceOne(person);
        //    if(!deleteRes.Successed)
        //    {
        //        res.Code = StatusCodes.Status500InternalServerError;
        //        res.Message = SystemMessage.Feedback_UnexpectedError;
        //        res.Errors = deleteRes.Message;
        //        res.Successed = false;

        //        return res;
        //    }
        //    #endregion

        //    res.Result = Mapper.Map<PersonModel>(deleteRes.Result);

        //    return res;
        //}

        public ServiceResponse<PersonModel> GetPersonById(string id)
        {
            var res = new ServiceResponse<PersonModel> { };

            var lookedUp = _personRepository.Aggregate()
                .Lookup<Person, PersonLookedUp>("contact_informations", "ContactInformationIds", "uuid", "ContactInformations")
                .Match(x => x.UUID == id)
                .SortByDescending(x => x.Name)
                .ToList();

            var person = lookedUp.FirstOrDefault();

            res.Result = Mapper.Map<PersonModel>(person);

            if (res == null)
            {
                res.Code = StatusCodes.Status404NotFound;
                res.Message = CustomMessage.UserNotFound;
                res.Successed = false;

                return res;
            }

            return res;
        }

        public ServiceResponse<List<PersonModel>> GetAllPersonsGroupByLocation()
        {
            //var persons = _personRepository.FilterBy(x => x.PersonLocation == person.Location).Result;
            //return Mapper.Map<List<PersonModel>>(persons);
            var res = new ServiceResponse<List<PersonModel>> { };

            //var persons = _personRepository.Aggregate()
            //        .Match(x => x.DeletedAt == null)
            //        .Group(x => x.PersonLocation, g => new { Key = g.Key, Count = g.Count() })
            //        .ToList();

            //if(persons == null)
            //{
            //    res.Code = StatusCodes.Status404NotFound;
            //    res.Message = CustomMessage.UserNotFound;
            //    res.Successed = false;

            //    return res;
            //}

            //res.Result = Mapper.Map<List<PersonModel>>(persons);

            return res;
        }

        public ServiceResponse<List<PersonModel>> GetAllPersons()
        {
            var res = new ServiceResponse<List<PersonModel>>();

            #region [Get Data]

            var match = Builders<Person>.Filter.Where(x => x.DeletedAt == null);

            var persons = _personRepository.Aggregate()
                                           .Match(match)
                                           .ToList();

            #endregion

            //var persons = _personRepository.FilterBy(x => x.DeletedAt == null).Result;

            res.Result = Mapper.Map<List<PersonModel>>(persons);

            return res;
        }
    }
}
