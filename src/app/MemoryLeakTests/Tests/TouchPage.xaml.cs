using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryLeakTests.Tests;

public partial class TouchPage : ContentPage
{
    public TouchPage()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PushAsync(new DIPS.Mobile.UI.Components.Pages.ContentPage()
        {
            Content = new Label() {Text = "Navigate back to see if the component in the test still works"}
        });
    }
}