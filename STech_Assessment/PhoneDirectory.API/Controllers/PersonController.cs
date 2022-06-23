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
        private readonly IBus _bus;
        public readonly IPersonService _personService;
        public readonly IQueueService _queueService;
        //public readonly IPublishEndpoint _publishEndpoint; masstransit
        private readonly PersonValidator personValidator = new PersonValidator();

        public PersonController(IPersonService personService, 
            IQueueService queueService,
            IBus bus
           ) //IPublishEndpoint publishEndpoint
        {
            _personService = personService;
            _queueService = queueService;
            _bus = bus;
            //_publishEndpoint = publishEndpoint; masstransit
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




            //await _publishEndpoint.Publish<ReportModel>(reportRequest); mass transit


            //send data
            //_queueService.SendToQueue(new QueueMessage { Message = reportRequest, To= "https://localhost:44381/api/v1/reports/insertreport" }, "insertqueue");

            //generate report by location
           // var response = _personService.GetReportByLocation(reportRequest);

            //send response result by location
            //_queueService.SendToQueue(new QueueMessage { Message = response.Result, To = "https://localhost:44381/api/v1/reports/updatereport" }, "updatequeue");

            //if (!response.Successed)
            //{
            //    return APIResponse(response);
           // }

            return Ok();
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

        //[HttpGet, Route("persons")]
        //public IActionResult GetAllPersonsGroupByLocation()
        //{
        //    var res = _personService.GetAllPersonsGroupByLocation();
        //    if(!res.Successed)
        //    {
        //        return APIResponse(res);
        //    }
        //    return Ok(res.Result);
        //}
    }
}
