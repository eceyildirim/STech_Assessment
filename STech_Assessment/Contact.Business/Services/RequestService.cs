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
            //QueueMessage queueMessage = new QueueMessage();
            //queueMessage.To = "google.com/uptadeuser";
            //queueMessage.WebHookActionId = "f0d87f48-60ee-4991-8c46-caf97";
            // List data response.
            var content1 = new StringContent(postData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("", content1).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                //var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                //var dataObjects = JsonConvert.DeserializeObject<IEnumerable<DataObject>>(response.Content.ToString());
                //foreach (var d in dataObjects)
                //{
                //    Console.WriteLine("{0}", d.Name);
                //}
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
