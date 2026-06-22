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
    private ImageCaptureTopToolbarView m_topToolbarView;
    private ImageCaptureBottomToolbarView m_bottomToolbarView;
    private CaptureSession m_cameraSession;
#nullable enable

    private CancellationTokenSource? m_captureProcessingCts;
    private bool m_isCapturing;

    public async Task StartSingleImageCapture(CameraPreview cameraPreview, DidCaptureImage onImageCapturedDelegate, 
        CameraFailed cameraFailedDelegate, CameraOptions cameraOptions)
    {
        m_cameraSession = new SingleCaptureSession(cameraOptions);

        await StartImageCapture(cameraPreview, onImageCapturedDelegate, cameraFailedDelegate);
    }
    
    public async Task StartMultiImageCapture(CameraPreview cameraPreview, DidCaptureImage onImageCapturedDelegate,
        CameraFailed cameraFailedDelegate, CameraOptions cameraOptions, MultiImageCaptureOptions multiImageCaptureOptions)
    {
        ArgumentNullException.ThrowIfNull(cameraPreview);
        ArgumentNullException.ThrowIfNull(onImageCapturedDelegate);
        ArgumentNullException.ThrowIfNull(cameraFailedDelegate);
        ArgumentNullException.ThrowIfNull(cameraOptions);
        ArgumentNullException.ThrowIfNull(multiImageCaptureOptions);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(multiImageCaptureOptions.MaxImageCount);

        m_capturedImages.Clear();
        m_cameraSession = new MultiCaptureSession(cameraOptions, multiImageCaptureOptions);

        await StartImageCapture(cameraPreview, onImageCapturedDelegate, cameraFailedDelegate);
    }

    private async Task StartImageCapture(CameraPreview cameraPreview, DidCaptureImage onImageCapturedDelegate,
        CameraFailed cameraFailedDelegate)
    {
        if (m_isCapturing)
            throw new InvalidOperationException("A capture session is already running. Stop the current session before starting another.");

        m_cameraPreview = cameraPreview;
        m_cameraPreview.AddUseCase(this);
        m_onImageCapturedDelegate = onImageCapturedDelegate;
        m_cameraFailedDelegate = cameraFailedDelegate;
        if (await CameraPermissions.CanUseCamera())
        {
            m_isCapturing = true;
            Log("Permitted to use camera");
            await m_cameraPreview.HasLoaded();
            ConstructCrossPlatformViews();
            await PlatformStart(m_cameraSession.CameraOptions, m_cameraFailedDelegate);
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
        m_topToolbarView = new ImageCaptureTopToolbarView(m_cameraSession, OnCancelImageCaptureButtonTapped);
        
        m_cameraPreview?.AddTopToolbarView(m_topToolbarView);
        m_cameraPreview?.AddBottomToolbarView(m_bottomToolbarView);
    }

    private void OnCameraStartedCrossPlatform(float previewViewHeight, Size cameraResolution)
    {
        m_cameraPreview?.SetToolbarHeights(previewViewHeight);
        m_cameraSession.CameraOptions.CameraInfo.CurrentCameraResolution = cameraResolution;
    }

    /// <summary>
    /// This is called when user has pressed the capture button, waiting for captured image to be processed
    /// </summary>
    private async Task SimulateCameraShutter(bool addActivityIndicator = true)
    {
        OptimisticallyUpdateGalleryButton();

        var blackBox = new BoxView { BackgroundColor = Microsoft.Maui.Graphics.Colors.Black, Opacity = 0 };

        VibrationService.SelectionChanged();
        
        if (m_cameraSession is SingleCaptureSession && addActivityIndicator)
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
        m_cameraSession.CameraOptions.CancelButtonCommand?.Execute(null);
        Stop();
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
        TearDownGalleryOverlay();

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
            TearDownGalleryOverlay();
            m_cameraPreview = null;
            m_onImageCapturedDelegate = null;
            m_capturedImages.Clear();
            m_capturedImagesGalleryButton = null;
            m_isCapturing = false;
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
        m_isCapturing = false;
    }

    private void OnPhotoCaptured(CapturedImage capturedImage)
    {
        if (m_cameraSession is MultiCaptureSession { MultiImageCaptureOptions.RequiresConfirmationOnEachImage: false })
        {
            m_onImageCapturedDelegate?.Invoke(capturedImage);
            AddImageAndUpdateImagePreview(capturedImage);
            ContinueInPreviewState();
            return;
        }

        GoToConfirmState(capturedImage);
    }

    private void OnImageCaptureFailed(CameraException cameraException)
    {
        m_capturedImagesGalleryButton?.CancelPendingCapture(m_capturedImages);
        PlatformOnCameraFailed(cameraException);
    }

    private void ContinueInPreviewState()
    {
        m_cameraPreview?.RemoveViewFromRoot(m_activityIndicator);
        m_cameraPreview?.RemoveViewFromRoot(m_keepCameraStillHint);
        m_bottomToolbarView?.SetShutterButtonEnabled(true);
    }

    private void OnFinishedImageCaptureButtonTapped()
    {
        if (m_cameraSession is MultiCaptureSession multiCaptureSession)
        {
            multiCaptureSession.MultiImageCaptureOptions.FinishedButtonCommand?.Execute(null);
        }

        Stop();
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
    private partial Task PlatformStart(CameraOptions cameraOptions, CameraFailed cameraFailedDelegate);
    private partial Task PlatformStop();
}
