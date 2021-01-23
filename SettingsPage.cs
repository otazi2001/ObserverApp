using System;

using Xamarin.Forms;

namespace ObseverAppCW2
{
    public class SettingsPage : TabbedPage
    {
        public SettingsPage()
        {
            Children.Add(new FormEditorPage());
            Children.Add(new SecurityManagerPage());
        }
    }
}

