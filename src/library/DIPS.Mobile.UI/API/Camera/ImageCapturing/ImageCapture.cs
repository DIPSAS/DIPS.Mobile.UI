using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.TopToolbar;
using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Internal.Logging;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using ActivityIndicator = DIPS.Mobile.UI.Components.Loading.ActivityIndicator;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : ICameraUseCase
{
    private readonly ActivityIndicator m_activityIndicator = new()
    {
        VerticalOptions = LayoutOptions.Center,
        HorizontalOptions = LayoutOptions.Center,
        IsRunning = true
    };
    
    private readonly Label m_keepCameraStillHint = new()
    {
        VerticalOptions = LayoutOptions.End,
        HorizontalOptions = LayoutOptions.Center,
        Text = DUILocalizedStrings.KeepCameraStill,
        Style = Styles.GetLabelStyle(LabelStyle.Body300),
        TextColor = Colors.White,
        BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgba(0, 0, 0, 0.5),
        Padding = new Thickness(12, 6)
    };
    
#nullable disable
    private DidCaptureImage m_onImageCapturedDelegate;
    private CameraFailed m_cameraFailedDelegate;
    private ImageCaptureSettings m_imageCaptureSettings;
    private ImageCaptureTopToolbarView m_topToolbarView;
    private ImageCaptureBottomToolbarView m_bottomToolbarView;
#nullable enable

    private CancellationTokenSource? m_captureProcessingCts;
    
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
        m_topToolbarView = new ImageCaptureTopToolbarView(m_imageCaptureSettings, OnCancelImageCaptureButtonTapped);
        
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
    private async Task SimulateCameraShutter(bool addActivityIndicator = true)
    {
        var blackBox = new BoxView { BackgroundColor = Microsoft.Maui.Graphics.Colors.Black, Opacity = 0 };

        VibrationService.SelectionChanged();
        
        if (addActivityIndicator)
        {
            m_cameraPreview?.AddViewToRoot(m_activityIndicator, usePreviewViewTranslation: false);
        }
        
        m_cameraPreview?.AddViewToRoot(blackBox);
        
        await blackBox.FadeTo(1, 50);
        await blackBox.FadeTo(0, 50);
        
        m_cameraPreview?.RemoveViewFromRoot(blackBox);
    }

    private void OnCancelImageCaptureButtonTapped()
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
        m_cameraPreview.RemoveViewFromRoot(m_keepCameraStillHint);
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
            CancelAnyActiveImageProcessing();
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

    private void CancelAnyActiveImageProcessing()
    {
        m_captureProcessingCts?.Cancel();
        m_captureProcessingCts?.Dispose();
        m_captureProcessingCts = null;
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