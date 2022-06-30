using PhoneDirectory.Business.Base;
using PhoneDirectory.Core;
using System;
using System.Collections.Generic;
using System.Text;
using static PhoneDirectory.Entity.Models.Person;

namespace PhoneDirectory.Business.Models
{
    public class PersonModel : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
    }
}
