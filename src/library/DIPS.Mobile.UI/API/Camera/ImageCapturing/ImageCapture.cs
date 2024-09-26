using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.ConfirmState;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.StreamingBottomToolbar;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.TopToolbar;
using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Internal.Logging;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using ActivityIndicator = DIPS.Mobile.UI.Components.Loading.ActivityIndicator;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : ICameraUseCase
{
    private readonly ActivityIndicator m_activityIndicator = new()
    {
        VerticalOptions = LayoutOptions.Center,
        HorizontalOptions = LayoutOptions.Center,
        IsRunning = true
    };
    
    private CameraPreview? m_cameraPreview;
    private DidCaptureImage? m_onImageCapturedDelegate;
    private CameraFailed m_cameraFailedDelegate;
    private ConfirmStateView? m_confirmStateView;
    private Image? m_confirmImage;
    private Grid m_bottomToolbar;
    private StreamingBottomToolbarView? m_streamingBottomToolbarView;

    private bool m_flashActive;
    
#nullable disable
    private ImageCaptureSettings m_imageCaptureSettings;
#nullable enable
    
    public async Task Start(CameraPreview cameraPreview, DidCaptureImage onImageCapturedDelegate, CameraFailed cameraFailedDelegate, Action<ImageCaptureSettings>? configure = null)
    {
        m_imageCaptureSettings = new ImageCaptureSettings();
        configure?.Invoke(m_imageCaptureSettings);

        m_cameraPreview = cameraPreview;
        m_cameraPreview.AddUseCase(this);
        m_onImageCapturedDelegate = onImageCapturedDelegate;
        m_cameraFailedDelegate = cameraFailedDelegate;
        if (await CameraPermissions.CanUseCamera())
        {
            Log("Permitted to use camera");
            await m_cameraPreview.HasLoaded();
            ConstructCrossPlatformViews();
            await PlatformStart(m_imageCaptureSettings, m_cameraFailedDelegate);
            m_cameraPreview.GoToStreamingState();
        }
        else
        {
            Log("Not permitted to use camera");
        }
    }

    private void CrossPlatformCameraStarted(float previewViewHeight, Size cameraResolution)
    {
        m_streamingBottomToolbarView?.SetShutterButtonEnabled(true);
        m_cameraPreview?.SetToolbarHeights(previewViewHeight);

        m_imageCaptureSettings.CameraInfo.CurrentCameraResolution = cameraResolution;
    }
    
    private async Task OnCapture()
    {
        VibrationService.SelectionChanged();
        
        m_streamingBottomToolbarView?.SetShutterButtonEnabled(false);

        var blackBox = new BoxView { BackgroundColor = Microsoft.Maui.Graphics.Colors.Black, Opacity = 0 };
        m_cameraPreview?.AddViewToRoot(blackBox);
        
        await blackBox.FadeTo(1, 50);
        await blackBox.FadeTo(0, 50);
        
        m_cameraPreview?.RemoveViewFromRoot(blackBox);
        m_cameraPreview?.AddViewToRoot(m_activityIndicator);
    }
    
    private async void SwitchToConfirmState(CapturedImage capturedImage, ImageCaptureSettings imageCaptureSettings)
    {
        if (m_cameraPreview == null)
        {
            return;
        }

        m_confirmImage = new Image
        {
            Source = ImageSource.FromStream(() => new MemoryStream(capturedImage.AsByteArray))
        };

        m_confirmStateView = new ConfirmStateView(() =>
            {
                m_onImageCapturedDelegate?.Invoke(capturedImage);
                
                switch (imageCaptureSettings.PostCaptureAction)
                {
                    case PostCaptureAction.Close:
                        ResetAllVisuals();
                        PlatformStop();
                        break;
                    case PostCaptureAction.Continue:
                        ResetToCaptureImageState();
                        PlatformStart(imageCaptureSettings, m_cameraFailedDelegate);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            },
            () =>
            {
                ResetToCaptureImageState();
                PlatformStart(imageCaptureSettings, m_cameraFailedDelegate);
            });

        
        m_cameraPreview.AddViewToRoot(m_confirmImage, true);
        // We need to add a slight delay, because the camera preview will be black for a short moment if we don't, because the image is not yet loaded - "simulating a shutter effect", 
        await Task.Delay(10);
        m_cameraPreview.AddBottomToolbarView(m_confirmStateView);
        RemoveCaptureImageStateVisuals();
        m_cameraPreview.GoToConfirmingState();

        _ = PlatformStop();
    }

    private void RemoveCaptureImageStateVisuals()
    {
        if (m_cameraPreview?.CameraZoomView != null)
        {
            m_cameraPreview.CameraZoomView.IsVisible = false;
        }
        m_cameraPreview?.RemoveToolbarView(m_streamingBottomToolbarView);
        m_cameraPreview?.RemoveViewFromRoot(m_activityIndicator);
    }

    private void ResetAllVisuals()
    {
        RemoveCaptureImageStateVisuals();
        RemoveConfirmStateVisuals();
    }

    private void ResetToCaptureImageState()
    {
        m_cameraPreview?.GoToStreamingState();

        m_streamingBottomToolbarView?.SetShutterButtonEnabled(true);
        
        RemoveConfirmStateVisuals();
        if (m_cameraPreview?.CameraZoomView != null)
        {
            m_cameraPreview.CameraZoomView.IsVisible = true;
        }
        
        m_cameraPreview?.AddBottomToolbarView(m_streamingBottomToolbarView);
    }

    private void RemoveConfirmStateVisuals()
    {
        m_cameraPreview?.RemoveViewFromRoot(m_confirmImage);
        m_cameraPreview?.RemoveToolbarView(m_confirmStateView);
    }

    private void ConstructCrossPlatformViews()
    {
        m_streamingBottomToolbarView = new StreamingBottomToolbarView(() =>
        {
            try
            {
                _ = OnCapture();
                PlatformCapturePhoto();
            }
            catch (Exception e)
            {
                PlatformOnCameraFailed(new CameraException("DidTryCaptureImage", e));
            }
        }, () =>
        {
            m_flashActive = !m_flashActive;
        });

        var topToolbarView = new TopToolbarView(m_imageCaptureSettings!, OnSettingsChanged);
        
        m_cameraPreview?.AddTopToolbarView(topToolbarView);
        m_cameraPreview?.AddBottomToolbarView(m_streamingBottomToolbarView);
    }

    private async void OnSettingsChanged()
    {
        _ = DialogService.ShowMessage(DUILocalizedStrings.SettingsChanged, DUILocalizedStrings.SettingsChangedDescription,
            "Ok");
        await PlatformStop();
        _ = PlatformStart(m_imageCaptureSettings, m_cameraFailedDelegate);
    }

    private partial void PlatformOnCameraFailed(CameraException cameraException);
    private partial void PlatformCapturePhoto();
    private partial Task PlatformStart(ImageCaptureSettings imageCaptureSettings, CameraFailed cameraFailedDelegate);
    private partial Task PlatformStop();

    private void Log(string message)
    {
        DUILogService.LogDebug<ImageCapture>(message);
    }

    /// <summary>
    /// Will stop the capture session.
    /// </summary>
    public void Stop()
    {
        try
        {
            PlatformStop();
            m_cameraPreview = null;
            m_onImageCapturedDelegate = null;
        }
        catch (Exception e)
        {
            Log(e.Message);
        }
    }

    internal void InvokeOnImageCaptured(CapturedImage capturedImage)
    {
        m_onImageCapturedDelegate?.Invoke(capturedImage);
    }
}