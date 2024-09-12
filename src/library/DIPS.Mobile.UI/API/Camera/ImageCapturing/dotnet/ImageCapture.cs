using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture
{
    private partial Task PlatformStart()
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

}