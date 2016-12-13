using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BibleApp.Pages
{
    public class VerseDisplayPage : ContentPage
    {
        public VerseDisplayPage()
        {

            Label label = new Label
            {
                Text = verse.verse,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };

            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Selected Verse" },
                    label
                }
            };
        }
    }
}
