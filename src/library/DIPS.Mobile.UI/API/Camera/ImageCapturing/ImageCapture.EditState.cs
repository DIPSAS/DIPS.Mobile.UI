using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : IImageEditStateObserver
{
    private TaskCompletionSource? m_rotatingImageTcs;
    
#nullable disable
    private CapturedImage m_rotatedImage;
#nullable enable
    
    private double m_startingImageWidth;
    private double m_startingImageHeight;
    
    private void GoToEditState(CapturedImage capturedImage)
    {
        m_rotatedImage = capturedImage;
        m_startingImageWidth = m_confirmImage.Width;
        m_startingImageHeight = m_confirmImage.Height;

        m_rotatingImageTcs = null;
        
        m_topToolbarView.GoToEditState();
        m_bottomToolbarView.GoToEditState(this);
    }

    void IImageEditStateObserver.OnSaveButtonTapped()
    {
        if(!m_rotatingImageTcs?.Task.IsCompleted ?? false)
            return;

        try
        {
            m_confirmImage.Rotation = 0;
            GoToConfirmState(m_rotatedImage!);
        }
        catch (Exception e)
        {
            PlatformOnCameraFailed(new CameraException("OnSaveButtonTapped", e));
        }
    }

    void IImageEditStateObserver.OnCancelButtonTapped()
    {
        try
        {
            m_confirmImage.Rotation = 0;
            GoToConfirmState(m_currentlyCapturedImage!);
        }
        catch (Exception e)
        {
            PlatformOnCameraFailed(new CameraException("OnCancelButtonTapped", e));
        }
    }

    async Task IImageEditStateObserver.OnRotateButtonTapped(bool clockwise)
    {
        if(!m_rotatingImageTcs?.Task.IsCompleted ?? false)
            return;

        m_rotatingImageTcs = new TaskCompletionSource();
            
        await Task.WhenAll(CapturedImage.RotateImage(clockwise, m_confirmImage, m_rotatedImage, m_startingImageWidth, m_startingImageHeight, m_currentlyCapturedImage.Transformation.OrientationDegree), Task.Run(async () =>
        {
            // Run on background thread, cuz this is heavy shit
            m_rotatedImage = await m_rotatedImage!.Rotate(clockwise);
        }));
            
        m_rotatingImageTcs.SetResult();
    }
}