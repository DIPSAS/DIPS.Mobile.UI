using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : IConfirmStateObserver
{
#nullable disable
    private CapturedImage m_currentlyCapturedImage;
    private Image m_confirmImage;
    private Grid m_confirmImageWrapper;
#nullable enable

    private async void GoToConfirmState(CapturedImage capturedImage)
    {
        m_currentlyCapturedImage = capturedImage;
        
        m_cameraPreview.RemoveViewFromRoot(m_confirmImageWrapper);
        m_confirmImage = new Image
        {
            Source = ImageSource.FromStream(() => new MemoryStream(capturedImage.AsByteArray)),
            InputTransparent = true
        };
        m_confirmImageWrapper = new Grid { Children = { m_confirmImage }, InputTransparent = true };
        m_cameraPreview.AddViewToRoot(m_confirmImageWrapper, 3, true);
        
        // We need to add a slight delay, because the camera preview will be black for a short moment if we don't, because the image is not yet loaded - "simulating a shutter effect", 
        await Task.Delay(10);

        if (m_cameraPreview.CameraZoomView is not null)
        {
            m_cameraPreview.CameraZoomView.Opacity = 0;
        }

        m_cameraPreview.RemoveViewFromRoot(m_activityIndicator);
        m_cameraPreview.GoToConfirmingState();
        m_topToolbarView.GoToConfirmState(m_currentlyCapturedImage, this);
        m_bottomToolbarView.GoToConfirmState(this);

        _ = PlatformStop();
    }

    void IConfirmStateObserver.OnEditButtonTapped()
    {
        GoToEditState(m_currentlyCapturedImage);
    }

    void IConfirmStateObserver.OnUsePhotoButtonTapped()
    {
        m_onImageCapturedDelegate?.Invoke(m_currentlyCapturedImage);

        switch (m_imageCaptureSettings.PostCaptureAction)
        {
            case PostCaptureAction.Close:
                ResetAllVisuals();
                PlatformStop();
                break;
            case PostCaptureAction.Continue:
                GoToStreamingState();
                PlatformStart(m_imageCaptureSettings, m_cameraFailedDelegate);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void IConfirmStateObserver.OnRetakePhotoButtonTapped()
    {
        GoToStreamingState();
        PlatformStart(m_imageCaptureSettings, m_cameraFailedDelegate);
    }
}