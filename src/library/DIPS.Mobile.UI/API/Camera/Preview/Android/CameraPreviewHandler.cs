using Android.Views;
using AndroidX.Camera.Core;
using DIPS.Mobile.UI.API.Camera.Preview.Android.Slider;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.API.Tip;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = Microsoft.Maui.Graphics.Colors;
using VerticalStackLayout = Microsoft.Maui.Controls.VerticalStackLayout;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.API.Camera.Preview;

//Preview: https://developer.android.com/media/camera/camera2/camera-preview
public partial class CameraPreviewHandler : ViewHandler<CameraPreview, View>
{
    private Android.Slider.CameraZoomSlider? m_slider;
    private ScaleGestureDetector? m_scaleGestureDetector;
    private Grid m_grid;
    private VerticalStackLayout m_customViewsContainer;

    public CameraPreviewHandler() : base(ViewMapper, ViewCommandMapper)
    {
    }

    protected override View CreatePlatformView()
    {
        var previewView = new Android.PreviewView.PreviewView();

        PreviewView = (AndroidX.Camera.View.PreviewView)previewView.ToPlatform(DUI.GetCurrentMauiContext!);

        m_customViewsContainer = new VerticalStackLayout
        {
            Padding = new Thickness(25, 10),
            BackgroundColor = Colors.Transparent, 
            VerticalOptions = LayoutOptions.End,
        };
        
        m_grid =
        [
            previewView,
            m_customViewsContainer
        ];

        return m_grid.ToPlatform(DUI.GetCurrentMauiContext!);
    }
    
    //Inspiration = https://proandroiddev.com/android-camerax-tap-to-focus-pinch-to-zoom-zoom-slider-eb88f3aa6fc6
    internal void OnCameraStarted(ICameraControl cameraControl)
    {
        m_slider = new CameraZoomSlider(cameraControl);
        m_customViewsContainer.Insert(0, m_slider);
        
        m_slider.VerticalOptions = LayoutOptions.End;
        m_slider.Margin = new Thickness(0, 0, 0, 25);
    }

    public AndroidX.Camera.View.PreviewView PreviewView { get; internal set; }

    public partial void ShowZoomSliderTip(string message, int durationInMilliseconds)
    {
        if (m_slider is null) 
            return;
        
        TipService.Show(message, m_slider, durationInMilliseconds);
    }

    public void AddView(Microsoft.Maui.Controls.View toolbarItems)
    {
        toolbarItems.VerticalOptions = LayoutOptions.Center;
        
        m_customViewsContainer.Add(toolbarItems);
    }

    internal void RemoveZoomSlider()
    {
        if (m_slider is null)
            return;

        m_slider.Handler?.DisconnectHandler();
        m_grid.Remove(m_slider);
        m_slider = null;
    }
}