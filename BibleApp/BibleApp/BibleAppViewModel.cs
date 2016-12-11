using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BibleLibrary;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BibleApp
{
    class BibleAppViewModel : INotifyPropertyChanged
    {
        #region Members

        public event PropertyChangedEventHandler PropertyChanged;
        private string m_userName;
        private string m_userPassword;
        private string m_userEmail;
        private string m_userToken;
        private static HttpClient m_client;
        private User m_user;
        private List<string> m_errorMessages;
        private IDictionary<string, object> m_properties;


        #endregion

        #region Constructors

        public BibleAppViewModel(IDictionary<string, object> properties)
        {
            m_properties = properties;
            InitializeClientAndUser().Wait();

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

        public string UserName
        {
            set
            {
                if (m_userName != value)
                {
                    m_userName = value;

                    // Fire event
                    PropertyChangedEventHandler handler = PropertyChanged;

                    if (handler != null)
                    {
                        handler(this, new PropertyChangedEventArgs("userName"));
                    }
                }
            }
        }

        #endregion
    }
}
