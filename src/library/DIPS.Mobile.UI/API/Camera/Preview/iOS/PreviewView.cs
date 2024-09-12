using AVFoundation;
using CoreAnimation;
using CoreGraphics;
using CoreMedia;
using DIPS.Mobile.UI.API.Library;
using Foundation;
using Microsoft.Maui.Controls.Shapes;
using UIKit;
using ContentView = Microsoft.Maui.Platform.ContentView;

namespace DIPS.Mobile.UI.API.Camera.Preview.iOS;
#pragma warning disable CA1422
public class PreviewView : ContentView
{
    private AVCaptureDevice? m_avCaptureDevice;

    public PreviewView()
    {
        BackgroundColor = UIColor.Black;

    }

    public AVCaptureVideoPreviewLayer PreviewLayer { get; internal set; }

    public override void LayoutSubviews()
    {
        UpdateOrientation();
        base.LayoutSubviews();
    }

    private void UpdateOrientation()
    {
        //Makes sure to rotate the camera and the preview layer from the bounds of this UIView which automatically resizes.
        if (PreviewLayer?.Connection == null) return;

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

        if (PreviewLayer.Orientation == orientation)
        {
            return;
        }

        PreviewLayer.Orientation = orientation;
        if (PreviewLayer.Session?.Connections is not null) //Update all connections, failing to do so will only update preview connection, but we also need to update capture connection.
        {
            foreach (var avCaptureConnection in PreviewLayer.Session?.Connections!)
            {
                if (avCaptureConnection.SupportsVideoOrientation)
                {
                    avCaptureConnection.VideoOrientation = PreviewLayer.Orientation;
                }
            }
        }

        TryAddOrUpdateRectOfInterest();
        PreviewLayer.Frame = this.Bounds;
    }

    public void TryAddOrUpdateRectOfInterest()
    {
        if (m_avCaptureDevice == null) return;
        
        var potentialCaptureMetadataConnection = PreviewLayer.Session?.Connections.FirstOrDefault(s => s.Output is AVCaptureMetadataOutput);
        if (potentialCaptureMetadataConnection?.Output is not AVCaptureMetadataOutput avCaptureMetadataOutput) return;
        
        var formatDimensions = ((CMVideoFormatDescription)m_avCaptureDevice.ActiveFormat.FormatDescription).Dimensions;
        var width = formatDimensions.Height / (double)formatDimensions.Width;
        var rectOfInterestHeight = 1.0;
        var xCoordinate = (1.0 - width) / 2.0;
        var yCoordinate = (1.0 - rectOfInterestHeight) / 2.0;
        var initialRectOfInterest = new CGRect(x: xCoordinate, y: yCoordinate, width: width,
            height: rectOfInterestHeight);
        avCaptureMetadataOutput.RectOfInterest = initialRectOfInterest;
        
        var rectOfInterestToLayerCoordinates = PreviewLayer.MapToLayerCoordinates(initialRectOfInterest);
        var layerName = nameof(AVCaptureMetadataOutput.RectOfInterest);
        var layer = new CAShapeLayer(){Name = layerName};
        layer.FillRule = new NSString(FillRule.EvenOdd.ToString());
        layer.FillColor = UIColor.Black.CGColor;
        layer.Opacity = 0.6f;
            
        layer.Frame = rectOfInterestToLayerCoordinates;
        layer.BorderColor = UIColor.White.CGColor;
        layer.BorderWidth = 2;
        var oldLayer = Layer.Sublayers?.FirstOrDefault(s => s.Name == layerName);
        oldLayer?.RemoveFromSuperLayer();
        Layer.AddSublayer(layer);
    }

    public void AddPreviewLayer(AVCaptureDevice? avCaptureDevice, AVCaptureSession? session, AVLayerVideoGravity videoGravity)
    {
        m_avCaptureDevice = avCaptureDevice;
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

    public void Dispose()
    {
        m_avCaptureDevice = null;
    }
}
#pragma warning restore CA1422
