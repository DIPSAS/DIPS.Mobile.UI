namespace Playground.ModalNavigationBarBackNavigationSample;

public partial class ModalNavigationBarBackNavigationSecondPage
{
    public ModalNavigationBarBackNavigationSecondPage()
    {
        InitializeComponent();
        SetValue(NavigationPage.BarBackgroundColorProperty, Microsoft.Maui.Graphics.Colors.Black);
        SetValue(NavigationPage.BarTextColorProperty, Microsoft.Maui.Graphics.Colors.White);
    }

    private async void OnBackToWhitePageTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnOpenRedPageTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ModalNavigationBarBackNavigationThirdPage());
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}