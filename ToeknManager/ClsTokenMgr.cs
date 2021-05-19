using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ToeknManager
{
    public class ClsTokenMgr
    {
        private static string Username = string.Empty;
        private static string Password = string.Empty;
        private static string baseAddress = "http://localhost:44382/";

        public static Token GetAccessToken(string username, string password)
        {
            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"grant_type", "password"},
                {"username", username},
                {"password", password},
                };
            var tokenResponse = client.PostAsync(baseAddress + "token", new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<Token>(JsonContent);
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            return token;
        }

        protected static void CallAPIResource(string AccessToken)
        {
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            var RequestBody = new Dictionary<string, string>
                {
                {"Parameter1", "value1"},
                {"Parameter2", "vakue2"},
                };
            var APIResponse = client.PostAsync(baseAddress + "api/test/method1", new FormUrlEncodedContent(RequestBody)).Result;

            if (APIResponse.IsSuccessStatusCode)
            {
                var JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                //Token Message = JsonConvert.DeserializeObject<Token>(JsonContent);  
                Console.WriteLine("APIResponse : " + JsonContent.ToString());
            }
            else
            {
                Console.WriteLine("APIResponse, Error : " + APIResponse.StatusCode);
            }
        }
    }
}

