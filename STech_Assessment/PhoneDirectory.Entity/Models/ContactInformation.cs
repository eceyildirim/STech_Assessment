using PhoneDirectory.Entity.Base;
using System;
using PhoneDirectory.Core;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Document = PhoneDirectory.Entity.Base.Document;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PhoneDirectory.Entity.Models
{
    [BsonCollection("contact_informations")]
    public class ContactInformation : Document
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string PersonId { get; set; }
        public ContactInformationType ContactInformationType { get; set; }
        public string ContactInformationContent { get; set; }
    }

    public class ContanctInformationLookedUp : ContactInformation
    {
        public Person Person { get; set; }
    }
}
