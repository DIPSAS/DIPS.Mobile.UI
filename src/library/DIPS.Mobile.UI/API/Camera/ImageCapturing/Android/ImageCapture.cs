using Android.Media;
using Android.Views;
using AndroidX.Camera.Core;
using AndroidX.Camera.Core.Internal.Utils;
using AndroidX.Camera.Core.ResolutionSelector;
using AndroidX.Core.Content;
using DIPS.Mobile.UI.API.Camera.Shared.Android;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : CameraFragment
{
    private AndroidX.Camera.Core.ImageCapture? m_cameraCaptureUseCase;
    
    private partial Task PlatformStart()
    {
        var resolutionSelector = new ResolutionSelector.Builder()
            .Build();
        m_cameraCaptureUseCase = new AndroidX.Camera.Core.ImageCapture.Builder()
            .SetResolutionSelector(resolutionSelector)
            .Build();

        // Add listener to receive updates.
        return m_cameraPreview != null
            ? base.SetupCameraAndTryStartUseCase(m_cameraPreview, m_cameraCaptureUseCase)
            : Task.CompletedTask;
    }

    private partial void PlatformCapturePhoto()
    {
        if (Context == null) 
            return;
        m_cameraCaptureUseCase?.TakePicture(ContextCompat.GetMainExecutor(Context),
            new ImageCaptureCallback(OnImageCaptured, InvokeOnImageCaptureFailed));
        TryPlayShutterSound();

    }

    private void TryPlayShutterSound()
    {
        var audio = (AudioManager?) Context?.GetSystemService(Android.Content.Context.AudioService);
        if (audio == null)
        {
            return;
        }

        switch (audio.RingerMode)
        {
            case RingerMode.Normal:
                new MediaActionSound().Play(MediaActionSoundType.ShutterClick);
                break;
            case RingerMode.Silent:
            case RingerMode.Vibrate:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }

    private partial Task PlatformStop()
    {
        return base.TryStop();
    }

    public override void OnStarted()
    {
        //Update target rotation
        if (m_cameraCaptureUseCase is null || PreviewView is null) 
            return;

       
    }

    private void InvokeOnImageCaptureFailed(ImageCaptureException obj)
    {
        if (obj.Message != null)
        {
            DUILogService.LogError<ImageCapture>(obj.Message);
        }
    }

    internal override void RotationChanged(SurfaceOrientation surfaceOrientation)
    {
    }

    private async void OnImageCaptured(IImageProxy imageProxy)
    {
        var capturedImage = new CapturedImage(ImageUtil.JpegImageToJpegByteArray(imageProxy), imageProxy.ImageInfo, imageProxy.Width, imageProxy.Height);
        InvokeOnImageCaptured(capturedImage);
    }
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