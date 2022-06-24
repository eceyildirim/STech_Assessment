using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PhoneDirectory.Business.Base;
using PhoneDirectory.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Business.Models
{
    public class ContactInformationModel : BaseModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string PersonId { get; set; }
        public ContactInformationType ContactInformationType { get; set; }
        public string ContactInformationContent { get; set; }

        #region [LookedUp Properties]
        public PersonModel Person { get; set; }
        #endregion
    }
}
