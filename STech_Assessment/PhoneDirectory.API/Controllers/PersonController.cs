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
        private readonly IBus _bus;
        private readonly PersonValidator personValidator = new PersonValidator();

        public PersonController(IPersonService personService, 
            IQueueService queueService, IBus bus)
        {
            _personService = personService;
            _queueService = queueService;
            _bus = bus;
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

            if (!contact.Successed)
            {
                return APIResponse(contact);
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

            Uri uri = new Uri("rabbitmq://localhost/reportQueue");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(reportRequest);

            var reportReq = new ReportRequest
            {
                Location = reportRequest.Location,
                ReportRequestDate = reportRequest.ReportRequestDate
            };

            //generate report by location
            var response = _personService.GetReportByLocation(reportReq);

            //send report 
            await endPoint.Send(response);

            return Ok();
        }
    }
}
