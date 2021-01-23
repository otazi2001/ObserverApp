using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ObseverAppCW2
{
    public class FormEditorPage : ContentPage
    {

        private Picker selectQuestionType = new Picker();

        public FormEditorPage()
        {

            Title = "Form Editor";

            selectQuestionType.IsVisible = false;

            Services.DefaultReportForm defaultForm = new Services.DefaultReportForm();
            Services.Form form = new Services.Form(defaultForm.Lines);

            Button reset = new Button { Text = "Reset" };
            reset.Clicked += (sender, args) => { defaultForm.resetForm(); };

            Button save = new Button { Text = "Save" };
            save.Clicked += (sender, args) => { defaultForm.NewCustomForm(form); };

            StackLayout stack = new StackLayout();

            foreach (Services.Section item in form.Sections)
            {
                BoxView seper = new BoxView() { Opacity = 0.5f, Color = Color.Gray, WidthRequest = 100, HeightRequest = 2 };
                stack.Children.Add(seper);
                stack.Children.Add(getSectionView(item, form, seper));
            }


            Button addSection = new Button { Text = "Add Section" };
            addSection.Clicked += (sender, args) =>
            {
                Services.Section newSec = new Services.Section("");
                form.addSection(newSec);
                BoxView seper = new BoxView() { Opacity = 0.5f, Color = Color.Gray, WidthRequest = 100, HeightRequest = 2 };
                stack.Children.Add(seper);
                stack.Children.Add(getSectionView(newSec, form, seper));
            };

            Grid grid = new Grid();
            grid.Children.Add(save, 0, 0);
            grid.Children.Add(reset, 1, 0);

            ScrollView scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        grid,
                        stack,
                        new BoxView() { Opacity = 0.5f, Color = Color.Gray, WidthRequest = 100, HeightRequest = 2 },
                        addSection,
                        selectQuestionType
                    }
                }
            };

            Content = scrollView;


        }

        private StackLayout getSectionView(Services.Section item, Services.Form form, BoxView seper)
        {
            StackLayout secStack = new StackLayout()
            {
                Margin = new Thickness(20),
                Spacing = 10
            };

            Button deleteS = new Button { Text = "Delete Section" };
            deleteS.Clicked += (sender, args) => {
                form.removeSection(item);
                secStack.IsVisible = false;
                seper.IsVisible = false;
            };

            Entry renameS = new Entry { Text = item.Name, Placeholder = "Section Title" };
            renameS.TextChanged += (sender, args) => { item.Name = renameS.Text; };

            StackLayout qStack = new StackLayout();
            qStack.IsVisible = false;

            foreach (Services.Question q in item.Questions)
            {
                qStack.Children.Add(GetQuestionView(q, item));
            }

            Button addQuestion = new Button { Text = "Add a Question" };
            addQuestion.IsVisible = false;
            addQuestion.Clicked += (sender, args) => AddQuestion(qStack, item);

            Button hideS = new Button { Text = "Show" };
            hideS.Clicked += (sender, args) => {
                if (qStack.IsVisible)
                {
                    qStack.IsVisible = false;
                    addQuestion.IsVisible = false;
                    hideS.Text = "Show";
                }
                else
                {
                    qStack.IsVisible = true;
                    addQuestion.IsVisible = true;
                    hideS.Text = "Hide";
                }
            };

            Grid grid = new Grid();
            grid.Children.Add(deleteS, 0, 0);
            grid.Children.Add(hideS, 1, 0);

            secStack.Children.Add(grid);
            secStack.Children.Add(renameS);
            secStack.Children.Add(qStack);
            secStack.Children.Add(addQuestion);

            return secStack;
        }

        private StackLayout GetQuestionView(Services.Question q, Services.Section s)
        {
            StackLayout grid = new StackLayout();

            Button deleteQ = new Button { Text = "Delete Question" };
            deleteQ.Clicked += (sender, args) => {
                grid.IsVisible = false;
                s.removeQuestion(q);
            };
            grid.Children.Add(deleteQ);

            Entry changeQinstruction = new Entry { Text = q.Instruction };
            changeQinstruction.TextChanged += (sender, args) => { q.Instruction = changeQinstruction.Text; };
            grid.Children.Add(changeQinstruction);

            switch (q.Type)
            {
                case Services.QuestionType.Multiple:
                    Label explainQ = new Label { Text = "Insert the options seperated by a comma" };
                    grid.Children.Add(explainQ);

                    string optionsString = "";
                    foreach(string item in q.Options)
                    {
                        optionsString += $"{item},";
                    }
                    Entry changeQoptions = new Entry { Text = optionsString };
                    changeQoptions.TextChanged += (sender, args) => {
                        q.Options = new List<string>(changeQoptions.Text.Split(','));
                    };
                    grid.Children.Add(changeQoptions);
                    break;
                default:
                    break;
            }
            
            return grid;
        }

        private void AddQuestion(StackLayout stack, Services.Section s)
        {
            selectQuestionType.IsVisible = true;
            selectQuestionType.ItemsSource = new List<string>{ "Multiple", "Text" };
            selectQuestionType.SelectedIndexChanged += (sender, args) =>
            {
                Services.Question question;
                switch (selectQuestionType.SelectedIndex)
                {
                    case 0:
                        question = new Services.Question(Services.QuestionType.Multiple);
                        break;
                    default:
                        question = new Services.Question(Services.QuestionType.Text);
                        break;
                }

                s.addQuestion(question);
                stack.Children.Add(GetQuestionView(question, s));
                selectQuestionType.IsVisible = false;
            };

            if(!selectQuestionType.IsFocused && selectQuestionType.IsVisible)
            {
                selectQuestionType.IsVisible = false;
            }
            selectQuestionType.Focus();


        }
    }
}

