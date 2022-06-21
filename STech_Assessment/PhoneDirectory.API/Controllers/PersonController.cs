using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Business.Interfaces;
using PhoneDirectory.Business.Models;
using PhoneDirectory.Business.Validators;
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
        private readonly PersonValidator personValidator = new PersonValidator();

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost, Route("create")]
        public IActionResult CreateUser(PersonModel person)
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
        public IActionResult GetAllUsers()
        {
            return Ok(_personService.GetAllPersons());
        }
    }
}
