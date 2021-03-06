using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("/api/v1");
        }

        [HttpGet, Route("/api/v1")]
        public string API()
        {

            return "SeturTech Assessment Phone Directory Rest API - v1";
        }
    }
}
