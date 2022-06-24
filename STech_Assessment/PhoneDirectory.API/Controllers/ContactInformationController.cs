using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Business.Interfaces;
using PhoneDirectory.Business.Models;
using PhoneDirectory.Core.Requests;
using PhoneDirectory.Resources;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.API.Controllers
{
    [Route("api/v1/contactinformation")]
    [ApiController]
    public class ContactInformationController : BaseController<ContactInformationController>
    {
        private readonly IContactInformationService _contactInformationService;
        private readonly IPersonService _personService;
        private readonly IBus _bus;

        public ContactInformationController
            (
                IContactInformationService contactInformationService,
                IPersonService personService,
                IBus bus)
        {
            _contactInformationService = contactInformationService;
            _bus = bus;
            _personService = personService;
        }

        [HttpPost, Route("")]
        public IActionResult AddContact(ContactInformationModel contactInformationModel)
        {
            var contact = _contactInformationService.AddContact(contactInformationModel);

            if (!contact.Successed)
            {
                return APIResponse(contact);
            }

            return Ok(contact.Result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(string id)
        {
            var contact = _contactInformationService.DeleteContactById(id);

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

            //BURASI AÇILACAK
            //Uri uri = new Uri("rabbitmq://localhost/reportQueue");
            //var endPoint = await _bus.GetSendEndpoint(uri);
            //await endPoint.Send(reportRequest);


            var reportReq = new ReportRequest
            {
                Location = reportRequest.Location,
                ReportRequestDate = reportRequest.ReportRequestDate
            };


            //generate report by location
            var response = _contactInformationService.GetReportByLocation(reportReq);

            //send data
            //_queueService.SendToQueue(new QueueMessage { Message = reportRequest, To= "https://localhost:44381/api/v1/reports/insertreport" }, "insertqueue");

            //send response result by location
            //_queueService.SendToQueue(new QueueMessage { Message = response.Result, To = "https://localhost:44381/api/v1/reports/updatereport" }, "updatequeue");

            //if (!response.Successed)
            //{
            //    return APIResponse(response);
            // }

            return Ok();
        }
    }
}
