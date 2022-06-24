using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Core.Requests
{
    public class BaseQueryRequest
    {
        public string Id { get; set; }

        public int PageSize { get; set; } = 100;

        public int Page { get; set; } = 1;

        public string Search { get; set; }

        public DateTime? DateFilter { get; set; }

    }
}