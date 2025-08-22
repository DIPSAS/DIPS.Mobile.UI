
using DIPS.Mobile.UI.Components.BottomSheets;

namespace Playground.VetleSamples;

public partial class BottomSheetWithToolbar
{
    public BottomSheetWithToolbar()
    {
        InitializeComponent();

    }

    private void Chip_OnTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ContentPage() {Title = "Test", Content = new Label(){Text = "Hei"} });
    }

    protected override void OnClosed()
    {
        base.OnClosed();
    }

    protected override void OnOpened()
    {
        base.OnOpened();
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        await BottomSheetService.CloseAll();
        Shell.Current.Navigation.PushModalAsync(new NavigationPage(new VetleTestPage1()));
    }

    private void VisualElement_OnSizeChanged(object sender, EventArgs e)
    {
    }
}