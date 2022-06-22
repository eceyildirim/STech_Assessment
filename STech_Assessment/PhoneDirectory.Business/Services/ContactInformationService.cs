using Microsoft.AspNetCore.Http;
using PhoneDirectory.Business.Base;
using PhoneDirectory.Business.Interfaces;
using PhoneDirectory.Business.Models;
using PhoneDirectory.Business.Responses;
using PhoneDirectory.Business.Validators;
using PhoneDirectory.DAL.Interfaces;
using PhoneDirectory.Entity.Models;
using PhoneDirectory.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Business.Services
{
    public class ContactInformationService : BaseService<ContactInformationService>, IContactInformationService
    {
        private readonly IMongoRepository<Person> _personRepository;
        private readonly IMongoRepository<ContactInformation> _contactInformationRepository;
        private readonly ContactInformationValidator contactInformationValidator = new ContactInformationValidator();
        public ContactInformationService(IMongoRepository<Person> personRepository, IMongoRepository<ContactInformation> contactInformationRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _personRepository = personRepository;
            _contactInformationRepository = contactInformationRepository;
        }
        public ServiceResponse<ContactInformationModel> AddContact(ContactInformationModel contactInformationModel)
        {
            var res = new ServiceResponse<ContactInformationModel> { };

            #region [Validate]
            var valResult = contactInformationValidator.Validate(contactInformationModel);
            if (!valResult.IsValid)
            {
                res.Code = StatusCodes.Status400BadRequest;
                res.Message = CustomMessage.PleaseFillInTheRequiredFields;
                res.Successed = false;

                return res;
            }

            //Check person
            var person = _personRepository.FindById(contactInformationModel.PersonId);
            if (!person.Successed)
            {
                res.Code = StatusCodes.Status500InternalServerError;
                res.Message = SystemMessage.Feedback_UnexpectedError;
                res.Successed = false;

                return res;
            }
            if (person.Result == null)
            {
                res.Code = StatusCodes.Status400BadRequest;
                res.Message = CustomMessage.UserNotFound;
                res.Successed = false;

                return res;
            }
            #endregion

            var contactEntity = Mapper.Map<ContactInformation>(contactInformationModel);
            contactEntity.ContactInformationType = contactInformationModel.ContactInformationType;
            contactEntity.ContactInformationContent = contactInformationModel.ContactInformationContent;

            var createdContact = _contactInformationRepository.InsertOne(contactEntity);

            if (!createdContact.Successed)
            {
                res.Code = StatusCodes.Status500InternalServerError;
                res.Message = SystemMessage.Feedback_UnexpectedError;
                res.Successed = false;

                return res;
            }

            res.Result = Mapper.Map<ContactInformationModel>(createdContact.Result);

            return res;
        }

        public ServiceResponse<ContactInformationModel> DeleteContactById(string id)
        {
            var res = new ServiceResponse<ContactInformationModel> { };


            #region [ Validate ]

            if (string.IsNullOrEmpty(id))
            {
                res.Code = StatusCodes.Status400BadRequest;
                res.Message = CustomMessage.PleaseFillInTheRequiredFields;
                res.Successed = false;

                return res;
            }

            #endregion

            #region [ Get Related Data ]

            var contactInformation = _contactInformationRepository.FindOne(x => x.UUID == id);

            if (!contactInformation.Successed)
            {
                res.Code = StatusCodes.Status500InternalServerError;
                res.Message = SystemMessage.Feedback_UnexpectedError;
                res.Successed = false;

                return res;
            }

            if (contactInformation.Result == null)
            {
                res.Code = StatusCodes.Status400BadRequest;
                res.Message = CustomMessage.ContactInformationNotFound;
                res.Successed = false;

                return res;
            }

            res.Result = Mapper.Map<ContactInformationModel>(contactInformation.Result);

            #endregion

            var deleteRes = _contactInformationRepository.DeleteById(id);

            if (!deleteRes.Successed)
            {

                res.Code = StatusCodes.Status500InternalServerError;
                res.Message = SystemMessage.Feedback_UnexpectedError;
                res.Successed = false;

                return res;
            }

            return res;
        }
    }
}
