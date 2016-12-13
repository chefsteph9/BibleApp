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
    class BeliefViewModel : ViewModelBase
    {
        #region Members

        private TopicViewModel m_topicViewModel;
        private VerseViewModel m_verseViewModel;

        private ObservableCollection<Belief> m_beliefs;

        private ObservableCollection<string> m_errorMessages;

        private int m_selectedVerse;

        #endregion

        #region Constructors



        #endregion

        #region Properties

        public TopicViewModel TopicViewModel
        {
            set { SetProperty(ref m_topicViewModel, value); }
            get { return m_topicViewModel; }
        }

        public VerseViewModel VerseViewModel
        {
            set { SetProperty(ref m_verseViewModel, value); }
            get { return m_verseViewModel; }
        }

        public ObservableCollection<Belief> Beliefs
        {
            set { SetProperty(ref m_beliefs, value); }
            get { return m_beliefs; }
        }

        #endregion

        #region Methods



        #endregion
    }
}
