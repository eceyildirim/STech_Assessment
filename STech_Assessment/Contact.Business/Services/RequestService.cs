using Report.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Report.Business.Services
{
    public class RequestService : IRequestService
    {
        public HttpResponseMessage SendPostRequest(string url = "https://www.google.com/", string postData = "")
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);


            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            // List data response.
            var content1 = new StringContent(postData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("", content1).Result;
            if (response.IsSuccessStatusCode)
            {
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            // Make any other calls using HttpClient here.

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
            return response;
        }
    }
}
