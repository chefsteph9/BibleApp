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
    public class AppViewModel : ViewModelBase
    {
        #region Members
        private TopicViewModel m_topicViewModel;

        private ObservableCollection<string> m_errorMessages;

        private IDictionary<string, object> m_properties;
        private User m_user;
        private HttpClient m_client;

        private int m_selectedTopic;
               
        #endregion

        #region Constructors

        public AppViewModel()
        {
            m_errorMessages = new ObservableCollection<string>();
            m_properties = App.Current.Properties;

            m_selectedTopic = 0;

            InitializeClientAndUser().Wait();

        }

        #endregion

        #region Properties

        public ObservableCollection<string> ErrorMessages
        {
            set { SetProperty(ref m_errorMessages, value); }
            get { return m_errorMessages; }
        }

        public User User
        {
            set { SetProperty(ref m_user, value); }
            get { return m_user; }
        }

        public HttpClient Client
        {
            set { SetProperty(ref m_client, value); }
            get { return m_client; }
        }

        #endregion

        #region Methods

        #region Private Methods

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
            }

            catch (Exception e)
            {
                m_errorMessages.Add(e.Message);
            }
        }


        #endregion

        #endregion
    }
}
