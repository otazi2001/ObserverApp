using System;

using Xamarin.Forms;

namespace ObseverAppCW2
{
    public class ReportFormPage : ContentPage
    {
        public ReportFormPage(string reportFormPath)
        {

            Services.Form form = new Services.Form(reportFormPath);

            Button save = new Button { Text = "Save" };
            save.Clicked += (sender, args) => form.SaveForm();

            StackLayout stack = new StackLayout();

            foreach(Services.Section item in form.Sections)
            {
                stack.Children.Add(new BoxView() { Opacity = 0.5f, Color = Color.Gray, WidthRequest = 100, HeightRequest = 2 });
                stack.Children.Add(getSectionView(item));
            }

            stack.Children.Add(new BoxView() { Opacity = 0.5f ,Color = Color.Gray, WidthRequest = 100, HeightRequest = 2 });

            ScrollView scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        save,
                        stack
                    }
                }
            };

            Content = scrollView;
        }

        private StackLayout getSectionView(Services.Section item)
        {
            StackLayout secStack = new StackLayout()
            {
                Margin = new Thickness(20)
            };

            Label sectionName = new Label { Text = item.Name, VerticalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold };

            StackLayout qStack = new StackLayout();
            qStack.IsVisible = false;

            foreach (Services.Question q in item.Questions)
            {
                qStack.Children.Add(GetQuestionView(q));
            }


            Button hideS = new Button { Text = "Show" , HorizontalOptions = LayoutOptions.End };
            hideS.Clicked += (sender, args) => {
                if (qStack.IsVisible)
                {
                    qStack.IsVisible = false;
                    hideS.Text = "Show";
                }
                else
                {
                    qStack.IsVisible = true;
                    hideS.Text = "Hide";
                }
            };

            Grid grid = new Grid();
            grid.Children.Add(sectionName, 0, 0);
            grid.Children.Add(hideS, 1, 0);

            secStack.Children.Add(grid);
            secStack.Children.Add(qStack);

            return secStack;
        }



        private Grid GetQuestionView(Services.Question q)
        {

            Grid grid = new Grid();

            Label instruction = new Label { Text = q.Instruction };
            grid.Children.Add(instruction, 0, 0);

            switch (q.Type)
            {
                case Services.QuestionType.Multiple:
                    Picker options = new Picker();
                    options.ItemsSource = q.Options;
                    int index = q.Options.FindIndex(0, q.Options.Count, a => a == q.Answer);
                    if(index > 0)
                    {
                        options.SelectedIndex = index;
                    }
                    options.SelectedIndexChanged += (sender, args) => PickerIndexChanged(q, options.SelectedIndex);
                    grid.Children.Add(options,0,1);
                    break;
                case Services.QuestionType.Text:
                    Entry inputText = new Entry();
                    inputText.Text = q.Answer;
                    inputText.TextChanged += (sender, args) => EntryTextChanged(q, inputText.Text);
                    grid.Children.Add(inputText, 0, 1);
                    break;

            }

            return grid;
        }

        private void PickerIndexChanged(Services.Question q, int newIndex)
        {
            q.Answer = q.Options[newIndex];
        }

        private void EntryTextChanged(Services.Question q, string Text)
        {
            q.Answer = Text;
        }
    }
}

