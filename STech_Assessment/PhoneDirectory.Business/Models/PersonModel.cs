using PhoneDirectory.Business.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Business.Models
{
    public class PersonModel : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
    }
}
