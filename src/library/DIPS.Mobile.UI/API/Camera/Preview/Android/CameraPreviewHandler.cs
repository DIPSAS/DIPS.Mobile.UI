using Android.App;
using Android.Views;
using DIPS.Mobile.UI.API.Camera.Extensions.Android;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.API.Camera.Preview;

//Preview: https://developer.android.com/media/camera/camera2/camera-preview
public partial class CameraPreviewHandler() : ViewHandler<CameraPreview, View>(ViewMapper)
{
    private ScaleGestureDetector? m_scaleGestureDetector;
    private StatusAndNavigationBarColors? m_statusAndNavigationBarColors;

    protected override View CreatePlatformView()
    {
        return VirtualView.ConstructView().ToPlatform(DUI.GetCurrentMauiContext!);
    }

    protected override void ConnectHandler(View platformView)
    {
        if (VirtualView.IsInFullscreen)
        {
            m_statusAndNavigationBarColors = Context.SetStatusAndNavigationBarColor(VirtualView.BackgroundColor);    
        }
        
        base.ConnectHandler(platformView);
    }

    protected override void DisconnectHandler(View platformView)
    {
        if (m_statusAndNavigationBarColors != null)
        {
            Context.ResetStatusAndNavigationBarColor(m_statusAndNavigationBarColors);    
        }
        
        base.DisconnectHandler(platformView);
    }
}