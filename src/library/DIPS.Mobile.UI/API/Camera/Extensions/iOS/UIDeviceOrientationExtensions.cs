using AVFoundation;
using UIKit;

namespace DIPS.Mobile.UI.API.Camera.Extensions.iOS;

public static class UIDeviceOrientationExtensions
{
    // ReSharper disable once InconsistentNaming
    public static AVCaptureVideoOrientation ToAVCaptureVideoOrientation(this UIDeviceOrientation deviceOrientation)
    {
        return deviceOrientation switch
        {
            UIDeviceOrientation.Unknown => AVCaptureVideoOrientation.Portrait,
            UIDeviceOrientation.Portrait => AVCaptureVideoOrientation.Portrait,
            UIDeviceOrientation.PortraitUpsideDown => AVCaptureVideoOrientation.PortraitUpsideDown,
            UIDeviceOrientation.LandscapeLeft => AVCaptureVideoOrientation.LandscapeRight, //No idea why we cant use left here, the camera will be upside down if we use left.
            UIDeviceOrientation.LandscapeRight => AVCaptureVideoOrientation.LandscapeLeft, //No idea why we cant use right here, the camera will be upside down if we use right.
            UIDeviceOrientation.FaceUp => AVCaptureVideoOrientation.Portrait,
            UIDeviceOrientation.FaceDown => AVCaptureVideoOrientation.PortraitUpsideDown,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}