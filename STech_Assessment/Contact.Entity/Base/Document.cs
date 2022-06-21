﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Report.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Entity.Base
{
    [BsonIgnoreExtraElements]
    public class Document : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UUID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public EntityStatus Status { get; set; }

    }
}