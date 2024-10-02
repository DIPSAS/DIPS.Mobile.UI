using AndroidX.Camera.Core;
using DIPS.Mobile.UI.API.Tip;

namespace DIPS.Mobile.UI.API.Camera.Preview.Android.Slider;

internal class CameraZoomSlider : View
{
    public CameraZoomSlider(ICameraControl cameraControl)
    {
        CameraControl = cameraControl;
    }

    public void SetZoomLevel(double linearZoom)
    {
        
    }

    public ICameraControl CameraControl { get; }
    public double ZoomLevel { get; set; }
    internal bool HasZoomed { get; set; }
}