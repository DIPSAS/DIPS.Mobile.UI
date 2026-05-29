namespace Playground;

public partial class NavigationBarColorSample
{
    public NavigationBarColorSample()
    {
        InitializeComponent();
        SetValue(NavigationPage.BarBackgroundColorProperty, Microsoft.Maui.Graphics.Colors.Black);
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}