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
    class VerseViewModel : ViewModelBase
    {
        #region Members

        private BeliefViewModel m_beliefViewModel;

        private ObservableCollection<Verse> m_verses;

        private ObservableCollection<string> m_errorMessages;

        private int m_selectedVerse;

        private HttpClient m_client;

        #endregion

        #region Constructors

        VerseViewModel()
        {

        }

        #endregion

        #region Properties



        #endregion

        #region Methods



        #endregion
    }
}
