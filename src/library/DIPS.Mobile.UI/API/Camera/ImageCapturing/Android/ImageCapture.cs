using Android.Graphics;
using Android.Hardware.Camera2;
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
            .Build();

        // Add listener to receive updates.
        return base.SetupCameraAndTryStartUseCase(m_cameraPreview, m_cameraCaptureUseCase, resolutionSelector, cameraFailedDelegate);
    }

    private partial void PlatformCapturePhoto()
    {
        if (Context is null) 
            return;

        m_cameraCaptureUseCase.FlashMode = FlashActive
            ? AndroidX.Camera.Core.ImageCapture.FlashModeOn
            : AndroidX.Camera.Core.ImageCapture.FlashModeOff;
         
        CameraProvider?.Unbind(PreviewUseCase);
        m_cameraCaptureUseCase?.TakePicture(ContextCompat.GetMainExecutor(Context),
            new ImageCaptureCallback(OnImageCaptured, InvokeOnImageCaptureFailed));
    }

    private partial async Task PlatformStop()
    {
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

    private async void OnImageCaptured(IImageProxy imageProxy)
    {
        var imageData = ImageUtil.JpegImageToJpegByteArray(imageProxy);
        var bitmap = imageProxy.ToBitmap();
        
        using var imageMemoryStream = new MemoryStream(imageData);
        var exif = new ExifInterface(imageMemoryStream);
        var orientationDegree = exif.ToTrueOrientationDegree();
        var imageTransformation = new ImageTransformation(orientationDegree, orientationDegree.ToString());
        var thumbnail = await TryGetThumbnail(exif, imageTransformation);

        var tuple = await CapturedImage.RotateBitmapImageBasedOnOrientation(imageTransformation, bitmap);

        imageData = tuple.Item1;
        bitmap = tuple.Item2;
        
        var capturedImage = new CapturedImage(imageData, bitmap, thumbnail.Item1, thumbnail.Item2, imageProxy, imageTransformation);
        
        GoToConfirmState(capturedImage);
    }

    private async Task<(byte[], Bitmap)> TryGetThumbnail(ExifInterface exif, ImageTransformation transformation)
    {
        if (!exif.HasThumbnail)
            return (null!, null!);

        var bitmapImage = exif.ThumbnailBitmap;
        if (bitmapImage == null) 
            return (null!, null!);
        
        return (await CapturedImage.RotateBitmapImageBasedOnOrientationAsByteArray(transformation, bitmapImage));
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

internal class ImageCaptureCallback(
    Action<IImageProxy> invokeOnImageCaptured,
    Action<ImageCaptureException> invokeOnImageCaptureFailed)
    : AndroidX.Camera.Core.ImageCapture.OnImageCapturedCallback
{
    private Action<IImageProxy>? m_invokeOnImageCaptured = invokeOnImageCaptured;

    public override void OnError(ImageCaptureException exception)
    {
        invokeOnImageCaptureFailed.Invoke(exception);
        base.OnError(exception);
    }

    public override void OnCaptureSuccess(IImageProxy image)
    {
        m_invokeOnImageCaptured?.Invoke(image);
        base.OnCaptureSuccess(image);
        image?.Close();
    }

    protected override void Dispose(bool disposing)
    {
        m_invokeOnImageCaptured = null;
        base.Dispose(disposing);
    }
}