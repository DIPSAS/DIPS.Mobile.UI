using Android.Hardware.Camera2;
using Android.Media;
using Android.Views;
using AndroidX.Camera.Core;
using AndroidX.Camera.Core.Internal.Utils;
using AndroidX.Camera.Core.ResolutionSelector;
using AndroidX.Core.Content;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.API.Camera.Shared.Android;
using DIPS.Mobile.UI.Internal.Logging;
using Enum = DIPS.Mobile.UI.Extensions.Enum;

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

    private void OnImageCaptured(IImageProxy imageProxy)
    {
        var imageData = ImageUtil.JpegImageToJpegByteArray(imageProxy);
        var (orientationConstant, orientationDisplayName) = GetOrientationMetadata(imageData);

        var capturedImage = new CapturedImage(imageData, imageProxy.ImageInfo, imageProxy.Width, imageProxy.Height, new ImageTransformation(orientationConstant, orientationDisplayName));
        if (m_imageCaptureSettings == null) return;
        SwitchToConfirmState(capturedImage, m_imageCaptureSettings);
    }

    private static (int orientationConstant, string orientationDisplayName) GetOrientationMetadata(byte[] imageData)
    {
        using var stream = new MemoryStream(imageData);
        var exif = new AndroidX.ExifInterface.Media.ExifInterface(stream);
        var orientationConstant = exif.GetAttributeInt(AndroidX.ExifInterface.Media.ExifInterface.TagOrientation,
            AndroidX.ExifInterface.Media.ExifInterface.OrientationNormal);
        var orientationDisplayName = orientationConstant switch
        {
            AndroidX.ExifInterface.Media.ExifInterface.OrientationNormal => nameof(AndroidX.ExifInterface.Media
                .ExifInterface.OrientationNormal),
            AndroidX.ExifInterface.Media.ExifInterface.OrientationRotate90 => nameof(AndroidX.ExifInterface.Media
                .ExifInterface.OrientationRotate90),
            AndroidX.ExifInterface.Media.ExifInterface.OrientationRotate180 => nameof(AndroidX.ExifInterface.Media
                .ExifInterface.OrientationRotate180),
            AndroidX.ExifInterface.Media.ExifInterface.OrientationRotate270 => nameof(AndroidX.ExifInterface.Media
                .ExifInterface.OrientationRotate270),
            AndroidX.ExifInterface.Media.ExifInterface.OrientationTranspose => nameof(AndroidX.ExifInterface.Media
                .ExifInterface.OrientationTranspose),
            AndroidX.ExifInterface.Media.ExifInterface.OrientationTransverse => nameof(AndroidX.ExifInterface.Media
                .ExifInterface.OrientationTransverse),
            AndroidX.ExifInterface.Media.ExifInterface.OrientationUndefined => nameof(AndroidX.ExifInterface.Media
                .ExifInterface.OrientationUndefined),
            _ => throw new ArgumentOutOfRangeException(nameof(orientationConstant))
        };

        return (orientationConstant, orientationDisplayName);
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