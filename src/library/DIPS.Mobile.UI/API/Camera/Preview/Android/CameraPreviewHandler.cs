using Android.Views;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.API.Camera.Preview;

//Preview: https://developer.android.com/media/camera/camera2/camera-preview
public partial class CameraPreviewHandler() : ViewHandler<CameraPreview, View>(ViewMapper)
{
    private ScaleGestureDetector? m_scaleGestureDetector;

    protected override View CreatePlatformView()
    {
        return VirtualView.ConstructView().ToPlatform(DUI.GetCurrentMauiContext!);
    }
}