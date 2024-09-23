using AVFoundation;
using CoreAnimation;
using CoreGraphics;
using CoreMedia;
using DIPS.Mobile.UI.Extensions.iOS;
using Foundation;
using Microsoft.Maui.Controls.Shapes;
using UIKit;
using ContentView = Microsoft.Maui.Platform.ContentView;

namespace DIPS.Mobile.UI.API.Camera.Preview.iOS;
#pragma warning disable CA1422
internal sealed class PreviewView : ContentView
{
    private AVCaptureDevice? m_avCaptureDevice;
    private readonly TaskCompletionSource m_hasArrangedSizeTcs = new();
    private UITapToFocusGestureRecognizer? m_tapToFocusTapGestureRecognizer;
    private UIPinchGestureRecognizer m_pinchToZoomGestureRecognizer;
    private AVCaptureVideoPreviewLayer? m_previewLayer;

    public static readonly int MaxZoomRatio = 8;
    
    public PreviewView()
    {
        BackgroundColor = UIColor.Black;
    }

    public AVCaptureVideoPreviewLayer PreviewLayer { get; internal set; }

    public event Action<float>? OnZoomChanged;
    public event Action<float, float>? OnTapToFocus;
    
    public override void LayoutSubviews()
    {
        UpdateOrientation();
        base.LayoutSubviews();
        
        m_hasArrangedSizeTcs.TrySetResult();
    }
    
    public void AddTapToFocus(AVCaptureDevice captureDevice)
    {
        m_tapToFocusTapGestureRecognizer =
            new UITapToFocusGestureRecognizer(((set, @event) => TapToFocus(set, @event, captureDevice)));
        AddGestureRecognizer(m_tapToFocusTapGestureRecognizer);
    }
    
    public void RemoveTouchToFocus()
    {
        if (m_tapToFocusTapGestureRecognizer == null)
        {
            return;
        }

        RemoveGestureRecognizer(m_tapToFocusTapGestureRecognizer);
        m_tapToFocusTapGestureRecognizer = null;
    }
    
    private void TapToFocus(NSSet touches, UIEvent @event, AVCaptureDevice captureDevice)
    {
        if (touches.First() is not UITouch touchPoint) 
            return;
        
        var touchPointLocation = touchPoint.LocationInView(this);
        
        OnTapToFocus?.Invoke((float)touchPointLocation.X, (float)touchPointLocation.Y);
        
        SetFocusPoint(touchPointLocation.X, touchPointLocation.Y,
            captureDevice, out var error);

        if (error != null)
        {
            Console.WriteLine(error);
        }

        // VibrationService.SelectionChanged();
        /*UISlider?.BecomeFirstResponder();*/ //Make sure slider does not loose focus for people to slide it after they tap to focus
    }
    
    public void AddPinchToZoom(AVCaptureDevice captureDevice)
    {
        m_pinchToZoomGestureRecognizer =
            new UIPinchGestureRecognizer((recognizer => PinchToZoom(recognizer, captureDevice)));
        AddGestureRecognizer(m_pinchToZoomGestureRecognizer);
    }
    
    //Taken from: https://stackoverflow.com/a/31214458
    private void PinchToZoom(UIPinchGestureRecognizer pinchRecognizer, AVCaptureDevice captureDevice)
    {
        if (pinchRecognizer.State == UIGestureRecognizerState.Changed)
        {
            if (captureDevice.LockForConfiguration(out var configurationLockError))
            {
                try
                {
                    var pinchVelocityDividerFactor = 5.0f;
                    var desiredZoomFactor = captureDevice.VideoZoomFactor +
                                            Math.Atan2(pinchRecognizer.Velocity, pinchVelocityDividerFactor);
                    
                    var maxZoomFactor = Math.Min(captureDevice.ActiveFormat.VideoMaxZoomFactor, MaxZoomRatio);
                    // Check if desiredZoomFactor fits required range from 1.0 to activeFormat.videoMaxZoomFactor
                    var zoomFactor = (nfloat)Math.Max(1.0,
                        Math.Min(desiredZoomFactor, maxZoomFactor));
                    captureDevice.VideoZoomFactor = zoomFactor;

                    OnZoomChanged?.Invoke((float)zoomFactor);
                    
                    /*if (m_slider != null) //Synchronize ZoomSlider if its added 
                    {
                        m_slider.Value = zoomFactor;
                    }
                    SetHasZoomed();*/
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
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
                    Console.WriteLine(configurationLockError.ToString());
                }
            }
        }
    }
    
    private void SetHasZoomed()
    {
        /*if (VirtualView is CameraPreview cameraPreview)
        {
            cameraPreview.HasZoomed = true;
        }*/
    }
    
    internal void RemovePinchToZoom()
    {
        if (m_pinchToZoomGestureRecognizer == null) return;
        
        RemoveGestureRecognizer(m_pinchToZoomGestureRecognizer);
        m_pinchToZoomGestureRecognizer = null;
    }

    private void RemovePreviewLayer()
    {
        m_previewLayer?.RemoveFromSuperLayer();
        m_previewLayer = null;
    }
    
    internal async Task<AVCaptureVideoPreviewLayer> WaitForViewLayoutAndAddSessionToPreview(AVCaptureDevice? avCaptureDevice, AVCaptureSession session,
        AVLayerVideoGravity videoGravity)
    {
        await m_hasArrangedSizeTcs
            .Task; //Have to wait for the view to have bounds, in case consumers wants to start a session when the view is not ready.
        
        AddPreviewLayer(avCaptureDevice, session, videoGravity);
        return PreviewLayer;
    }

    private void UpdateOrientation()
    {
        //Makes sure to rotate the camera and the preview layer from the bounds of this UIView which automatically resizes.
        if (PreviewLayer?.Connection == null) return;

        var orientation = UIDevice.CurrentDevice.Orientation.ToAVCaptureVideoOrientation();
        
        PreviewLayer.Orientation = orientation; //This will log warning: [<AVCaptureVideoPreviewLayer: 0x1d641b0> orientation] is deprecated. Please use AVCaptureConnection's -videoOrientation, but ony using videoOrientation will not work as rotation is incorrect.
        
        if (PreviewLayer.Session?.Connections is not null) //Update all connections, failing to do so will only update preview connection, but we also need to update capture connection.
        {
            foreach (var connection in PreviewLayer.Session?.Connections!)
            {
                if (connection.SupportsVideoOrientation)
                {
                    if (connection.VideoOrientation != orientation)
                    {
                        connection.VideoOrientation = PreviewLayer.Orientation;    
                    }
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

    public new void Dispose()
    {
        m_avCaptureDevice = null;
        
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        
        RemovePinchToZoom();
        RemoveTouchToFocus();    
    
        /*RemoveZoomSlider();*/
        RemovePreviewLayer();
    }
}
#pragma warning restore CA1422
