namespace Playground.ModalNavigationBarBackNavigationSample;

public partial class ModalNavigationBarBackNavigationSample
{
    public ModalNavigationBarBackNavigationSample()
    {
        InitializeComponent();
        SetValue(NavigationPage.BarBackgroundColorProperty, Microsoft.Maui.Graphics.Colors.White);
        SetValue(NavigationPage.BarTextColorProperty, Microsoft.Maui.Graphics.Colors.Black);
    }

    private async void OnOpenBlackPageTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ModalNavigationBarBackNavigationSecondPage());
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}