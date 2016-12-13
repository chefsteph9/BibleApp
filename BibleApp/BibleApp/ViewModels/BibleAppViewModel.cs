using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BibleLibrary;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;


namespace BibleApp
{
    public class BibleAppViewModel : ViewModelBase
    {
        #region Members
        private List<string> m_errorMessages;

        private static HttpClient m_client;
        private User m_user;
        private Topic m_currentTopic;

        private ObservableCollection<Topic> m_topics;
        private ObservableCollection<Belief> m_beliefs;
        private ObservableCollection<Verse> m_verses;
        private IDictionary<string, object> m_properties;


        #endregion

        #region Constructors

        public BibleAppViewModel()
        {
            m_errorMessages = new List<string>();
            m_properties = App.Current.Properties;

            InitializeClientAndUser().Wait();
            UpdateTopics().Wait();
        }

        #endregion

        #region Properties

        public User User
        {
            set { SetProperty(ref m_user, value); }
            get { return m_user; }
        }

        public ObservableCollection<Topic> Topics
        {
            set { SetProperty(ref m_topics, value); }
            get { return m_topics; }
        }

        public ObservableCollection<Belief> Beliefs
        {
            set { SetProperty(ref m_beliefs, value); }
            get { return m_beliefs; }
        }

        public ObservableCollection<Verse> Verses
        {
            set { SetProperty(ref m_verses, value); }
            get { return m_verses; }
        }

        #endregion

        #region Methods

        #region Private Methods

        private async Task GetDataFromServer()
        {
            Topics = await GetAllTopics().ConfigureAwait(false);
        }

        private async Task InitializeClientAndUser()
        {
            try
            {
                // Initialize HTTP Client
                m_client = new HttpClient();
                m_client.BaseAddress = new Uri("http://localhost:8000/");
                m_client.DefaultRequestHeaders.Accept.Clear();
                m_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                m_user = new User(m_client);

                // Check if there is user information stored
                List<string> keysNeeded = new List<string>() { "userName", "userPassword", "userEmail" };
                bool userKeysStored = true;
                foreach (string key in keysNeeded)
                {
                    if (!m_properties.ContainsKey(key))
                        userKeysStored = false;
                }

                if (userKeysStored)
                {
                    m_user.name = m_properties["userName"].ToString();
                    m_user.password = m_properties["userPassword"].ToString();
                    m_user.email = m_properties["userEmail"].ToString();

                    var tokenResponse = await User.LoginUserAsync(m_user);
                    m_user.token = tokenResponse.ToString();
                }
                else
                {
                    m_user.name = "Eric";
                    m_user.password = "08frontier";
                    m_user.email = "test@test.com";

                    var tokenResponse = await User.LoginUserAsync(m_user).ConfigureAwait(false);
                    m_user.token = tokenResponse.ToString();
                }

                //Set the token in the HTTP header to allow the user to access other parts of the API
                m_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_user.token);
                Topic topic = new Topic(m_client);
                Belief belief = new Belief(m_client);
                Verse verse = new Verse(m_client);
            }

            catch (Exception e)
            {
                m_errorMessages.Add(e.Message);
            }
        }

        private async Task<ObservableCollection<Topic>> GetAllTopics()
        {
            ObservableCollection<Topic> returnList = new ObservableCollection<Topic>();
            try
            {
                string topicList = await Topic.ListTopicsAsync().ConfigureAwait(false);
                JArray data = JArray.Parse(topicList);

                foreach (JToken entry in data)
                {
                    returnList.Add(new Topic(m_client)
                    {
                        topicText = entry["topicText"].ToString(),
                        ID = int.Parse(entry["id"].ToString()),
                        userID = int.Parse(entry["user_id"].ToString())
                    });
                }
            }
            catch(Exception e)
            {
                m_errorMessages.Add(e.Message);
            }

            return returnList;
        }

        private async Task<ObservableCollection<Belief>> GetAllBeliefs(Topic topic)
        {
            ObservableCollection<Belief> returnList = new ObservableCollection<Belief>();

            try
            {
                string beliefList = await Belief.ListBeliefsAsync(topic.ID).ConfigureAwait(false);
                JArray data = JArray.Parse(beliefList);

                foreach (JToken entry in data)
                {
                    returnList.Add(new Belief(m_client)
                    {
                        beliefText = entry["beliefText"].ToString(),
                        userID = int.Parse(entry["user_id"].ToString()),
                        topicID = int.Parse(entry["topic_id"].ToString()),
                        ID = int.Parse(entry["id"].ToString())
                    });
                }
            }
            catch (Exception e)
            {
                m_errorMessages.Add(e.Message);
            }
            return returnList;
        }

        private async Task<ObservableCollection<Verse>> GetAllVerses(Belief belief)
        {
            ObservableCollection<Verse> returnList = new ObservableCollection<Verse>();

            try
            {
                string verseList = await Verse.ListVersesAsync(belief.topicID, belief.ID).ConfigureAwait(false);
                JArray data = JArray.Parse(verseList);

                foreach (JToken entry in data)
                {
                    returnList.Add(new Verse(m_client)
                    {
                        userID = int.Parse(entry["user_id"].ToString()),
                        topicID = belief.topicID,
                        beliefID = int.Parse(entry["belief_id"].ToString()),
                        ID = int.Parse(entry["id"].ToString()),

                        verse = entry["verse"].ToString(),
                        book = int.Parse(entry["book"].ToString()),
                        chapter = int.Parse(entry["chapter"].ToString()),
                        verseStart = int.Parse(entry["verseStart"].ToString()),
                        verseEnd = int.Parse(entry["verseEnd"].ToString())
                    });
                }
            }
            catch(Exception e)
            {
                m_errorMessages.Add(e.Message);
            }

            return returnList;
        }

        #endregion

        #region Public Methods

        public async Task UpdateTopics()
        {
            Topics = await GetAllTopics().ConfigureAwait(false);
        }

        public async Task UpdateBeliefs(Topic topic)
        {
            Beliefs = await GetAllBeliefs(topic).ConfigureAwait(false);
        }

        public async Task UpdateVerses(Belief belief)
        {
            Verses = await GetAllVerses(belief);
        }

        #endregion

        #endregion
    }
}
