using AVFoundation;
using CoreMedia;
using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared.iOS;
using DIPS.Mobile.UI.Internal.Logging;
using Foundation;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : CameraSession
{
    private AVCapturePhotoOutput? m_capturePhotoOutput;
    private AVCapturePhotoSettings? m_avCapturePhotoSettings;
    private IAVCapturePhotoCaptureDelegate? m_avCapturePhotoDelegate;

    private partial Task PlatformStart()
    {
        m_capturePhotoOutput = new AVCapturePhotoOutput();
        m_avCapturePhotoSettings = AVCapturePhotoSettings.Create();
        m_avCapturePhotoSettings.Format = new NSDictionary<NSString, NSObject>() //CONTINUE HERE.
        m_avCapturePhotoDelegate = new PhotoCaptureDelegate();
        if (m_cameraPreview != null)
        {
            return base.ConfigureAndStart(m_cameraPreview, AVCaptureSession.PresetPhoto, m_capturePhotoOutput);
        }
        
        return Task.CompletedTask;
    }

    private partial Task PlatformStop()
    {
        base.StopCameraSession();
        m_avCapturePhotoSettings = null;
        m_avCapturePhotoDelegate = null;
        return Task.CompletedTask;
    }

    public override async void ConfigureSession()
    {
        if (m_avCapturePhotoSettings == null || m_avCapturePhotoDelegate == null) return;

        await Task.Delay(1500);
        m_capturePhotoOutput?.CapturePhoto(m_avCapturePhotoSettings, m_avCapturePhotoDelegate);
    }
}

public class PhotoCaptureDelegate : AVCapturePhotoCaptureDelegate
{
    public override void DidCapturePhoto(AVCapturePhotoOutput captureOutput, AVCaptureResolvedPhotoSettings resolvedSettings)
    {
        base.DidCapturePhoto(captureOutput, resolvedSettings);
    }
}