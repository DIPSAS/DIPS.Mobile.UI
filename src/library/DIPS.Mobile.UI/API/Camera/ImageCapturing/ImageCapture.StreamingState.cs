using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.StreamingState;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : IStreamingStateObserver
{
    void IStreamingStateObserver.OnTappedShutterButton()
    {
        if (HasReachedImageLimit)
        {
            ShowMaxImagesReachedMessage();
            return;
        }

        try
        {
            m_bottomToolbarView?.SetShutterButtonEnabled(false);
            PlatformCapturePhoto();
        }
        catch (Exception e)
        {
            OnImageCaptureFailed(new CameraException("DidTryCaptureImage", e));
        }
    }

    void IStreamingStateObserver.OnTappedFlashButton()
    {
        FlashActive = !FlashActive;
    }
    
    private void GoToStreamingState()
    {
        m_cameraPreview.GoToStreamingState();
        m_topToolbarView.GoToStreamingState(this);

        StreamingTrailingControl trailingControl = m_cameraSession switch
        {
            MultiCaptureSession multiCaptureSession =>
                new StreamingTrailingControl.FinishCapture(multiCaptureSession.MultiImageCaptureOptions.FinishedButtonText, OnFinishedImageCaptureButtonTapped),
            _ => new StreamingTrailingControl.Flash()
        };

        var leadingControl = CreateCapturedImagesGalleryButtonForMultiCapture();

        m_bottomToolbarView.GoToStreamingState(this, trailingControl, leadingControl);

        m_bottomToolbarView.SetShutterButtonEnabled(true);

        m_cameraPreview.RemoveViewFromRoot(m_confirmImageWrapper);

        if (m_cameraPreview.CameraZoomView != null)
        {
            m_cameraPreview.CameraZoomView.Opacity = 1;
        }
    }

    async void IStreamingStateObserver.OnSettingsChanged()
    {
        _ = DialogService.ShowMessage(DUILocalizedStrings.SettingsChanged, DUILocalizedStrings.SettingsChangedDescription, "Ok");
        await PlatformStop();
        _ = PlatformStart(m_cameraSession.CameraOptions, m_cameraFailedDelegate);
    }

    private bool FlashActive { get; set; }

    bool IStreamingStateObserver.FlashActive => FlashActive;
}