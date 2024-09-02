using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.API.Camera.PhotoCapturing;

public partial class PhotoCapture
{
    
    private CameraPreview? m_cameraPreview;

    public async Task Start(CameraPreview cameraPreview)
    {
        m_cameraPreview = cameraPreview;
        if (await CameraPermissions.CanUseCamera())
        {
            Log("Permitted to use camera");
            await m_cameraPreview.HasLoaded();
            await PlatformStart();
        }
        else
        {
            Log("Not permitted to use camera");
        }
    }

    private partial Task PlatformStart();
    private partial Task PlatformStop();

    private void Log(string message)
    {
        DUILogService.LogDebug<PhotoCapture>(message);
    }

    public void Stop()
    {
        PlatformStop();
        m_cameraPreview = null;
    }
}