using System;

using Xamarin.Forms;

namespace ObseverAppCW2
{
    public class SecurityManagerPage : ContentPage
    {
        public SecurityManagerPage()
        {
            Title = "Security";

            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

