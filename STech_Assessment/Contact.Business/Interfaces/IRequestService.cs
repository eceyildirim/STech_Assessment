using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Report.Business.Interfaces
{
    public interface IRequestService
    {
        HttpResponseMessage SendPostRequest(string url, string postData);
    }
}
