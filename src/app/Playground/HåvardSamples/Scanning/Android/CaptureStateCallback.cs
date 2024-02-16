using Android.Hardware.Camera2;

namespace Playground.HåvardSamples.Scanning.Android;


public class CaptureStateCallback : CameraCaptureSession.StateCallback
{
    private readonly Action<CameraCaptureSession> m_onConfigured;
    private readonly Action<CameraCaptureSession> m_onConfigureFailed;

    public CaptureStateCallback(Action<CameraCaptureSession> onConfigured,Action<CameraCaptureSession> OnConfigureFailed)
    {
        m_onConfigured = onConfigured;
        m_onConfigureFailed = OnConfigureFailed;
    }
    
    public override void OnConfigured(CameraCaptureSession session)
    {
        m_onConfigured.Invoke(session);
    }

    public override void OnConfigureFailed(CameraCaptureSession session)
    {
        m_onConfigureFailed(session);
    }
}