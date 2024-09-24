using Android.Hardware.Camera2;
using System.Diagnostics;
using Android.Graphics;
using Android.Media;
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
    private AndroidX.Camera.Core.ImageCapture? m_cameraCaptureUseCase;
    private ImageCaptureSettings? m_imageCaptureSettings;

    private partial Task PlatformStart(ImageCaptureSettings imageCaptureSettings, CameraFailed cameraFailedDelegate)
    {
        m_imageCaptureSettings = imageCaptureSettings;
        var resolutionSelector = new ResolutionSelector.Builder()
            .SetResolutionStrategy(new ResolutionStrategy(new Size((int)m_cameraPreview.TargetResolution.Width, (int)m_cameraPreview.TargetResolution.Height), ResolutionStrategy.FallbackRuleClosestLowerThenHigher))
            .SetAspectRatioStrategy(AspectRatioStrategy.Ratio43FallbackAutoStrategy)
            .Build();
        
        m_cameraCaptureUseCase = new AndroidX.Camera.Core.ImageCapture.Builder()
            .SetResolutionSelector(resolutionSelector)
            .Build();

        // Add listener to receive updates.
        return m_cameraPreview != null
            ? base.SetupCameraAndTryStartUseCase(m_cameraPreview, m_cameraCaptureUseCase, resolutionSelector, cameraFailedDelegate)
            : Task.CompletedTask;
    }

    private partial void PlatformCapturePhoto()
    {
        if (Context is null || m_cameraCaptureUseCase is null) 
            return;

        m_cameraCaptureUseCase.FlashMode = m_flashActive
            ? AndroidX.Camera.Core.ImageCapture.FlashModeOn
            : AndroidX.Camera.Core.ImageCapture.FlashModeOff;
         
        CameraProvider?.Unbind(m_previewUseCase);
        m_cameraCaptureUseCase?.TakePicture(ContextCompat.GetMainExecutor(Context),
            new ImageCaptureCallback(OnImageCaptured, InvokeOnImageCaptureFailed));
    }

    private partial async Task PlatformStop()
    {
        await base.TryStop();
    }

    public override void OnStarted()
    {
        Console.WriteLine(m_cameraCaptureUseCase.ResolutionInfo.Resolution);
        m_cameraPreview?.SetToolbarHeights();
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
        var (orientationConstant, orientationDisplayName) = GetOrientationMetadata(exif);
        var imageTransformation = new ImageTransformation(orientationConstant, orientationDisplayName);
        var thumbnail = await TryGetThumbnail(exif, imageTransformation);
        
        var capturedImage = new CapturedImage(imageData,bitmap,  thumbnail, imageProxy.ImageInfo, imageProxy.Width, imageProxy.Height,imageTransformation);
        if (m_imageCaptureSettings == null) return;
        SwitchToConfirmState(capturedImage, m_imageCaptureSettings);
    }

    private async Task<byte[]?> TryGetThumbnail(ExifInterface exif, ImageTransformation transformation)
    {
        if (!exif.HasThumbnail) return null;

        var bitmapImage = exif.ThumbnailBitmap;
        if (bitmapImage == null) return null;
        
        return await CapturedImage.RotateBitmapImageBasedOnOrientation(transformation, bitmapImage); ;
    }

    

    private static (int orientationConstant, string orientationDisplayName) GetOrientationMetadata(ExifInterface exif)
    {
        var orientationConstant = exif.GetAttributeInt(AndroidX.ExifInterface.Media.ExifInterface.TagOrientation,
            AndroidX.ExifInterface.Media.ExifInterface.OrientationNormal);
        
        var orientationDisplayName = orientationConstant switch
        {
            ExifInterface.OrientationNormal => nameof(ExifInterface.OrientationNormal),
            ExifInterface.OrientationRotate90 => nameof(ExifInterface.OrientationRotate90),
            ExifInterface.OrientationRotate180 => nameof(ExifInterface.OrientationRotate180),
            ExifInterface.OrientationRotate270 => nameof(ExifInterface.OrientationRotate270),
            ExifInterface.OrientationTranspose => nameof(ExifInterface.OrientationTranspose),
            ExifInterface.OrientationTransverse => nameof(ExifInterface.OrientationTransverse),
            ExifInterface.OrientationUndefined => nameof(ExifInterface.OrientationUndefined),
            _ => throw new ArgumentOutOfRangeException(nameof(orientationConstant))
        };

        return (orientationConstant, orientationDisplayName);
    }

    private static long[] GetBitsPerSample(AndroidX.ExifInterface.Media.ExifInterface exif)
    {
        return exif.GetAttributeRange(ExifInterface.TagBitsPerSample);
    }

    private partial void PlatformOnCameraFailed(CameraException cameraException) =>
        OnCameraFailed<ImageCapture>(cameraException);
}

internal class ImageCaptureCallback : AndroidX.Camera.Core.ImageCapture.OnImageCapturedCallback
{
    private Action<IImageProxy> m_invokeOnImageCaptured;
    private readonly Action<ImageCaptureException> m_invokeOnImageCaptureFailed;

    public ImageCaptureCallback(Action<IImageProxy> invokeOnImageCaptured, Action<ImageCaptureException> invokeOnImageCaptureFailed)
    {
        m_invokeOnImageCaptured = invokeOnImageCaptured;
        m_invokeOnImageCaptureFailed = invokeOnImageCaptureFailed;
    }


    public override void OnError(ImageCaptureException exception)
    {
        m_invokeOnImageCaptureFailed.Invoke(exception);
        base.OnError(exception);
    }
    

    public override void OnCaptureSuccess(IImageProxy image)
    {
        m_invokeOnImageCaptured.Invoke(image);
        base.OnCaptureSuccess(image);
        image?.Close();
    }

    protected override void Dispose(bool disposing)
    {
        m_invokeOnImageCaptured = null;
        base.Dispose(disposing);
    }
}