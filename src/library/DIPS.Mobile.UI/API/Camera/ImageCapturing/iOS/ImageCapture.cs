using AVFoundation;
using CoreMedia;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.API.Camera.Shared.iOS;
using DIPS.Mobile.UI.Extensions.iOS;
using Foundation;
using ImageIO;
using UIKit;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : CameraSession
{
#nullable disable
    private AVCapturePhotoOutput m_capturePhotoOutput;
    private PhotoCaptureDelegate m_photoCaptureDelegate;
#nullable enable

    private partial Task PlatformStart(ImageCaptureSettings imageCaptureSettings, CameraFailed cameraFailedDelegate)
    {
        m_capturePhotoOutput = new AVCapturePhotoOutput() { };

        m_photoCaptureDelegate = new PhotoCaptureDelegate(PhotoCaptured, PlatformOnCameraFailed);
        
        return base.ConfigureAndStart(m_cameraPreview, imageCaptureSettings.MaxHeightOrWidth, m_capturePhotoOutput, cameraFailedDelegate);
    }

    private partial Task PlatformStop()
    {
        m_capturePhotoOutput = null;
        m_photoCaptureDelegate?.Dispose();
        m_photoCaptureDelegate = null;
        
        return base.StopCameraSession();
    }

    public override void ConfigureSession()
    {
        OnCameraStartedCrossPlatform((float)PreviewView!.Frame.Height, GetCurrentCameraResolution());
    }

    private Size GetCurrentCameraResolution()
    {
        if (CaptureDevice?.ActiveFormat.FormatDescription is CMVideoFormatDescription selectedVideoFormatDescription)
        {
            return new Size(selectedVideoFormatDescription.Dimensions.Width,
                selectedVideoFormatDescription.Dimensions.Height);
        }

        return new Size(0, 0);
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
            GoToConfirmState(new CapturedImage(photo.FileDataRepresentation.ToArray(),
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

    private async partial void PlatformCapturePhoto()
    {
        await this.HasStartedSession();
        
        var settings = CreateSettings();
        if (settings is null)
        {
            return;
        }
        
        UpdateCaptureOrientation(UIDevice.CurrentDevice.Orientation.ToAVCaptureVideoOrientation());
        m_capturePhotoOutput.CapturePhoto(settings, m_photoCaptureDelegate);
        DisablePreview();
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
        settings.FlashMode = m_flashActive ? AVCaptureFlashMode.On : AVCaptureFlashMode.Off;
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

internal class PhotoCaptureDelegate(Action<AVCapturePhoto> onPhotoCaptured, Action<CameraException> onPhotoCaptureFailed) : AVCapturePhotoCaptureDelegate
{
    private Action<CameraException> m_onPhotoCaptureFailed = onPhotoCaptureFailed;
    private Action<AVCapturePhoto> m_onPhotoCaptured = onPhotoCaptured;

    public override void WillBeginCapture(AVCapturePhotoOutput captureOutput, AVCaptureResolvedPhotoSettings resolvedSettings)
    {
        Console.WriteLine("--- WillBeginCapture --- | " + captureOutput + "|" + resolvedSettings);
    }

    public override void DidFinishCapture(AVCapturePhotoOutput captureOutput,
        AVCaptureResolvedPhotoSettings resolvedSettings,
        NSError? error)
    {
        Console.WriteLine("DidFinishCapture" + error?.LocalizedFailureReason);
        
        if (error != null)
        {
            m_onPhotoCaptureFailed.Invoke(new CameraException("iOS: DidFinishCapture", new Exception(error.ToExceptionMessage()), error.LocalizedDescription, error.LocalizedRecoverySuggestion));
        }
    }

    public override void DidFinishProcessingPhoto(AVCapturePhotoOutput output, AVCapturePhoto photo, NSError? error)
    {
        Console.WriteLine("DidFinishProcessingCapture" + error?.LocalizedFailureReason);
        
        if (error != null)
        {
            m_onPhotoCaptureFailed.Invoke(new CameraException("iOS: DidFinishProcessingPhoto", new Exception(error.ToExceptionMessage()), error.LocalizedDescription, error.LocalizedRecoverySuggestion));
        }
        m_onPhotoCaptured.Invoke(photo);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        m_onPhotoCaptureFailed = null!;
        m_onPhotoCaptured = null!;
    }
}