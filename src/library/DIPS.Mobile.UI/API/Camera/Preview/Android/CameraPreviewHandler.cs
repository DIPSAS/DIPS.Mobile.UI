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
public partial class CameraPreviewHandler : ContentViewHandler
{
    private ScaleGestureDetector? m_scaleGestureDetector;
    

    public CameraPreviewHandler() : base(ViewMapper, ViewCommandMapper)
    {
    }

    
    

    

    

    
}