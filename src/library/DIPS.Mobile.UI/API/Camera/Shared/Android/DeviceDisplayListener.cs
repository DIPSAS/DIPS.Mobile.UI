using Android.Hardware.Display;
using Android.Views;

namespace DIPS.Mobile.UI.API.Camera.Shared.Android;

internal class DeviceDisplayListener(Action<SurfaceOrientation>? orientationChanged, DisplayManager displayManager)
    : Java.Lang.Object, DisplayManager.IDisplayListener
{
    private Action<SurfaceOrientation>? m_orientationChanged = orientationChanged;

    public void OnDisplayAdded(int displayId)
    {
        
    }

    public void OnDisplayChanged(int displayId)
    {
        var display = displayManager.GetDisplay(displayId);
        if (display != null)
        {
            m_orientationChanged?.Invoke(display.Rotation);
        }
    }

    public void OnDisplayRemoved(int displayId)
    {
        
    }

    protected override void Dispose(bool disposing)
    {
        m_orientationChanged = null;
        base.Dispose(disposing);
    }
}