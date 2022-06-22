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

        //[HttpPut, Route("addcontact")]
        //public IActionResult AddContact(PersonModel person)
        //{
        //    var res = _personService.AddContact(person);

        //    if (!res.Successed)
        //    {
        //        return APIResponse(res);
        //    }

        //    return Ok(res.Result);
        //}

        //[HttpDelete, Route("contact/{id}")]
        //public IActionResult DeleteContact(string id)
        //{
        //    var res = _personService.DeleteContact(id);
        //    if (!res.Successed)
        //    {
        //        return APIResponse(res);
        //    }

        //    return Ok(res.Result);
        //}

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

        [HttpGet, Route("persons")]
        public IActionResult GetAllPersonsGroupByLocation()
        {
            var res = _personService.GetAllPersonsGroupByLocation();
            if(!res.Successed)
            {
                return APIResponse(res);
            }
            return Ok(res.Result);
        }
    }
}
