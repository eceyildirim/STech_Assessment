using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Business.Interfaces;
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

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

    }
}
