using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Windows.Media.Animation;

namespace Projekt1
{
    class API
    {
        public async static  Task<String> GetAPI()
        {
            var client = new HttpClient();

            // specify the API endpoint you want to call
            var url = "http://www.eskmenfocsanak.hu/r%C3%A1di%C3%B3/api.php";

            // create a dictionary to hold the request headers
            var headers = new Dictionary<string, string>();

            // add the headers to the dictionary
            headers.Add("header1", "value1");
            headers.Add("header2", "value2");

            // create the request body as a string
            var body = "{\"key1\":\"value1\",\"key2\":\"value2\"}";
            Console.WriteLine(body);
            // create a new HttpRequestMessage object with the specified method, url, and body
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(body)
            };
            Console.WriteLine(request);

            // add the headers to the request
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            // make the API call using the SendAsync method of the HttpClient
            var response = await client.SendAsync(request);

            // check the status code of the response to make sure the call was successful
            if (response.IsSuccessStatusCode)
            {
                // if the call was successful, read the response content
                var content = await response.Content.ReadAsStringAsync();
                return content;
                // do something with the response content
                // ...
            }
            return null;
        }
    }
}