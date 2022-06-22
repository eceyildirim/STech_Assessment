using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Business.Interfaces;
using PhoneDirectory.Business.Models;
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

        public ContactInformationController(IContactInformationService contactInformationService, IPersonService personService)
        {
            _contactInformationService = contactInformationService;
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
    }
}
