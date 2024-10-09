using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.API.Camera.Preview;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture
{

    internal CameraPreview m_cameraPreview;
    
    private partial Task PlatformStart(ImageCaptureSettings imageCaptureSettings, CameraFailed cameraFailedDelegate)
    {
        return Task.CompletedTask;
    }
    
    private partial Task PlatformStop()
    {
        return Task.CompletedTask;
    }

    private partial void PlatformCapturePhoto()
    {
        
    }
    
    private partial void PlatformOnCameraFailed(CameraException cameraException){}

}