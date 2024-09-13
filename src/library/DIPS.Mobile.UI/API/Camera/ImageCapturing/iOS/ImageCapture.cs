using AVFoundation;
using CoreMedia;
using DIPS.Mobile.UI.API.Camera.Extensions.iOS;
using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared.iOS;
using DIPS.Mobile.UI.Internal.Logging;
using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : CameraSession
{
    private AVCapturePhotoOutput? m_capturePhotoOutput;
    private ImageCaptureSettings? m_imageCaptureSettings;

    private partial Task PlatformStart(ImageCaptureSettings imageCaptureSettings)
    {
        m_imageCaptureSettings = imageCaptureSettings;
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

    public override void ConfigureSession()
    {
    }

    public override AVCaptureDevice? SelectCaptureDevice() =>
        AVCaptureDevice.GetDefaultDevice(AVCaptureDeviceType.BuiltInWideAngleCamera,
            AVMediaTypes.Video, AVCaptureDevicePosition.Back) ??
        AVCaptureDevice.GetDefaultDevice(AVCaptureDeviceType.BuiltInDualCamera,
            AVMediaTypes.Video, AVCaptureDevicePosition.Back);

    private void PhotoCaptured(AVCapturePhoto photo)
    {
        if (photo.FileDataRepresentation != null && m_imageCaptureSettings != null)
        {
            SwitchToConfirmState(new CapturedImage(photo.FileDataRepresentation.ToArray(), photo),
                m_imageCaptureSettings);
        }
    }

    private partial void PlatformCapturePhoto()
    {
        var settings = CreateSettings();
        if (settings is not null)
        {
            UpdateCaptureOrientation(UIDevice.CurrentDevice.Orientation.ToAVCaptureVideoOrientation());
            m_capturePhotoOutput?.CapturePhoto(settings, new PhotoCaptureDelegate(PhotoCaptured));
            DisablePreview();
        }
    }

    private void DisablePreview()
    {
        if (PreviewLayer is {Connection: not null})
        {
            PreviewLayer.Connection.Enabled = false;
        }
    }

    private void UpdateCaptureOrientation(AVCaptureVideoOrientation orientation)
    {
        var captureConnection = 
            m_capturePhotoOutput?.Connections.FirstOrDefault(c =>
                c.Output != null && c.Output.Equals(m_capturePhotoOutput));
        if (captureConnection != null)
        {
            captureConnection.VideoOrientation = orientation;
        }
    }

    // Cant re-use settings for each capture, remarks from Apple doc: https://developer.apple.com/documentation/avfoundation/avcapturephotosettings#overview
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

    public override void DidCapturePhoto(AVCapturePhotoOutput captureOutput,
        AVCaptureResolvedPhotoSettings resolvedSettings)
    {
    }

    public override void DidFinishCapture(AVCapturePhotoOutput captureOutput,
        AVCaptureResolvedPhotoSettings resolvedSettings,
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