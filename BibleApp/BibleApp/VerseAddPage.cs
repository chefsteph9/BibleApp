using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;

using System.Threading.Tasks;
using BibleLibrary;
using Newtonsoft.Json.Linq;

namespace BibleApp
{

    public class VerseAddPage : ContentPage
    {
        //Connect to the Api with the bible passage as a parameter
        //Not sure if the connection is setup right
        public async Task<String> apiURI(string book, string chapter, string verseStart, string verseEnd)
        {
            string URI = "http://labs.bible.org/api/?passage=";

            string passage = URI + book + "+" + chapter + ":" + verseStart + "-" + verseEnd + "&formatting=plain";

            HttpClient p_client;
            p_client = new HttpClient();

            HttpResponseMessage response = await p_client.GetAsync(passage).ConfigureAwait(false);
            passage = await response.Content.ReadAsStringAsync();

            return passage;
        }

        Dictionary<string, int> bookToInt = new Dictionary<string, int>
        {
            { "Genesis", 1 },
            { "Exodus", 2 },
            { "Leviticus", 3 },
            { "Numbers", 4 },
            { "Deuteronomy", 5 },
            { "Joshua", 6 },
            { "Judges", 7 },
            { "Ruth", 8 },
            { "1Samuel", 9 },
            { "2Samuel", 10 },
            { "1Kings", 11 },
            { "2Kings", 12 },
            { "1Chronicles", 13 },
            { "2Chronicles", 14 },
            { "Ezra", 15 },
            { "Nehemiah", 16 },
            { "Esther", 17 },
            { "Job", 18 },
            { "Psalms", 19 },
            { "Proverbs", 20 },
            { "Ecclesiastes", 21 },
            { "Song of Solomon", 22 },
            { "Isaiah", 23 },
            { "Jeremiah", 24 },
            { "Lamentations", 25 },
            { "Ezekiel", 26 },
            { "Daniel", 27 },
            { "Hosea", 28 },
            { "Joel", 29 },
            { "Amos", 30 },
            { "Obadiah", 31 },
            { "Jonah", 32 },
            { "Micah", 33 },
            { "Nahum", 34 },
            { "Habakkuk", 35 },
            { "Zephaniah", 36 },
            { "Haggai", 37 },
            { "Zechariah", 38 },
            { "Malachi", 39 },
            { "Matthew", 40 },
            { "Mark", 41 },
            { "Luke", 42 },
            { "John", 43 },
            { "Acts", 44 },
            { "Romans", 45 },
            { "1Corinthians", 46 },
            { "2Corinthians", 47 },
            { "Galatians", 48 },
            { "Ephesians", 49 },
            { "Philippians", 50 },
            { "Colossians", 51 },
            { "1Thessalonians", 52 },
            { "2Thessalonians", 53 },
            { "1Timothy", 54 },
            { "2Timothy", 55 },
            { "Titus", 56 },
            { "Philemon", 57 },
            { "Hebrews", 58 },
            { "James", 59 },
            { "1Peter", 60 },
            { "2Peter", 61 },
            { "1John", 62 },
            { "2John", 63 },
            { "3John", 64 },
            { "Jude", 65 },
            { "Revelation", 66 }
        };

        public VerseAddPage()
        {

            int bookNumber = 0;
            string bookName = "";

            Picker bookPicker = new Picker
            {
                Title = "Book of the Bible"
            };

            foreach (string book in bookToInt.Keys)
            {
                bookPicker.Items.Add(book);
            }

            bookPicker.SelectedIndexChanged += (sender, args) =>
            {
                bookName = bookPicker.Items[bookPicker.SelectedIndex];
                bookNumber = bookToInt[bookName];
            };

            var chapter = new Entry
            {
                Placeholder = "Chapter Number"
            };

            var verseStart = new Entry
            {
                Placeholder = "Starting Verse"
            };

            var verseEnd = new Entry
            {
                Placeholder = "Ending Verse"
            };

            var submit = new Button
            {
                Text = "Submit"
            };

            submit.Clicked += async (object sender, EventArgs e) =>
            {
                var passage = apiURI(bookName, chapter.ToString(), verseStart.ToString(), verseEnd.ToString());

                var verse = new Verse();
                verse.book = bookNumber;
                verse.chapter = chapter;
                verse.verseStart = verseStart;
                verse.verseEnd = verseEnd;
                verse.verse = passage.Result;
            
                await CreateVerseAsync(topicId, beliefID, verse);
            
                await Navigation.PushModalAsync(new VersePage());
            
            };

            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Add New Verse" },
                    bookPicker,
                    chapter,
                    verseStart,
                    verseEnd,
                    submit
                }
            };
        }

        private void Submit_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
