using System.ComponentModel;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Extensions.Android;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.API.Camera.Preview;

//Preview: https://developer.android.com/media/camera/camera2/camera-preview
public class CameraPreviewHandler() : ViewHandler<CameraPreview, View>(ViewMapper)
{
    protected override View CreatePlatformView()
    {
        return VirtualView.ConstructView().ToPlatform(DUI.GetCurrentMauiContext!);
    }
}