using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : IStreamingStateObserver
{
    void IStreamingStateObserver.OnTappedShutterButton()
    {
        try
        {
            _ = OnBeforeCapture();
            PlatformCapturePhoto();
        }
        catch (Exception e)
        {
            PlatformOnCameraFailed(new CameraException("DidTryCaptureImage", e));
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
        m_bottomToolbarView.GoToStreamingState(this);

        m_bottomToolbarView.SetShutterButtonEnabled(true);

        m_cameraPreview.RemoveViewFromRoot(m_confirmImage);

        if (m_cameraPreview.CameraZoomView != null)
        {
            m_cameraPreview.CameraZoomView.Opacity = 1;
        }
    }

    async void IStreamingStateObserver.OnSettingsChanged()
    {
        _ = DialogService.ShowMessage(DUILocalizedStrings.SettingsChanged, DUILocalizedStrings.SettingsChangedDescription, "Ok");
        await PlatformStop();
        _ = PlatformStart(m_imageCaptureSettings, m_cameraFailedDelegate);
    }

    private bool FlashActive { get; set; }

    bool IStreamingStateObserver.FlashActive => FlashActive;
}