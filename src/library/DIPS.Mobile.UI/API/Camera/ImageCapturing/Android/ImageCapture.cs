using Android.Graphics;
using Android.Views;
using AndroidX.Camera.Core;
using AndroidX.Camera.Core.Internal.Utils;
using AndroidX.Camera.Core.ResolutionSelector;
using AndroidX.Core.Content;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.API.Camera.Shared.Android;
using ExifInterface = AndroidX.ExifInterface.Media.ExifInterface;
using Size = Android.Util.Size;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : CameraFragment
{
#nullable disable
    private AndroidX.Camera.Core.ImageCapture m_cameraCaptureUseCase;
#nullable enable

    private ImageCaptureCallback? m_imageCaptureCallback;

    private partial Task PlatformStart(ImageCaptureSettings imageCaptureSettings, CameraFailed cameraFailedDelegate)
    {
        var resolutionSelectorBuilder = new ResolutionSelector.Builder()
            .SetAspectRatioStrategy(AspectRatioStrategy.Ratio43FallbackAutoStrategy);

        if (imageCaptureSettings.MaxHeightOrWidth is not null)
        {
            resolutionSelectorBuilder.SetResolutionStrategy(new ResolutionStrategy(
                new Size(imageCaptureSettings.MaxHeightOrWidth.Value, imageCaptureSettings.MaxHeightOrWidth.Value),
                ResolutionStrategy.FallbackRuleClosestLowerThenHigher));
        }
        else
        {
            // If consumer has not set TargetResolution, we will use the highest available resolution.
            resolutionSelectorBuilder.SetResolutionStrategy(ResolutionStrategy.HighestAvailableStrategy);
        }
            
        var resolutionSelector = resolutionSelectorBuilder.Build();
        
        m_cameraCaptureUseCase = new AndroidX.Camera.Core.ImageCapture.Builder()
            .SetResolutionSelector(resolutionSelector)
            // CaptureModeZeroShutterLag is only supported on some devices, but will fall back to
            // CAPTURE_MODE_MINIMIZE_LATENCY if it isn't supported. If needed in the future, you can check
            // if the device supports it via AndroidX.Camera.Core.ImageCapture.Camera.CameraInfo.IsZslSupported
            .SetCaptureMode(AndroidX.Camera.Core.ImageCapture.CaptureModeZeroShutterLag)
            .Build();
        
        // Add listener to receive updates.
        return base.SetupCameraAndTryStartUseCase(m_cameraPreview, m_cameraCaptureUseCase, resolutionSelector, cameraFailedDelegate);
    }

    private partial void PlatformCapturePhoto()
    {
        if (Context is null)
            return;

        CancelAnyActiveImageProcessing();
        m_captureProcessingCts = new CancellationTokenSource();

        m_cameraCaptureUseCase.FlashMode = FlashActive
            ? AndroidX.Camera.Core.ImageCapture.FlashModeOn
            : AndroidX.Camera.Core.ImageCapture.FlashModeOff;
        
        m_imageCaptureCallback?.Dispose();
        m_imageCaptureCallback = new ImageCaptureCallback(
            onCaptureStarted: () => SimulateCameraShutter(false),
            onImageCaptureFailed: InvokeOnImageCaptureFailed,
            onImageCaptured: ProcessImageAndGoToConfirmState);

        if (CameraInfo?.IsZslSupported != true && m_cameraPreview is not null)
        {
            AddKeepCameraStillHint();
        }

        m_cameraCaptureUseCase?.TakePicture(ContextCompat.GetMainExecutor(Context), m_imageCaptureCallback);
    }

    private partial async Task PlatformStop()
    {
        CancelAnyActiveImageProcessing();

        m_imageCaptureCallback?.Dispose();
        m_imageCaptureCallback = null;

        await base.TryStop();
    }

    public override void OnStarted()
    {
        var resolution = m_cameraCaptureUseCase.ResolutionInfo?.Resolution ?? new Size(0, 0);
        OnCameraStartedCrossPlatform((float)m_cameraPreview.PreviewView.Height, new Microsoft.Maui.Graphics.Size(resolution.Width, resolution.Height));
    }

    private void InvokeOnImageCaptureFailed(ImageCaptureException imageCaptureException)
    {
        if (imageCaptureException.Message != null)
        {
            PlatformOnCameraFailed(new CameraException("DidTryCaptureImage", imageCaptureException));
        }
    }

    internal override void OrientationChanged(SurfaceOrientation surfaceOrientation)
    {
    }

    private async void ProcessImageAndGoToConfirmState(IImageProxy imageProxy)
    {
        var cancellationToken = m_captureProcessingCts?.Token ?? CancellationToken.None;

        try
        {
            var imageData = ImageUtil.JpegImageToJpegByteArray(imageProxy);
            var bitmap = imageProxy.ToBitmap();

            using var imageMemoryStream = new MemoryStream(imageData);
            var exif = new ExifInterface(imageMemoryStream);

            var orientationDegree = exif.ToTrueOrientationDegree();
            var imageTransformation = new ImageTransformation(orientationDegree, orientationDegree.ToString());
            
            var thumbnail = await TryGetThumbnail(exif, imageTransformation, cancellationToken);
            var tuple = await CapturedImage.RotateBitmapImageBasedOnOrientation(imageTransformation, bitmap, cancellationToken);

            imageData = tuple.Item1;
            bitmap = tuple.Item2;

            var capturedImage = new CapturedImage(imageData, bitmap, thumbnail.Item1, thumbnail.Item2, imageProxy, imageTransformation);

            GoToConfirmState(capturedImage);
        }
        catch (OperationCanceledException)
        {
            // Camera was stopped while processing the captured image, for example if the user canceled capture.
        }
        catch (Exception e)
        {
            PlatformOnCameraFailed(new CameraException("ProcessImageFailed", e));
        }
    }

    private async Task<(byte[], Bitmap)> TryGetThumbnail(ExifInterface exif, ImageTransformation transformation, CancellationToken cancellationToken = default)
    {
        if (!exif.HasThumbnail)
            return (null!, null!);

        var bitmapImage = exif.ThumbnailBitmap;
        if (bitmapImage == null)
            return (null!, null!);

        return await CapturedImage.RotateBitmapImageBasedOnOrientationAsByteArray(transformation, bitmapImage, cancellationToken);
    }

    private void AddKeepCameraStillHint()
    {
        m_keepCameraStillHint.Margin = new Thickness(0, 0, 0, m_cameraPreview.BottomOverlayOffset);
        
        m_cameraPreview?.AddViewToRoot(m_keepCameraStillHint, usePreviewViewTranslation: false);
    }

    private static int GetOrientationMetadata(ExifInterface exif)
    {
        exif.SetAttribute(ExifInterface.TagOrientation, string.Empty);

        var orientationConstant = exif.GetAttributeInt(ExifInterface.TagOrientation, ExifInterface.OrientationNormal);
        
        return orientationConstant;
    }

    private static long[] GetBitsPerSample(ExifInterface exif)
    {
        return exif.GetAttributeRange(ExifInterface.TagBitsPerSample);
    }

    private partial void PlatformOnCameraFailed(CameraException cameraException) =>
        OnCameraFailed<ImageCapture>(cameraException);
}

