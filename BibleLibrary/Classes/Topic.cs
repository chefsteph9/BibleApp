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
    public class Topic
    {
        #region Members

        public string topicText { get; set; }
        private static HttpClient m_client;

        #endregion

        #region Constructors

        public Topic(HttpClient client)
        {
            m_client = client;
        }

        #endregion

        #region Methods

        //--------------------------------- Topic Methods ------------------------------------------//


        //This will attempt to create a new topic and return success/error message;
        //The API requires the topicText field;
        //HTTP method: POST
        public static async Task<Object> CreateTopicAsync(Topic topic)
        {
            HttpResponseMessage response = await m_client.PostAsJsonAsync("api/topics", topic).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);


            // return Success/Error message
            return data;
        }

        //This will attempt to retrieve all topics;
        //HTTP method: GET
        public static async Task<String> ListTopicsAsync()
        {
            HttpResponseMessage response = await m_client.GetAsync("api/topics").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);


            // return Success/Error message
            return data;
        }

        //This will attempt to retrieve a specific topic;
        //HTTP method: GET
        public static async Task<Object> GetTopicAsync(int id)
        {
            HttpResponseMessage response = await m_client.GetAsync("api/topics/" + id).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);


            // return Success/Error message
            return data;
        }

        //This will attempt to retrieve a specific topic;
        //HTTP method: PUT
        public static async Task<Object> UpdateTopicAsync(int id, Topic topic)
        {
            HttpResponseMessage response = await m_client.PutAsJsonAsync("api/topics/" + id, topic).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);


            // return Success/Error message
            return data;
        }

        //This will attempt to delete a specific topic;
        //HTTP method: DELETE
        public static async Task<Object> DeleteTopicAsync(int id)
        {
            HttpResponseMessage response = await m_client.DeleteAsync("api/topics/" + id).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);


            // return Success/Error message
            return data;
        }

        #endregion

    }
}
