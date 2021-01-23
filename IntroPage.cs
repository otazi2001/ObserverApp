using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ObseverAppCW2
{


    public class IntroPage : ContentPage
    {

        private Services.ReportManager manager = new Services.ReportManager();
        private StackLayout reportList = new StackLayout();
        private Label emptyDisplay = new Label { Text = "You dont have any reports.\nCreate a Report to get started!", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.Center };
        private Image imageDisplay = new Image{Source = "ReportImage.png", HorizontalOptions = LayoutOptions.Center };



        public IntroPage()
        {
            Button create = new Button { Text = "+" };
            create.Clicked += async (sender, args) => await CreateReportAsync();
            Button settings = new Button { Text = "Settings" };
            settings.Clicked += (sender, args) =>
            {
                Navigation.PushAsync(new SettingsPage());
            };

            Grid topControls = new Grid();
            topControls.Children.Add(settings, 0, 0);
            topControls.Children.Add(create, 1, 0);

            foreach(DirectoryInfo item in manager.ReportNames)
            {
                BoxView seper = new BoxView() { Opacity = 0.5f, Color = Color.Gray, WidthRequest = 100, HeightRequest = 2 };
                reportList.Children.Add(getReportView(item.Name, seper));
                reportList.Children.Add(seper);
            }

            StackLayout stack = new StackLayout { VerticalOptions = LayoutOptions.Center};

            if(manager.ReportNames.Count < 1)
            {
                reportList.IsVisible = false;
            } else
            {
                emptyDisplay.IsVisible = false;
                imageDisplay.IsVisible = false;
            }


            stack.Children.Add(reportList);
            stack.Children.Add(imageDisplay);
            stack.Children.Add(emptyDisplay);

            Content = new StackLayout
            {
                Children = {
                    topControls,

                    new BoxView() { Opacity = 0.5f, Color = Color.Gray, WidthRequest = 100, HeightRequest = 2 },
                    stack
                }
            };
        }

        private Grid getReportView(string name, BoxView seper)
        {
            Grid grid = new Grid();

            Button reportName = new Button { Text = name};
            reportName.Clicked += (sender, args) => ReportSelected(name);

            Button delete = new Button { Text = "Delete" };
            delete.Clicked += async (sender, args) => {
                await DeleteReportAsync(name);
                grid.IsVisible = false;
                seper.IsVisible = false;
            };

            grid.Children.Add(reportName, 0, 0);
            grid.Children.Add(delete, 1, 0);

            return grid;
        }

        private async Task DeleteReportAsync(string name)
        {
            bool confirm = await DisplayAlert("Delete Report", $"Are You sure you want to delete {name} ?", "Yes", "No");
            if (confirm)
            {
                manager.DeleteReport(name);
            }

            if(manager.ReportNames.Count < 1)
            {
                reportList.IsVisible = false;
                emptyDisplay.IsVisible = true;
                imageDisplay.IsVisible = true;
            }
        }


        public async Task CreateReportAsync()
        {
            string newFileName = await DisplayPromptAsync("New Report", "What would you like to name the report?");
            manager.CreateNewReport(newFileName);
            if (!reportList.IsVisible)
            {
                reportList.IsVisible = true;
                emptyDisplay.IsVisible = false;
                imageDisplay.IsVisible = false;
            }

            BoxView seper = new BoxView() { Opacity = 0.5f, Color = Color.Gray, WidthRequest = 100, HeightRequest = 2 };
            reportList.Children.Add(getReportView(newFileName, seper));
            reportList.Children.Add(seper);

        }


        public void ReportSelected(string name)
        {
            //Services.Report report = args.CurrentSelection.First() as Services.Report;
            Navigation.PushAsync(new ReportPage(new Services.Report(name)));
        }
    }
}

