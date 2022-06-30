using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhoneDirectory.Business.Interfaces;
using PhoneDirectory.Business.Models;
using PhoneDirectory.Business.Validators;
using PhoneDirectory.Core.Requests;
using PhoneDirectory.Resources;
using Shared.Models;
using Shared.Models.Interfaces;
using Shared.Models.MQ;
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
        public readonly IBus _bus;
        public readonly IRabbitMQService _rabbitMQService;
        public readonly PersonValidator personValidator = new PersonValidator();

        public PersonController(IPersonService personService, IBus bus, IRabbitMQService rabbitMQService)
        {
            _personService = personService;
            _bus = bus;
            _rabbitMQService = rabbitMQService;
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
                return BadRequest(res);
            }

            return Ok(res.Result);

        }

        [HttpGet, Route("")]
        public IActionResult GetAllPersons()
        {
            var res = _personService.GetAllPersons();

            if(res == null)
            {
                return NotFound(res);
            }

            return Ok(res.Result);
        }

        [HttpGet, Route("{id}")]
        public IActionResult GetPersonById(string id)
        {
            var res = _personService.GetPersonById(id);

            if(res == null)
            {
                return NotFound(res);
            }

            if(!res.Successed)
            {
                return BadRequest(res);
            }

            return Ok(res.Result);
        }

        [HttpPost, Route("")]
        public IActionResult AddContact(PersonModel personModel)
        {
            var contact = _personService.AddContact(personModel);

            if (!contact.Successed)
            {
                return APIResponse(contact);
            }

            return Ok(contact.Result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(string id)
        {
            var contact = _personService.DeleteContact(id);

            if(contact == null)
            {
                return NotFound(contact);
            }

            if (!contact.Successed)
            {
                return BadRequest(contact);
            }

            return Ok(contact.Result);
        }

        [HttpGet, Route("personreports/{location}")]
        public async Task<IActionResult> GetReportByLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return BadRequest(new { Message = CustomMessage.PleaseFillInTheRequiredFields });
            }

            var reportRequest = new SharedReport
            {
                Location = location,
                ReportRequestDate = DateTime.UtcNow,
                ReportStatus = (ReportStatus)Core.ReportStatus.Prepare
            };


            await _rabbitMQService.SendMessages(reportRequest);

            var reportReq = new ReportRequest
            {
                Location = reportRequest.Location,
                ReportRequestDate = reportRequest.ReportRequestDate
            };

            //generate report by location
            var response = _personService.GetReportByLocation(reportReq);

            reportRequest.Location = reportReq.Location;
            reportRequest.ReportRequestDate = reportReq.ReportRequestDate;
            reportRequest.NumberOfRegisteredPersons = response.Result.NumberOfRegisteredPersons;
            reportRequest.NumberOfRegisteredPhones = response.Result.NumberOfRegisteredPhones;
            reportRequest.ReportStatus = (ReportStatus)response.Result.ReportStatus;

            //send report 
            await _rabbitMQService.SendMessages(reportRequest);


            return Ok();
        }
    }
}
