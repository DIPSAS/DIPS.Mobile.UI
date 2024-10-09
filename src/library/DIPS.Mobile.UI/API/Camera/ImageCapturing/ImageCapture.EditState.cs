using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : IEditStateObserver
{
    private TaskCompletionSource? m_rotatingImageTcs;
    
#nullable disable
    private CapturedImage m_rotatedImage;
#nullable enable
    
    private void GoToEditState(CapturedImage capturedImage)
    {
        m_rotatedImage = capturedImage;

        m_rotatingImageTcs = null;
        
        m_bottomToolbarView.GoToEditState(this);
    }

    void IEditStateObserver.OnDoneButtonTapped()
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

    async Task IEditStateObserver.OnRotateButtonTapped()
    {
        if(!m_rotatingImageTcs?.Task.IsCompleted ?? false)
            return;

        m_rotatingImageTcs = new TaskCompletionSource();
            
        await Task.WhenAll(CapturedImage.RotateImage(m_confirmImage, m_rotatedImage, m_confirmImage.Width, m_confirmImage.Height, m_currentlyCapturedImage.Transformation.OrientationDegree), Task.Run(async () =>
        {
            // Run on background thread, cuz this is heavy shit
            m_rotatedImage = await m_rotatedImage!.Rotate();
        }));
            
        m_rotatingImageTcs.SetResult();
    }
}