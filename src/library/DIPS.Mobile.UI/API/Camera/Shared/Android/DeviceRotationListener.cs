using Android.Content;
using Android.Views;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.API.Camera.Shared.Android;

//Based on : https://developer.android.com/media/camera/camerax/orientation-rotation#orientation-event-listener-setup
internal class DeviceRotationListener(Action<SurfaceOrientation>? orientationChanged, Context? context)
    : OrientationEventListener(context)
{
    private Action<SurfaceOrientation>? m_orientationChanged = orientationChanged;

    public override void OnOrientationChanged(int orientation)
    {
        if (orientation == OrientationUnknown)
        {
            return;
        }
        
        DUILogService.LogDebug<CameraFragment>($"Rotation degrees: {orientation}");
        switch (orientation)
        {
            case > 45 and <= 135:
                m_orientationChanged?.Invoke(SurfaceOrientation.Rotation270);
                break;
            case > 135 and <= 225:
                m_orientationChanged?.Invoke(SurfaceOrientation.Rotation180);
                break;
            case > 225 and <= 315:
                m_orientationChanged?.Invoke(SurfaceOrientation.Rotation90);
                break;
            default:
                m_orientationChanged?.Invoke(SurfaceOrientation.Rotation0);
                break;
        }
    }

    protected override void Dispose(bool disposing)
    {
        m_orientationChanged = null;
        base.Dispose(disposing);
    }
}