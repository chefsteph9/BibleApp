using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibleLibrary;

using Xamarin.Forms;

namespace BibleApp
{
    public partial class VersesPage : ContentPage
    {
        public VersesPage()
        {
            InitializeComponent();
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                listview.SelectedItem = null;

                BibleAppViewModel newBindingContext = BindingContext as BibleAppViewModel;

                await newBindingContext.UpdateVerses((Belief)args.SelectedItem);


                await Navigation.PushAsync(new VersesPage()
                {
                    BindingContext = newBindingContext
                });
            }
        }
    }
}
