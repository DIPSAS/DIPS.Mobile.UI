using AVFoundation;
using CoreAnimation;
using CoreGraphics;
using Microsoft.Maui.Handlers;
using UIKit;

namespace DIPS.Mobile.UI.API.Camera;

public partial class PreviewHandler : ContentViewHandler
{
    internal AVCaptureVideoPreviewLayer AddSessionToPreviewLayer(AVCaptureSession session,
        AVLayerVideoGravity videoGravity)
    {
        var previewLayer = new AVCaptureVideoPreviewLayer();
        //This makes sure we display the video feed
        previewLayer.Frame = PlatformView.Bounds;
        previewLayer.Session = session;
        previewLayer.VideoGravity = videoGravity;

        PlatformView.Layer.AddSublayer(previewLayer);
        return previewLayer;
    }
}