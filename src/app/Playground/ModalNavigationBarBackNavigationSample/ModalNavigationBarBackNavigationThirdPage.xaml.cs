namespace Playground.ModalNavigationBarBackNavigationSample;

public partial class ModalNavigationBarBackNavigationThirdPage
{
    public ModalNavigationBarBackNavigationThirdPage()
    {
        InitializeComponent();
        SetValue(NavigationPage.BarBackgroundColorProperty, Microsoft.Maui.Graphics.Colors.Red);
        SetValue(NavigationPage.BarTextColorProperty, Microsoft.Maui.Graphics.Colors.White);
    }

    private async void OnBackToBlackPageTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}