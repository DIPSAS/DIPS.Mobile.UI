using AVFoundation;
using CoreGraphics;
using UIKit;

namespace DIPS.Mobile.UI.API.Camera.Preview.iOS;

public class PreviewView : UIView
{
    public PreviewView()
    {
        BackgroundColor = UIColor.DarkGray;    
    }

    public override void TraitCollectionDidChange(UITraitCollection? previousTraitCollection)
    {
        //Makes sure to rotate the camera and the preview layer from the bounds of this UIView which automatically resizes.
        var potentialLayer = Layer.Sublayers?.FirstOrDefault(l => l is AVCaptureVideoPreviewLayer);
        if (potentialLayer is not AVCaptureVideoPreviewLayer videoPreviewLayer) return;
        if (videoPreviewLayer?.Connection == null) return;
#pragma warning disable CA1422
        videoPreviewLayer.Orientation = UIDevice.CurrentDevice.Orientation switch
        {
            UIDeviceOrientation.Unknown => AVCaptureVideoOrientation.Portrait,
            UIDeviceOrientation.Portrait => AVCaptureVideoOrientation.Portrait,
            UIDeviceOrientation.PortraitUpsideDown => AVCaptureVideoOrientation.PortraitUpsideDown,
            UIDeviceOrientation.LandscapeLeft => AVCaptureVideoOrientation.LandscapeRight,
            UIDeviceOrientation.LandscapeRight => AVCaptureVideoOrientation.LandscapeLeft,
            UIDeviceOrientation.FaceUp => AVCaptureVideoOrientation.Portrait,
            UIDeviceOrientation.FaceDown => AVCaptureVideoOrientation.PortraitUpsideDown,
            _ => throw new ArgumentOutOfRangeException()
        };
        if (videoPreviewLayer.Connection.SupportsVideoOrientation)
        {
            videoPreviewLayer.Connection.VideoOrientation = videoPreviewLayer.Orientation;
            videoPreviewLayer.Frame = this.Bounds;
        }

        base.TraitCollectionDidChange(previousTraitCollection);
#pragma warning restore CA1422
    }
}