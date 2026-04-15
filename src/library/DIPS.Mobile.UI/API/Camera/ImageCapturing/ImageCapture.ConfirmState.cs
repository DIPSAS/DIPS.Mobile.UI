using System.ComponentModel;
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

    private void GoToConfirmState(CapturedImage capturedImage)
    {
        if (m_cameraPreview is null)
            throw new InvalidOperationException($"{nameof(GoToConfirmState)} was called after the camera preview was torn down.");

        m_currentlyCapturedImage = capturedImage;

        m_cameraPreview.RemoveViewFromRoot(m_confirmImageWrapper);
        m_confirmImage = new Image
        {
            Source = ImageSource.FromStream(() => new MemoryStream(capturedImage.AsByteArray)),
            InputTransparent = true
        };
        m_confirmImageWrapper = new Grid { Children = { m_confirmImage }, InputTransparent = true };
        
        // Makes sure the preview stays visible until the image has loaded, preventing a
        // black flash that occurs when the preview is hidden before the image is rendered.
        m_confirmImage.PropertyChanged += HidePreviewAfterImageLoaded;

        m_cameraPreview.AddViewToRoot(m_confirmImageWrapper, 3, true);

        if (m_cameraPreview.CameraZoomView is not null)
        {
            m_cameraPreview.CameraZoomView.Opacity = 0;
        }

        m_cameraPreview.RemoveViewFromRoot(m_activityIndicator);
        m_cameraPreview.RemoveViewFromRoot(m_keepCameraStillHint);
        m_topToolbarView.GoToConfirmState(m_currentlyCapturedImage, this);
        m_bottomToolbarView.GoToConfirmState(this);
        m_cameraPreview.GoToConfirmingState();

        _ = PlatformStop();
    }

    private void HidePreviewAfterImageLoaded(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != Microsoft.Maui.Controls.Image.IsLoadingProperty.PropertyName)
            return;

        if (sender is not Microsoft.Maui.Controls.Image { IsLoading: false } image)
            return;

        image.PropertyChanged -= HidePreviewAfterImageLoaded;

        if (m_cameraPreview is null)                                                                                                              
            return;
        
        m_cameraPreview.HidePreview();
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
