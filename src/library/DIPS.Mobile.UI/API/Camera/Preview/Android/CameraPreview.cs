using AndroidX.Camera.Core;
using DIPS.Mobile.UI.API.Camera.Preview.Android.Slider;
using DIPS.Mobile.UI.API.Tip;

namespace DIPS.Mobile.UI.API.Camera.Preview;

public partial class CameraPreview
{
    private CameraZoomSlider? m_slider;
    
    internal void OnCameraStarted(ICameraControl cameraControl)
    {
        if (m_slider != null) 
            return; 
        
        // Inspiration = https://proandroiddev.com/android-camerax-tap-to-focus-pinch-to-zoom-zoom-slider-eb88f3aa6fc6
        m_slider = new CameraZoomSlider(cameraControl);
        m_customViewsContainer.Insert(0, m_slider);
        
        m_slider.VerticalOptions = LayoutOptions.End;
        m_slider.Margin = new Thickness(0, 0, 0, 25);
    }
    
    public void ShowZoomSliderTip(string message, int durationInMilliseconds = 4000)
    {
        if (m_slider is null) 
            return;
        
        TipService.Show(message, m_slider, durationInMilliseconds);
    }
}