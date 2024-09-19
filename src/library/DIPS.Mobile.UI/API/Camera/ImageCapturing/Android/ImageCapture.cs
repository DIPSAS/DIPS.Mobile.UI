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
namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : CameraFragment
{
    private AndroidX.Camera.Core.ImageCapture? m_cameraCaptureUseCase;
    private ImageCaptureSettings? m_imageCaptureSettings;

    private partial Task PlatformStart(ImageCaptureSettings imageCaptureSettings, CameraFailed cameraFailedDelegate)
    {
        m_imageCaptureSettings = imageCaptureSettings;
        var resolutionSelector = new ResolutionSelector.Builder()
            .Build();
        m_cameraCaptureUseCase = new AndroidX.Camera.Core.ImageCapture.Builder()
            .SetResolutionSelector(resolutionSelector)
            .Build();

        // Add listener to receive updates.
        return m_cameraPreview != null
            ? base.SetupCameraAndTryStartUseCase(m_cameraPreview, m_cameraCaptureUseCase, cameraFailedDelegate)
            : Task.CompletedTask;
    }

    private partial void PlatformCapturePhoto()
    {
        if (Context == null) 
            return;
        
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
        using var imageMemoryStream = new MemoryStream(imageData);
        var exif = new ExifInterface(imageMemoryStream);
        var (orientationConstant, orientationDisplayName) = GetOrientationMetadata(exif);
        
        var thumbnail = await TryGetThumbnail(exif, orientationConstant);
        var capturedImage = new CapturedImage(imageData, thumbnail, imageProxy.ImageInfo, imageProxy.Width, imageProxy.Height, new ImageTransformation(orientationConstant, orientationDisplayName));
        if (m_imageCaptureSettings == null) return;
        SwitchToConfirmState(capturedImage, m_imageCaptureSettings);
    }

    private async Task<byte[]?> TryGetThumbnail(ExifInterface exif, int orientationConstant)
    {
        if (!exif.HasThumbnail) return null;

        var bitmapImage = exif.ThumbnailBitmap;
        if (bitmapImage == null) return null;
        var matrix = new Matrix();
        var rotationDegrees = orientationConstant switch
        {
            ExifInterface.OrientationRotate90 => 90,
            ExifInterface.OrientationRotate180 => 180,
            ExifInterface.OrientationRotate270 => 270,
            _ => 0
        };
        
        matrix.PostRotate(rotationDegrees);
        
        var rotatedBitmap =
            Bitmap.CreateBitmap(bitmapImage, 0, 0, bitmapImage.Width, bitmapImage.Height, matrix, true);
        
        using var rotatedMemoryStream = new MemoryStream();
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        await rotatedBitmap.CompressAsync(Bitmap.CompressFormat.Jpeg!, 100, rotatedMemoryStream);
        stopwatch.Stop();
        rotatedBitmap.Dispose();
        return rotatedMemoryStream.ToArray();
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