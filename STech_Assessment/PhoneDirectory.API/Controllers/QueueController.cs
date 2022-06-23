using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Business.Interfaces;
using PhoneDirectory.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.API.Controllers
{
    [Route("api/v1/queue")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        IQueueService _service;

        public QueueController(IQueueService service)
        {
            _service = service;
        }

        [HttpGet]
        public void GetQueue()
        {
            _service.CreateQueue("mailqueue");
        }


        [HttpGet("send")]
        public void SendMsg(string message = "Hello")
        {
            //_service.SendToQueue(new QueueMessage { Message = message, To = "https://localhost:44381/api/v1/reports/" }, "mailqueue");
        }

        //[HttpPost("mail")]
        //public void SendMail(string message = "Hello")
        //{
        //    SendMailRequest request = new SendMailRequest(_emailSettings);
        //    _mailService.SendMail(request);
        //}
    }
}
