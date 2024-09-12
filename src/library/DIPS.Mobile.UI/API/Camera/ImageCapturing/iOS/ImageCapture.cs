using AVFoundation;
using CoreMedia;
using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared.iOS;
using DIPS.Mobile.UI.Internal.Logging;
using Foundation;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : CameraSession
{
    private AVCapturePhotoOutput? m_capturePhotoOutput;

    private partial Task PlatformStart()
    {
        m_capturePhotoOutput = new AVCapturePhotoOutput();
        if (m_cameraPreview != null)
        {
            return base.ConfigureAndStart(m_cameraPreview, AVCaptureSession.PresetHigh, m_capturePhotoOutput);
        }
        
        return Task.CompletedTask;
    }

    private partial Task PlatformStop()
    {
        base.StopCameraSession();
        return Task.CompletedTask;
    }

    public override async void ConfigureSession()
    {
        var settings = CreateSettings();
        if (settings == null) return;
        await Task.Delay(1500);
        
        m_capturePhotoOutput?.CapturePhoto(settings, new PhotoCaptureDelegate(PhotoCaptured));
    }

    public override AVCaptureDevice? SelectCaptureDevice() =>
        AVCaptureDevice.GetDefaultDevice(AVCaptureDeviceType.BuiltInWideAngleCamera,
            AVMediaTypes.Video, AVCaptureDevicePosition.Back) ?? 
        AVCaptureDevice.GetDefaultDevice(AVCaptureDeviceType.BuiltInDualCamera,
            AVMediaTypes.Video, AVCaptureDevicePosition.Back);

    private void PhotoCaptured(AVCapturePhoto photo)
    {
        if (photo.FileDataRepresentation != null)
        {
            InvokeOnImageCaptured(new CapturedImage(photo.FileDataRepresentation.ToArray(), photo));    
        }
    }

    //Cant re-use settings for each capture, remarks from Apple doc: https://developer.apple.com/documentation/avfoundation/avcapturephotosettings#overview
#pragma warning disable CA1422
    private AVCapturePhotoSettings? CreateSettings()
    {
        if (m_capturePhotoOutput == null) return null;

        var formatKey = AVVideo.CodecKey;
        NSObject? formatValue = null;
        if (m_capturePhotoOutput.AvailablePhotoCodecTypes.Contains(AVVideo.CodecJPEG))
        {

            formatValue = AVVideo.CodecJPEG;
        }

        if (formatValue == null && m_capturePhotoOutput.AvailablePhotoCodecTypes.Contains(AVVideo.CodecH264))
        {
            formatValue = AVVideo.CodecH264;
        }

        if (formatValue == null) return null;
        
        var settings = AVCapturePhotoSettings.FromFormat(new NSDictionary<NSString, NSObject>(formatKey, formatValue));
        return settings;
    }
#pragma warning restore CA1422
    
}

public class PhotoCaptureDelegate(Action<AVCapturePhoto> onPhotoCaptured) : AVCapturePhotoCaptureDelegate
{
    private Action<AVCapturePhoto>? m_onPhotoCaptured = onPhotoCaptured;

    public override void DidCapturePhoto(AVCapturePhotoOutput captureOutput, AVCaptureResolvedPhotoSettings resolvedSettings)
    {
        
    }

    public override void DidFinishCapture(AVCapturePhotoOutput captureOutput, AVCaptureResolvedPhotoSettings resolvedSettings,
        NSError? error)
    {
    }

    public override void DidFinishProcessingPhoto(AVCapturePhotoOutput output, AVCapturePhoto photo, NSError? error)
    {
        m_onPhotoCaptured?.Invoke(photo);
    }

    protected override void Dispose(bool disposing)
    {
        m_onPhotoCaptured = null;
        base.Dispose(disposing);
    }
}