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

    public class Verse
    {
        #region Members

        public int userID { set; get; }
        public int topicID { get; set; }
        public int beliefID { set; get; }
        public int ID { set; get; }

        public string verse { get; set; }
        public int book { get; set; }
        public int chapter { get; set; }
        public int verseStart { get; set; }
        public int verseEnd { get; set; }
        private static HttpClient m_client;

        #endregion

        #region Constructors

        public Verse(HttpClient client)
        {
            m_client = client;
        }

        public Verse(Belief belief)
        {
            m_client = belief.Client;
            topicID = belief.topicID;
        }

        #endregion

        #region Verse Methods

        //--------------------------------- Verse Methods ------------------------------------------//


        //This will attempt to create a new verse and return success/error message;
        //The API requires the verse, book, chapter, verseStart, and verseEnd field;
        //HTTP method: POST
        public static async Task<Object> CreateVerseAsync(int topicID, int beliefID, Verse verse)
        {
            HttpResponseMessage response = await m_client.PostAsJsonAsync("api/topics/" + topicID + "/beliefs/" + beliefID + "/verses", verse).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);


            // return Success/Error message
            return data;
        }

        //This will attempt to retrieve all verses;
        //HTTP method: GET
        public static async Task<String> ListVersesAsync(int topicID, int beliefID)
        {
            HttpResponseMessage response = await m_client.GetAsync("api/topics/" + topicID + "/beliefs/" + beliefID + "/verses").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);


            // return Success/Error message
            return data;
        }

        //This will attempt to retrieve a specific verse;
        //HTTP method: GET
        public static async Task<Object> GetVerseAsync(int topicID, int beliefID, int verseID)
        {
            HttpResponseMessage response = await m_client.GetAsync("api/topics/" + topicID + "/beliefs/" + beliefID + "/verses" + verseID).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);


            // return Success/Error message
            return data;
        }

        //This will attempt to update a specific verse;
        //HTTP method: PUT
        public static async Task<Object> UpdateVerseAsync(int topicID, int beliefID, int verseID, Verse verse)
        {
            HttpResponseMessage response = await m_client.PutAsJsonAsync("api/topics/" + topicID + "/beliefs/" + beliefID + "/verses/" + verseID, verse).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);


            // return Success/Error message
            return data;
        }

        //This will attempt to delete a specific verse;
        //HTTP method: DELETE
        public static async Task<Object> DeleteVerseAsync(int topicID, int beliefID, int verseID)
        {
            HttpResponseMessage response = await m_client.DeleteAsync("api/topics/" + topicID + "/beliefs/" + beliefID + "/verses/" + verseID).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);


            // return Success/Error message
            return data;
        }

        #endregion
    }

}
