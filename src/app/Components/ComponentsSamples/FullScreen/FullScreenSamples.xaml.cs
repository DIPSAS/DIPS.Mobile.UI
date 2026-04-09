using DIPS.Mobile.UI.Components.FullScreenPresenter;

namespace Components.ComponentsSamples.FullScreen;

public partial class FullScreenSamples
{
    public FullScreenSamples()
    {
        InitializeComponent();
    }

    private async void OnCloseClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void OnPresentFullscreenClicked(object? sender, EventArgs e)
    {
        await FullScreenPresenterService.Present(scrollView, closesOnAppBackgrounded: true);
    }
}
