namespace Components.ComponentsSamples.BottomSheets.Sheets;

public partial class BottomSheetWithNavigation
{
    public BottomSheetWithNavigation()
    {
        InitializeComponent();
    }

    private async void OnPage1Tapped(object? sender, EventArgs e)
    {
        await Sheet.PushAsync(new NavigationPage1(Sheet));
    }

    private async void OnPage2Tapped(object? sender, EventArgs e)
    {
        await Sheet.PushAsync(new NavigationPage2(Sheet));
    }
}
