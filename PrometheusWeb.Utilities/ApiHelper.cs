using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PrometheusWeb.Utilities
{
    //Api Helper Class to help with making requests to WebApi.
    public class ApiHelper 
    {
        const string BaseUrl = "https://localhost:44375/";
        public static HttpClient ApiClient { get; set; }

        

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            //Passing service base url  
            ApiClient.BaseAddress = new Uri(BaseUrl);

            ApiClient.DefaultRequestHeaders.Clear();
            //Define request data format  
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        /*public void Dispose()
        {
            ApiClient.Dispose();
        }*/
    }
}
