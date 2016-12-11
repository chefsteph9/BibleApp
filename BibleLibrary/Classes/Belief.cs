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
    public class Belief
    {
        #region Members

        public string beliefText { get; set; }
        private static HttpClient m_client;

        #endregion

        #region Constructors

        public Belief(HttpClient client)
        {
            m_client = client;
        }

        #endregion

        #region Belief Methods

        //--------------------------------- Belief Methods ------------------------------------------//


        //This will attempt to create a new belief and return success/error message;
        //The API requires the beliefText field;
        //HTTP method: POST
        public static async Task<Object> CreateBeliefAsync(int topicID, Belief belief)
        {
            HttpResponseMessage response = await m_client.PostAsJsonAsync("api/topics/" + topicID + "/beliefs", belief);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();


            // return Success/Error message
            return data;
        }

        //This will attempt to retrieve all beliefs;
        //HTTP method: GET
        public static async Task<String> ListBeliefsAsync(int topicID)
        {
            HttpResponseMessage response = await m_client.GetAsync("api/topics/" + topicID + "/beliefs");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();


            // return Success/Error message
            return data;
        }

        //This will attempt to retrieve a specific belief;
        //HTTP method: GET
        public static async Task<Object> GetBeliefAsync(int topicID, int beliefID)
        {
            HttpResponseMessage response = await m_client.GetAsync("api/topics/" + topicID + "/beliefs/" + beliefID);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();


            // return Success/Error message
            return data;
        }

        //This will attempt to retrieve a update belief;
        //HTTP method: PUT
        public static async Task<Object> UpdateBeliefAsync(int topicID, int beliefID, Belief belief)
        {
            HttpResponseMessage response = await m_client.PutAsJsonAsync("api/topics/" + topicID + "/beliefs/" + beliefID, belief);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();


            // return Success/Error message
            return data;
        }

        //This will attempt to delete a specific belief;
        //HTTP method: DELETE
        public static async Task<Object> DeleteBeliefAsync(int topicID, int beliefID)
        {
            HttpResponseMessage response = await m_client.DeleteAsync("api/topics/" + topicID + "/beliefs/" + beliefID);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();


            // return Success/Error message
            return data;
        }

        #endregion
    }
}
