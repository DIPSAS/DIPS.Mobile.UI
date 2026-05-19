namespace Playground;

public partial class NavigationBarColorSample
{
    public NavigationBarColorSample()
    {
        InitializeComponent();
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}