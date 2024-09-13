using AndroidX.Camera.Core;
using DIPS.Mobile.UI.API.Camera.Preview.Android.Slider;
using DIPS.Mobile.UI.API.Tip;

namespace DIPS.Mobile.UI.API.Camera.Preview;

public partial class CameraPreview
{
    internal CameraZoomSlider? Slider { get; private set; }

    internal void OnCameraStarted(ICameraControl cameraControl)
    {
        if (Slider != null)
            return;

        // Inspiration = https://proandroiddev.com/android-camerax-tap-to-focus-pinch-to-zoom-zoom-slider-eb88f3aa6fc6
        Slider = new CameraZoomSlider(cameraControl);

        m_customViewsContainer?.Insert(0, Slider);

        Slider.VerticalOptions = LayoutOptions.End;
        Slider.Margin = new Thickness(0, 0, 0, 25);
        Slider.SetBinding(IsVisibleProperty, new Binding(nameof(CameraPreview.IsVisible), source: this));
    }

    public void ShowZoomSliderTip(string message, int durationInMilliseconds = 4000)
    {
        if (Slider is null)
            return;

        TipService.Show(message, Slider, durationInMilliseconds);
    }

    internal  void PlatformGoToConfirmingState()
    {
        if (Slider != null)
        {
            Slider.IsVisible = false;
        }
    }

    internal void PlatformGoToStreamingState()
    {
        if (Slider != null)
        {
            Slider.IsVisible = true;
        }
    }
}