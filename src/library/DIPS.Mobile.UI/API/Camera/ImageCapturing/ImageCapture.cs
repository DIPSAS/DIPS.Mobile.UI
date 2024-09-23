using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.ConfirmState;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.StreamingState;
using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Internal.Logging;
using ActivityIndicator = DIPS.Mobile.UI.Components.Loading.ActivityIndicator;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : ICameraUseCase
{
    private CameraPreview? m_cameraPreview;
    private DidCaptureImage? m_onImageCapturedDelegate;
    private CameraFailed m_cameraFailedDelegate;
    private ConfirmStateView? m_confirmStateView;
    private Image? m_confirmImage;
    private ActivityIndicator? m_activityIndicator;
    private Grid m_bottomToolbar;
    private StreamingStateView? m_streamingStateView;

    private bool m_flashActive;
    
    public async Task Start(CameraPreview cameraPreview, DidCaptureImage onImageCapturedDelegate, CameraFailed cameraFailedDelegate, Action<ImageCaptureSettings>? configure = null)
    {
        var imageCaptureSettings = new ImageCaptureSettings();
        if (configure != null)
        {
            configure.Invoke(imageCaptureSettings);
        }
        
        m_cameraPreview = cameraPreview;
        m_cameraPreview.AddUseCase(this);
        m_onImageCapturedDelegate = onImageCapturedDelegate;
        m_cameraFailedDelegate = cameraFailedDelegate;
        if (await CameraPermissions.CanUseCamera())
        {
            Log("Permitted to use camera");
            await m_cameraPreview.HasLoaded();
            ConstructCrossPlatformViews();
            await PlatformStart(imageCaptureSettings, m_cameraFailedDelegate);
            m_cameraPreview.GoToStreamingState();
        }
        else
        {
            Log("Not permitted to use camera");
        }
    }

    private async Task OnCapture()
    {
        VibrationService.SelectionChanged();
        
        if (m_streamingStateView?.ShutterButton != null)
        {
            Touch.SetIsEnabled(m_streamingStateView.ShutterButton, false);    
        }

#if __ANDROID__ //iOS already has a blinking effect when we capture photo. TODO: Maybe remove blinking effect on iOS? Because the blinking effect appears after photo is processed
        var blackBox = new BoxView { BackgroundColor = Microsoft.Maui.Graphics.Colors.Black, Opacity = 0 };
        m_cameraPreview?.AddViewToRoot(blackBox, true);
        
        await blackBox.FadeTo(1, 50);
        await blackBox.FadeTo(0, 50);
        
        m_cameraPreview?.RemoveViewFromRoot(blackBox);
#endif

        m_activityIndicator = new ActivityIndicator
        {
            VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, IsRunning = true
        };
        m_cameraPreview?.AddViewToRoot(m_activityIndicator);
    }
    
    private void SwitchToConfirmState(CapturedImage capturedImage, ImageCaptureSettings imageCaptureSettings)
    {
        PlatformStop();

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
        m_cameraPreview.AddBottomToolbarView(m_confirmStateView);
        RemoveCaptureImageStateVisuals();
        m_cameraPreview.GoToConfirmingState();
    }

    private void RemoveCaptureImageStateVisuals()
    {
        if (m_cameraPreview?.CameraZoomView != null)
        {
            m_cameraPreview.CameraZoomView.IsVisible = false;
        }
        m_cameraPreview?.RemoveToolbarView(m_streamingStateView);
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

        if (m_streamingStateView?.ShutterButton != null)
        {
            Touch.SetIsEnabled(m_streamingStateView.ShutterButton, true);    
        }
        
        RemoveConfirmStateVisuals();
        if (m_cameraPreview?.CameraZoomView != null)
        {
            m_cameraPreview.CameraZoomView.IsVisible = true;
        }
        
        m_cameraPreview?.AddBottomToolbarView(m_streamingStateView);
    }

    private void RemoveConfirmStateVisuals()
    {
        m_cameraPreview?.RemoveViewFromRoot(m_confirmImage);
        m_cameraPreview?.RemoveToolbarView(m_confirmStateView);
    }

    private void ConstructCrossPlatformViews()
    {
        m_streamingStateView = new StreamingStateView(() =>
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
        
        m_cameraPreview?.AddBottomToolbarView(m_streamingStateView);
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
        PlatformStop();
        m_cameraPreview = null;
        m_onImageCapturedDelegate = null;
    }

    internal void InvokeOnImageCaptured(CapturedImage capturedImage)
    {
        m_onImageCapturedDelegate?.Invoke(capturedImage);
    }
}