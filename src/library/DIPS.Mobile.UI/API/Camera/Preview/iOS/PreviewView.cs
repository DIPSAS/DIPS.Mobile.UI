using AVFoundation;
using CoreGraphics;
using CoreMedia;
using UIKit;
using ContentView = Microsoft.Maui.Platform.ContentView;

namespace DIPS.Mobile.UI.API.Camera.Preview.iOS;
#pragma warning disable CA1422
public class PreviewView : ContentView
{
    public PreviewView()
    {
        BackgroundColor = UIColor.Black;    
    }

    public AVCaptureVideoPreviewLayer PreviewLayer { get; internal set; }

    public override void LayoutSubviews()
    {
        UpdateRotation();
        base.LayoutSubviews();
    }

    private void UpdateRotation()
    {
        //Makes sure to rotate the camera and the preview layer from the bounds of this UIView which automatically resizes.
        var potentialLayer = Layer.Sublayers?.FirstOrDefault(l => l is AVCaptureVideoPreviewLayer);
        if (potentialLayer is not AVCaptureVideoPreviewLayer videoPreviewLayer) return;
        if (videoPreviewLayer?.Connection == null) return;

        var orientation = UIDevice.CurrentDevice.Orientation switch
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

        if (videoPreviewLayer.Orientation == orientation)
        {
            return;
        }

        videoPreviewLayer.Orientation = orientation;
        if (videoPreviewLayer.Session?.Connections != null) //Update all connections, failing to do so will only update preview connection, but we also need to update capture connection.
        {
            foreach (var avCaptureConnection in videoPreviewLayer.Session?.Connections)
            {
                if (avCaptureConnection.SupportsVideoOrientation)
                {
                    avCaptureConnection.VideoOrientation = videoPreviewLayer.Orientation;    
                }
                
            }
        }

        videoPreviewLayer.Frame = this.Bounds;
    }

    public void AddRectOfInterest(CMVideoDimensions formatDimensions)
    {
        
    }

    public void AddPreviewLayer(AVCaptureSession? session, AVLayerVideoGravity videoGravity)
    {
        if (TryGetAvCaptureVideoPreviewLayer(out var oldPreviewLayer))
        {
            oldPreviewLayer?.RemoveFromSuperLayer();
        }

        PreviewLayer = new AVCaptureVideoPreviewLayer() {Name = nameof(AVCaptureVideoPreviewLayer),};

        //This makes sure we display the video feed
        PreviewLayer.Frame = this.Layer.Bounds;
        PreviewLayer.Session = session;
        PreviewLayer.VideoGravity = videoGravity;

        this.Layer?.AddSublayer(PreviewLayer);
    }
    
    private bool TryGetAvCaptureVideoPreviewLayer(out AVCaptureVideoPreviewLayer? previewLayer)
    {
        previewLayer = null;
        var potentialLayer =
            this.Layer?.Sublayers?.FirstOrDefault(l => l.Name == nameof(AVCaptureVideoPreviewLayer));
        if (potentialLayer is not AVCaptureVideoPreviewLayer avCaptureVideoPreviewLayer)
        {
            return false;
        }

        {
            previewLayer = avCaptureVideoPreviewLayer;
            return true;
        }
    }
    
    internal void SetFocusPoint(double x, double y, AVCaptureDevice captureDevice, out string? error)
    {
        error = "Unable to find av capture layer on the preview";
        if (TryGetAvCaptureVideoPreviewLayer(out var previewLayer))
        {
            if (previewLayer == null) return;
            var focusPoint = previewLayer.CaptureDevicePointOfInterestForPoint(new CGPoint(x, y));

            // var focusPoint = new CGPoint(x: y / previewSize.Height,
            //     y: 1.0 - x / previewSize.Width);

            Console.WriteLine(focusPoint);
            error = null;
            if (captureDevice.LockForConfiguration(out var configurationLockError))
            {
                try
                {
                    if (captureDevice.FocusPointOfInterestSupported)
                    {
                        captureDevice.FocusPointOfInterest = focusPoint;
                        captureDevice.FocusMode = AVCaptureFocusMode.AutoFocus;
                    }

                    if (captureDevice.ExposurePointOfInterestSupported)
                    {
                        captureDevice.ExposurePointOfInterest = focusPoint;
                        captureDevice.ExposureMode = AVCaptureExposureMode.AutoExpose;
                    }
                }
                catch (Exception e)
                {
                    error = e.Message;
                }
                finally
                {
                    captureDevice.UnlockForConfiguration();
                }
            }
            else
            {
                if (configurationLockError != null)
                {
                    error = configurationLockError.ToString();
                }
            }
        }
    }
}
#pragma warning restore CA1422
