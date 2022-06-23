using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhoneDirectory.Business.Interfaces;
using PhoneDirectory.Business.Models;
using PhoneDirectory.Business.Validators;
using PhoneDirectory.Core.Requests;
using PhoneDirectory.Resources;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.API.Controllers
{
    [Route("api/v1/persons")]
    [ApiController]
    public class PersonController : BaseController<PersonController>
    {
        public readonly IPersonService _personService;
        public readonly IQueueService _queueService;
        private readonly PersonValidator personValidator = new PersonValidator();

        public PersonController(IPersonService personService, 
            IQueueService queueService)
        {
            _personService = personService;
            _queueService = queueService;
        }

        [HttpPost, Route("create")]
        public IActionResult CreatePerson(PersonModel person)
        {
            var validationResult = personValidator.Validate(person);

            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            var created = _personService.CreatePerson(person);

            return Ok(created);
        }

        [HttpDelete("deleteperson/{id}")]
        public IActionResult DeletePersonById(string id)
        {
            var res = _personService.DeletePerson(id);
            if (!res.Successed)
            {
                return APIResponse(res);
            }

            return Ok(res.Result);

        }

        [HttpGet, Route("")]
        public IActionResult GetAllPersons()
        {
            return Ok(_personService.GetAllPersons());
        }

        [HttpGet, Route("{id}")]
        public IActionResult GetPersonById(string id)
        {
            var res = _personService.GetPersonById(id);
            if(!res.Successed)
            {
                return APIResponse(res);
            }

            return Ok(res.Result);
        }
    }
}
