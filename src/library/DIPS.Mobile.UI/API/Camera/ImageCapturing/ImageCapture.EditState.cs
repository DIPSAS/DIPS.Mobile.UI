using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : IEditStateObserver
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
        
        m_bottomToolbarView.GoToEditState(this);
    }

    void IEditStateObserver.OnSaveButtonTapped()
    {
        if(!m_rotatingImageTcs?.Task.IsCompleted ?? false)
            return;
            
        m_confirmImage.Rotation = 0;
        GoToConfirmState(m_rotatedImage!);
    }

    void IEditStateObserver.OnCancelButtonTapped()
    {
        m_confirmImage.Rotation = 0;
        GoToConfirmState(m_currentlyCapturedImage!);
    }

    async Task IEditStateObserver.OnRotateButtonTapped(bool clockwise)
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