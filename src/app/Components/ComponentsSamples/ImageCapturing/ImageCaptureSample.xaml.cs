using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.ImageCapturing;

namespace Components.ComponentsSamples.ImageCapturing;

public partial class ImageCaptureSample
{
    public ImageCaptureSample()
    {
        InitializeComponent();
        
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PushModalAsync(new ImagCapturePage());
    }
}