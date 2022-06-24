using MongoDB.Bson;
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
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        //public List<ObjectId> ContactInformationIds { get; set; } = new List<ObjectId> { };
    }

    //public class PersonLookedUp : Person
    //{
    //    public ICollection<ContactInformation> ContactInformations { get; set; } = new List<ContactInformation>();
    //}
}
