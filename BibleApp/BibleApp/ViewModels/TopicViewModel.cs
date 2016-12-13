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
    class TopicViewModel : ViewModelBase
    {
        #region Members

        private HttpClient m_client;

        private AppViewModel m_appViewModel;
        private BeliefViewModel m_beliefViewModel;

        private ObservableCollection<Topic> m_topics;

        private ObservableCollection<string> m_errorMessages;

        private int m_selectedTopic;

        #endregion

        #region Constructors

        public TopicViewModel(HttpClient client)
        {
            m_client = client;


        }

        #endregion

        #region Properties

        public AppViewModel AppViewModel
        {
            set { SetProperty(ref m_appViewModel, value); }
            get { return m_appViewModel; }
        }

        public BeliefViewModel BeliefViewModel
        {
            set { SetProperty(ref m_beliefViewModel, value); }
            get { return m_beliefViewModel; }
        }

        public ObservableCollection<Topic> Topics
        {
            set { SetProperty(ref m_topics, value); }
            get { return m_topics; }
        }

        public int SelectedTopic
        {
            set { SetProperty(ref m_selectedTopic, value); }
            get { return m_selectedTopic; }
        }

        #endregion

        #region Methods

        private async Task InitializeTopics()
        {
            Topics = await GetAllTopics();
        }

        public async Task<ObservableCollection<Topic>> GetAllTopics()
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
            catch (Exception e)
            {
                m_errorMessages.Add(e.Message);
            }

            return returnList;
        }

        private async Task<List<Belief>> GetAllBeliefs()
        {
            List<Belief> returnList = new List<Belief>();

            try
            {
                string beliefList = await Belief.ListBeliefsAsync(1).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                m_errorMessages.Add(e.Message);
            }
            return returnList;
        }

        #endregion

    }
}
