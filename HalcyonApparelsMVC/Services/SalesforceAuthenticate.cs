using HalcyonApparelsMVC.Interfaces;
using HalcyonApparelsMVC.Models;
using Newtonsoft.Json;

namespace HalcyonApparelsMVC.Services
{
    public class SalesforceAuthenticate: IAuthenticate
    {
        public string Authenticate()
        {

            RegisterConnection value = new RegisterConnection();
            ReturnClass? returnClass = new ReturnClass();

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://login.salesforce.com/");
            var request = new FormUrlEncodedContent(new Dictionary<string, string>
               {
                   {"grant_type","password" },
                   {"client_id",value.client_id },
                   {"client_secret",value.client_secret },
                   {"username",value.username },
                   {"password", value.password },

               });

            var Result = client.PostAsync("services/oauth2/token?", request).Result;


            if (Result.IsSuccessStatusCode)
            {
                var result = Result.Content.ReadAsStringAsync().Result;
                returnClass = JsonConvert.DeserializeObject<ReturnClass>(result);

                return returnClass.access_token;

            }
            return null;
        }
    }
}