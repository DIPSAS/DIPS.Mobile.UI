using Android.Content;
using Android.Views;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.API.Camera.Shared.Android;

//Based on : https://developer.android.com/media/camera/camerax/orientation-rotation#orientation-event-listener-setup
internal class DeviceRotationListener(Action<SurfaceOrientation>? orientationChanged, Context? context)
    : OrientationEventListener(context)
{
    private Action<SurfaceOrientation>? m_orientationChanged = orientationChanged;
    private SurfaceOrientation m_currentRotation;

    public override void OnOrientationChanged(int orientation)
    {
        if (orientation == OrientationUnknown)
        {
            return;
        }
        
        switch (orientation)
        {
            case > 45 and <= 135:
                if(m_currentRotation is not SurfaceOrientation.Rotation270)
                {
                    m_currentRotation = SurfaceOrientation.Rotation270;
                    m_orientationChanged?.Invoke(m_currentRotation);
                }
                break;
            case > 135 and <= 225:
                if (m_currentRotation is not SurfaceOrientation.Rotation180)
                {
                    m_currentRotation = SurfaceOrientation.Rotation180;
                    m_orientationChanged?.Invoke(m_currentRotation);
                }
                break;
            case > 225 and <= 315:
                if(m_currentRotation is not SurfaceOrientation.Rotation90)
                {
                    m_currentRotation = SurfaceOrientation.Rotation90;
                    m_orientationChanged?.Invoke(m_currentRotation);
                }
                break;
            default:
                if (m_currentRotation is not SurfaceOrientation.Rotation0)
                {
                    m_currentRotation = SurfaceOrientation.Rotation0;
                    m_orientationChanged?.Invoke(m_currentRotation);
                }
                break;
        }
    }

    protected override void Dispose(bool disposing)
    {
        m_orientationChanged = null;
        base.Dispose(disposing);
    }
}