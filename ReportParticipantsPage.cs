using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ObseverAppCW2
{
    public class ReportParticipantsPage : ContentPage
    {
        public ReportParticipantsPage(string reportParticipantsPath)
        {

            Services.ParticipantsManager manager = new Services.ParticipantsManager(reportParticipantsPath);

            StackLayout stack = new StackLayout();

            foreach(Services.Participant item in manager.Participants)
            {
                stack.Children.Add(new BoxView() { Opacity = 0.5f, Color = Color.Gray, WidthRequest = 100, HeightRequest = 2 });
                stack.Children.Add(getParticpantView(manager, item));
            }

            stack.Children.Add(new BoxView() { Opacity = 0.5f, Color = Color.Gray, WidthRequest = 100, HeightRequest = 2 });

            Button addPartipant = new Button { Text = "New Partipant"};
            addPartipant.Clicked += (sender, args) =>
            {
                Services.Participant newP = new Services.Participant();
                manager.AddPartipant(newP);
                stack.Children.Add(getParticpantView(manager, newP));
            };

            Button save = new Button { Text = "Save" };
            save.Clicked += (sender, args) => manager.SaveParticipants();

            Grid grid = new Grid();
            grid.Children.Add(save, 0, 0);
            grid.Children.Add(addPartipant, 1, 0);
                

            ScrollView scrollView = new ScrollView
            {
                Content = new StackLayout{
                    Children =
                    {
                        grid,
                        stack
                    }
                }
            };

            Content = scrollView;
        }

        private StackLayout getParticpantView(Services.ParticipantsManager m, Services.Participant p)
        {

            StackLayout pStack = new StackLayout()
            {
                Margin = new Thickness(20),
                Spacing = 10
            };
            StackLayout stack = new StackLayout();
            stack.IsVisible = false;

            Label display = new Label { Text = p.Name, VerticalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold };
            if (string.IsNullOrEmpty(p.Name))
            {
                display.Text = "New Participant";
            }

            Button hideP = new Button { Text = "Show" };
            hideP.Clicked += (sender, args) =>
            {
                if (stack.IsVisible)
                {
                    stack.IsVisible = false;
                    hideP.Text = "Show";
                }
                else
                {
                    stack.IsVisible = true;
                    hideP.Text = "Hide";
                }
            };

            Button deleteP = new Button { Text = "Delete" };
            deleteP.Clicked += (sender, args) =>
            {
                m.RemovePartipant(p);
                pStack.IsVisible = false;
            };

            Grid grid = new Grid();
            
            grid.Children.Add(display, 0, 0);
            grid.Children.Add(deleteP, 1, 0);
            grid.Children.Add(hideP, 2, 0);

            pStack.Children.Add(grid);


            Entry nameE = new Entry { Text = p.Name, Placeholder = "Full Name" };
            nameE.TextChanged += (sender, args) => {
                display.Text = nameE.Text;
                p.Name = nameE.Text;
            };
            stack.Children.Add(nameE);


            Entry birthdayE = new Entry { Text = p.Birthday, Placeholder = "Birthday" };
            birthdayE.TextChanged += (sender, args) => {
                p.Birthday = birthdayE.Text;
            };
            stack.Children.Add(birthdayE);


            List<string> genders = new List<string>{ "Male", "Female", "Other" }; 
            Picker genderP = new Picker();
            genderP.ItemsSource = genders;
            genderP.SelectedIndex = findIndex(genders, p.Gender);
            genderP.SelectedIndexChanged += (sender, args) => {
                p.Gender = genders[genderP.SelectedIndex];
            };
            stack.Children.Add(genderP);


            List<string> roles = new List<string> { "Judge", "Prosecutor", "Defendant", "Attourney", "Witness", "Other" };
            Picker roleP = new Picker();
            roleP.ItemsSource = roles;
            roleP.SelectedIndex = findIndex(roles, p.Role);
            roleP.SelectedIndexChanged += (sender, args) => {
                p.Role = roles[genderP.SelectedIndex];
            };
            stack.Children.Add(roleP);


            Editor infoE = new Editor { Text =  p.Info.Replace('¬','\n'), Placeholder = "Additional Info"};
            infoE.TextChanged += (sender, args) => {
                var lines = infoE.Text.Split('\n');
                var input = "";
                foreach(string item in lines)
                {
                    input += $"{item}¬";
                }

                p.Info = input;
            };

            Frame infoF = new Frame { HasShadow = false, Padding = 5, CornerRadius = 8, BorderColor = Color.Gray, Content = infoE };

            stack.Children.Add(infoF);

            pStack.Children.Add(stack);



            return pStack;
        }

        public int findIndex(List<string> list, string test)
        {
            for(var i = 0; i < list.Count; i++)
            {
                if(list[i] == test)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}

