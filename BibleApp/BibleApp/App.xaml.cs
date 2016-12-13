using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibleLibrary;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

using Xamarin.Forms;

using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace BibleApp
{
    public partial class App : Application
    {
        #region Members

        private BibleAppViewModel m_bibleAppViewModel;

        #endregion

        #region Constructors

        public App()
        {
            BibleAppViewModel = new BibleAppViewModel();
            AppViewModel = new AppViewModel();
            InitializeComponent();
            MainPage = new NavigationPage(new BibleApp.TopicsPage())
            {
                BindingContext = m_bibleAppViewModel
            };
        }

        #endregion

        #region Properties

        public BibleAppViewModel BibleAppViewModel
        {
            set
            {
                m_bibleAppViewModel = value;
            }
            get
            {
                return m_bibleAppViewModel;
            }
        }

        public AppViewModel AppViewModel
        {
            private set; get;
        }

        #endregion

        #region Methods

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        #endregion
    }
}
