using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using BibleLibrary.Classes;

namespace BibleLibrary
{
    //public static async Task<HttpResponseMessage> PostAsJsonAsync<TModel>(this HttpClient client, string requestUrl, TModel model)
    //{
    //    var serializer = new JavaScriptSerializer();
    //    var json = serializer.Serialize(model);
    //    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
    //    return await client.PostAsync(requestUrl, stringContent);

    public class User
    {
        #region Members

        public string id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string token { get; set; }
        private static HttpClient m_client;

        #endregion

        #region Constructors

        public User(HttpClient client)
        {
            m_client = client;
        }

        #endregion

        #region Methods

        //This will return a JSON token if the user successfully signs up;
        //Requires a unique name, unique email, and a password;
        //HTTP method: POST
        public static async Task<Object> CreateUserAsync(User user)
        {
            HttpResponseMessage response = await m_client.PostAsJsonAsync("api/auth/signup", user).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            JObject token = JObject.Parse(data);


            // return token string
            return token["token"];
        }

        //This should return a new token if the user has been logged out or the previous token expired;
        //The API requires either the user's name or email and also the password;
        //HTTP method: POST
        public static async Task<Object> LoginUserAsync(User user)
        {
            HttpResponseMessage response = await m_client.PostAsJsonAsync("api/auth/login", user).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            JObject token = JObject.Parse(data);


            // return token string
            return token["token"];
        }

        #endregion

    }
}
