using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibleLibrary;

using Xamarin.Forms;

namespace BibleApp
{
    public partial class TopicsPage : ContentPage
    {
        public TopicsPage()
        {
            InitializeComponent();
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                listview.SelectedItem = null;

                BibleAppViewModel newBindingContext = BindingContext as BibleAppViewModel;

                await newBindingContext.UpdateBeliefs((Topic)args.SelectedItem);


                await Navigation.PushAsync(new BeliefsPage()
                {
                    BindingContext = newBindingContext
                });
            }
        }
    }
}
