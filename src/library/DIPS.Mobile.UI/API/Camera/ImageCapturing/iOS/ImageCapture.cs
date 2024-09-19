using AVFoundation;
using CoreGraphics;
using CoreMedia;
using DIPS.Mobile.UI.API.Camera.Extensions.iOS;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared.iOS;
using DIPS.Mobile.UI.Internal.Logging;
using Foundation;
using ImageIO;
using UIKit;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : CameraSession
{
    private AVCapturePhotoOutput? m_capturePhotoOutput;
    private ImageCaptureSettings? m_imageCaptureSettings;

    private partial Task PlatformStart(ImageCaptureSettings imageCaptureSettings, CameraFailed cameraFailedDelegate)
    {
        m_imageCaptureSettings = imageCaptureSettings;
        m_capturePhotoOutput = new AVCapturePhotoOutput();
        if (m_cameraPreview != null)
        {
            return base.ConfigureAndStart(m_cameraPreview, AVCaptureSession.PresetHigh, m_capturePhotoOutput, cameraFailedDelegate);
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
        if (m_cameraPreview is { CameraZoomView: not null })
        {
            m_cameraPreview.CameraZoomView.Margin = new Thickness(0, 0, 0, 120);
        }
    }

    private partial void PlatformOnCameraFailed(CameraException cameraException) =>
        OnCameraFailed<ImageCapture>(cameraException);

    public override AVCaptureDevice? SelectCaptureDevice() =>
        AVCaptureDevice.GetDefaultDevice(AVCaptureDeviceType.BuiltInWideAngleCamera,
            AVMediaTypes.Video, AVCaptureDevicePosition.Back) ??
        AVCaptureDevice.GetDefaultDevice(AVCaptureDeviceType.BuiltInDualCamera,
            AVMediaTypes.Video, AVCaptureDevicePosition.Back);

    private void PhotoCaptured(AVCapturePhoto photo)
    {
        if (photo.FileDataRepresentation != null && m_imageCaptureSettings != null)
        {
            SwitchToConfirmState(new CapturedImage(photo.FileDataRepresentation.ToArray(),
                    TryGetThumbnail(photo), 
                    photo, 
                    new ImageTransformation((int?)photo.Properties.Orientation ?? 0, 
                        photo.Properties.Orientation.ToString() ?? "Unknown")),
                m_imageCaptureSettings);
        }
    }

    private static byte[]? TryGetThumbnail(AVCapturePhoto photo)
    {
        if (photo.FileDataRepresentation == null)
        {
            return null;
        }

        var cgImageSource = CGImageSource.FromData(photo.FileDataRepresentation);
        var thumbnailCgImage = cgImageSource?.CreateThumbnail(0, new CGImageThumbnailOptions()
        {
            CreateThumbnailWithTransform = true //Makes sure to rotate if needed 
        });
        return thumbnailCgImage == null ? null : new UIImage(thumbnailCgImage).AsJPEG()?.ToArray();

    }

    private partial void PlatformCapturePhoto()
    {
        var settings = CreateSettings();
        if (settings is not null)
        {
            UpdateCaptureOrientation(UIDevice.CurrentDevice.Orientation.ToAVCaptureVideoOrientation());
            m_capturePhotoOutput?.CapturePhoto(settings, new PhotoCaptureDelegate(PhotoCaptured, PlatformOnCameraFailed));
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
        settings.EmbeddedThumbnailPhotoFormat = new AVCapturePhotoSettingsThumbnailFormat()
        {
            Codec = AVVideo.CodecJPEG,
            Width = Sizes.GetSize(SizeName.size_18),
            Height = Sizes.GetSize(SizeName.size_18),
        };
        return settings;
    }
#pragma warning restore CA1422
}

public class PhotoCaptureDelegate(Action<AVCapturePhoto> onPhotoCaptured, Action<CameraException> onPhotoCaptureFailed) : AVCapturePhotoCaptureDelegate
{
    private Action<AVCapturePhoto>? m_onPhotoCaptured = onPhotoCaptured;
    private Action<CameraException>? m_onPhotoCaptureFailed = onPhotoCaptureFailed;

    public override void DidCapturePhoto(AVCapturePhotoOutput captureOutput,
        AVCaptureResolvedPhotoSettings resolvedSettings)
    {
    }

    public override void DidFinishCapture(AVCapturePhotoOutput captureOutput,
        AVCaptureResolvedPhotoSettings resolvedSettings,
        NSError? error)
    {
        if (error != null)
        {
            m_onPhotoCaptureFailed?.Invoke(new CameraException("iOS: DidFinishProcessingPhoto", new Exception(error.ToExceptionMessage())));
        }
    }

    public override void DidFinishProcessingPhoto(AVCapturePhotoOutput output, AVCapturePhoto photo, NSError? error)
    {
        if (error != null)
        {
            m_onPhotoCaptureFailed?.Invoke(new CameraException("iOS: DidFinishProcessingPhoto", new Exception(error.ToExceptionMessage())));
        }
        m_onPhotoCaptured?.Invoke(photo);
    }

    protected override void Dispose(bool disposing)
    {
        m_onPhotoCaptured = null;
        m_onPhotoCaptureFailed = null;
        base.Dispose(disposing);
    }
}