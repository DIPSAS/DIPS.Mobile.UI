using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar;
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
    
    private Image? m_confirmImage;
    private bool m_flashActive;
    
    private TaskCompletionSource? m_rotatingImageTcs;
    
#nullable disable
    private DidCaptureImage m_onImageCapturedDelegate;
    private CameraFailed m_cameraFailedDelegate;
    private ImageCaptureSettings m_imageCaptureSettings;
    private ImageCaptureTopToolbarView m_topToolbarView;
    private ImageCaptureBottomToolbarView m_bottomToolbarView;
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
            GoToStreamingState();
        }
        else
        {
            Log("Not permitted to use camera");
        }
    }
    
    private void ConstructCrossPlatformViews()
    {
        m_bottomToolbarView = [];
        m_topToolbarView = new ImageCaptureTopToolbarView(m_imageCaptureSettings, OnDoneButtonTapped);
        
        m_cameraPreview?.AddTopToolbarView(m_topToolbarView);
        m_cameraPreview?.AddBottomToolbarView(m_bottomToolbarView);
    }

    private void OnCameraStartedCrossPlatform(float previewViewHeight, Size cameraResolution)
    {
        m_cameraPreview?.SetToolbarHeights(previewViewHeight);
        m_imageCaptureSettings.CameraInfo.CurrentCameraResolution = cameraResolution;
    }
    
    /// <summary>
    /// This is called when user has pressed the capture button, waiting for captured image to be processed
    /// </summary>
    private async Task OnBeforeCapture()
    {
        var blackBox = new BoxView { BackgroundColor = Microsoft.Maui.Graphics.Colors.Black, Opacity = 0 };

        VibrationService.SelectionChanged();
        
        m_bottomToolbarView?.SetShutterButtonEnabled(false);
        m_cameraPreview?.AddViewToRoot(blackBox);
        
        await blackBox.FadeTo(1, 50);
        await blackBox.FadeTo(0, 50);
        
        m_cameraPreview?.RemoveViewFromRoot(blackBox);
        m_cameraPreview?.AddViewToRoot(m_activityIndicator);
    }
    
    private void GoToStreamingState()
    {
        m_cameraPreview.GoToStreamingState();
        m_topToolbarView.GoToStreamingState(OnSettingsChanged);
        m_bottomToolbarView.GoToStreamingState(() =>
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
        }, () => m_flashActive = !m_flashActive, m_flashActive);

        m_bottomToolbarView.SetShutterButtonEnabled(true);

        m_cameraPreview.RemoveViewFromRoot(m_confirmImage);

        if (m_cameraPreview.CameraZoomView != null)
        {
            m_cameraPreview.CameraZoomView.Opacity = 1;
        }
    }
    
    private async void GoToConfirmState(CapturedImage capturedImage, ImageCaptureSettings imageCaptureSettings, bool updateImageSource = true)
    {
        if (updateImageSource)
        {
            m_cameraPreview.RemoveViewFromRoot(m_confirmImage);
            m_confirmImage = new Image
            {
                Source = ImageSource.FromStream(() => new MemoryStream(capturedImage.AsByteArray)),
                InputTransparent = true
            };
            m_cameraPreview.AddViewToRoot(m_confirmImage, 3);
        }
        
        // We need to add a slight delay, because the camera preview will be black for a short moment if we don't, because the image is not yet loaded - "simulating a shutter effect", 
        await Task.Delay(10);

        if (m_cameraPreview.CameraZoomView is not null)
        {
            m_cameraPreview.CameraZoomView.Opacity = 0;
        }

        m_cameraPreview.RemoveViewFromRoot(m_activityIndicator);
        m_cameraPreview.GoToConfirmingState();
        m_topToolbarView.GoToConfirmState(capturedImage, () =>
        {
            GoToEditState(capturedImage);
        });
        m_bottomToolbarView.GoToConfirmState(() =>
        {
            m_onImageCapturedDelegate?.Invoke(capturedImage);
                
            switch (imageCaptureSettings.PostCaptureAction)
            {
                case PostCaptureAction.Close:
                    ResetAllVisuals();
                    PlatformStop();
                    break;
                case PostCaptureAction.Continue:
                    GoToStreamingState();
                    PlatformStart(imageCaptureSettings, m_cameraFailedDelegate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }, () =>
        {
            GoToStreamingState();
            PlatformStart(m_imageCaptureSettings, m_cameraFailedDelegate);
        });

        _ = PlatformStop();
    }

    private void GoToEditState(CapturedImage capturedImage)
    {
        var rotatedImage = capturedImage;

        var startingHeight = m_confirmImage!.Height;
        var startingWidth = m_confirmImage.Width;
        var startingOrientationDegree = capturedImage.Transformation.OrientationDegree;

        m_rotatingImageTcs = null;
        
        m_bottomToolbarView.GoToEditState(() =>
        {
            if(!m_rotatingImageTcs?.Task.IsCompleted ?? false)
                return;
            
            m_confirmImage!.Rotation = 0;
            GoToConfirmState(rotatedImage, m_imageCaptureSettings);
        }, () =>
        {
            m_confirmImage!.Rotation = 0;
            GoToConfirmState(capturedImage, m_imageCaptureSettings);
        }, async () =>
        {
            if(!m_rotatingImageTcs?.Task.IsCompleted ?? false)
                return;

            m_rotatingImageTcs = new TaskCompletionSource();
            
            await Task.WhenAll(CapturedImage.RotateImage(m_confirmImage, rotatedImage, startingWidth, startingHeight, startingOrientationDegree), Task.Run(async () =>
                {
                    // Run on background thread, cuz this is heavy shit
                    rotatedImage = await rotatedImage.Rotate();
                }));
            
            m_rotatingImageTcs.SetResult();
        });
    }

    private async void OnSettingsChanged()
    {
        _ = DialogService.ShowMessage(DUILocalizedStrings.SettingsChanged, DUILocalizedStrings.SettingsChangedDescription,
            "Ok");
        await PlatformStop();
        _ = PlatformStart(m_imageCaptureSettings, m_cameraFailedDelegate);
    }

    private void OnDoneButtonTapped()
    {
        m_imageCaptureSettings.DoneButtonCommand?.Execute(null);
        ResetAllVisuals();
        PlatformStop();
    }
    
    private void Log(string message)
    {
        DUILogService.LogDebug<ImageCapture>(message);
    }

    private void ResetAllVisuals()
    {
        m_cameraPreview.RemoveTopToolbarView(m_topToolbarView);
        m_cameraPreview.RemoveBottomToolbarView(m_bottomToolbarView);
        m_cameraPreview.RemoveViewFromRoot(m_activityIndicator);
        m_cameraPreview.RemoveViewFromRoot(m_confirmImage);
        if (m_cameraPreview.CameraZoomView != null)
        {
            m_cameraPreview.CameraZoomView.Opacity = 0;
        }
    }
    
    /// <summary>
    /// Will stop the capture session.
    /// </summary>
    public void StopAndDispose()
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

    public void Stop()
    {
        ResetAllVisuals();
        PlatformStop();
    }

    internal void InvokeOnImageCaptured(CapturedImage capturedImage)
    {
        m_onImageCapturedDelegate?.Invoke(capturedImage);
    }
    
    private partial void PlatformOnCameraFailed(CameraException cameraException);
    private partial void PlatformCapturePhoto();
    private partial Task PlatformStart(ImageCaptureSettings imageCaptureSettings, CameraFailed cameraFailedDelegate);
    private partial Task PlatformStop();
}