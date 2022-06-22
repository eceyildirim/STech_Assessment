using PhoneDirectory.Core;
using PhoneDirectory.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Entity.Models
{
    [BsonCollection("persons")]
    public class Person : Document
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ContactInformationType InformationType { get; set; }
        public string InformationContent { get; set; }
    }
}
