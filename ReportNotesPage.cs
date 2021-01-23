using System;

using Xamarin.Forms;

namespace ObseverAppCW2
{
    public class ReportNotesPage : ContentPage
    {
        public ReportNotesPage(string path)
        {

            Services.NotesManager manager = new Services.NotesManager(path);

            Button save = new Button { Text = "Save" };
            save.Clicked += (sender, args) => manager.SaveNotes();

            var height = Application.Current.MainPage.Height;

            Editor notesE = new Editor { Text = manager.Notes, Placeholder = "Type your notes here...", HeightRequest = height / 2 };
            notesE.TextChanged += (sender, args) => {
                manager.Notes = notesE.Text;
            };

            Frame notesF = new Frame { HasShadow = false, Padding = 5, CornerRadius = 8, BorderColor = Color.Gray, Content = notesE };

            ScrollView scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        save,
                        notesF
                    }
                }
            };

            Content = scrollView;
        }
    }
}

