using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Business.Base
{
    public class BaseModel
    {
        [JsonProperty(Order = -2)]
        public string UUID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Draft;

    }
}
