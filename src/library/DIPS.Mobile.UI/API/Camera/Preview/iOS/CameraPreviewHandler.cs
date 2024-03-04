using AVFoundation;
using CoreGraphics;
using DIPS.Mobile.UI.API.Camera.Preview.iOS;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Foundation;
using Microsoft.Maui.Handlers;
using UIKit;
using Colors = Microsoft.Maui.Graphics.Colors;
using ContentView = Microsoft.Maui.Platform.ContentView;

namespace DIPS.Mobile.UI.API.Camera.Preview;

public partial class CameraPreviewHandler : ContentViewHandler
{
    private readonly TaskCompletionSource m_hasArrangedSizeTcs = new();
    private Slider? m_slider;
    private UITapToFocusGestureRecognizer? m_tapToFocusTapGestureRecognizer;
    private UIPinchGestureRecognizer? m_pinchToZoomGestureRecognizer;

    internal UISlider? UISlider => m_slider?.Handler is SliderHandler sliderHandler ? sliderHandler.PlatformView : null;

    protected override void ConnectHandler(ContentView platformView)
    {
        PlatformView.BackgroundColor = UIColor.Black;
        base.ConnectHandler(platformView);
    }

    internal async Task<AVCaptureVideoPreviewLayer> WaitForViewLayoutAndAddSessionToPreview(AVCaptureSession session,
        AVLayerVideoGravity videoGravity)
    {
        await m_hasArrangedSizeTcs
            .Task; //Have to wait for the view to have bounds, in case consumers wants to start a session when the view is not ready.
        if (TryGetAvCaptureVideoPreviewLayer(out var oldPreviewLayer))
        {
            oldPreviewLayer?.RemoveFromSuperLayer();
        }

        var previewLayer = new AVCaptureVideoPreviewLayer() {Name = nameof(AVCaptureVideoPreviewLayer)};
        //This makes sure we display the video feed
        previewLayer.Frame = PlatformView.Bounds;
        previewLayer.Session = session;
        previewLayer.VideoGravity = videoGravity;

        PlatformView.Layer?.AddSublayer(previewLayer);
        return previewLayer;
    }

    private bool TryGetAvCaptureVideoPreviewLayer(out AVCaptureVideoPreviewLayer? previewLayer)
    {
        previewLayer = null;
        var potentialLayer =
            PlatformView.Layer?.Sublayers?.FirstOrDefault(l => l.Name == nameof(AVCaptureVideoPreviewLayer));
        if (potentialLayer is not AVCaptureVideoPreviewLayer avCaptureVideoPreviewLayer)
        {
            return false;
        }

        {
            previewLayer = avCaptureVideoPreviewLayer;
            return true;
        }
    }

    public override void PlatformArrange(Rect rect)
    {
        base.PlatformArrange(rect);
        m_hasArrangedSizeTcs.TrySetResult();
    }

    public void AddZoomSlider(AVCaptureDevice captureDevice)
    {
        if (VirtualView is Microsoft.Maui.Controls.ContentView contentView)
        {
            double bottomPadding = Sizes.GetSize(SizeName.size_3);
            var bottomSafeArea = UIApplication.SharedApplication.KeyWindow?.SafeAreaInsets.Bottom;
            if (bottomSafeArea > 0)
            {
                bottomPadding = bottomSafeArea.Value + bottomPadding;
            }

            var slider = new Slider
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.End,
                ThumbColor = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_primary_90),
                MinimumTrackColor = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_primary_90),
                Maximum = (float)Math.Min((float)captureDevice.ActiveFormat.VideoMaxZoomFactor, 8.0),
                Minimum = 1,
                Value = (float)captureDevice.VideoZoomFactor,
                Margin = new Thickness(Sizes.GetSize(SizeName.size_4), 0, Sizes.GetSize(SizeName.size_2),
                    bottomPadding)
            };

            slider.ValueChanged += (_, _) =>
            {
                captureDevice.LockForConfiguration(out var error);
                captureDevice.VideoZoomFactor = (float)slider.Value;
                captureDevice.UnlockForConfiguration();
            };

            SemanticProperties.SetHint(slider, DUILocalizedStrings.ZoomLevel);

            m_slider = slider;
            contentView.Content = slider;
        }
    }

    public void RemoveZoomSlider()
    {
        UISlider?.RemoveFromSuperview();
        m_slider = null;
    }

    internal void SetFocusPoint(double x, double y, AVCaptureDevice captureDevice, out string? error)
    {
        var previewSize = PlatformView.Bounds.Size;
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

    public void AddTapToFocus(AVCaptureDevice captureDevice)
    {
        m_tapToFocusTapGestureRecognizer =
            new UITapToFocusGestureRecognizer(((set, @event) => TapToFocus(set, @event, captureDevice)));
        PlatformView.AddGestureRecognizer(m_tapToFocusTapGestureRecognizer);
    }

    public void RemoveTouchToFocus()
    {
        if (m_tapToFocusTapGestureRecognizer == null)
        {
            return;
        }

        PlatformView.RemoveGestureRecognizer(m_tapToFocusTapGestureRecognizer);
        m_tapToFocusTapGestureRecognizer = null;
    }

    private void TapToFocus(NSSet touches, UIEvent @event, AVCaptureDevice captureDevice)
    {
        if (touches.First() is not UITouch touchPoint) return;
        SetFocusPoint(touchPoint.LocationInView(PlatformView).X, touchPoint.LocationInView(PlatformView).Y,
            captureDevice, out var error);

        if (error != null)
        {
            Console.WriteLine(error);
        }

        // VibrationService.SelectionChanged();
        UISlider?.BecomeFirstResponder(); //Make sure slider does not loose focus for people to slide it after they tap to focus
    }

    public void AddPinchToZoom(AVCaptureDevice captureDevice)
    {
        m_pinchToZoomGestureRecognizer =
            new UIPinchGestureRecognizer((recognizer => PinchToZoom(recognizer, captureDevice)));
        PlatformView.AddGestureRecognizer(m_pinchToZoomGestureRecognizer);
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
                    // Check if desiredZoomFactor fits required range from 1.0 to activeFormat.videoMaxZoomFactor
                    var zoomFactor = (nfloat)Math.Max(1.0,
                        Math.Min(desiredZoomFactor, captureDevice.ActiveFormat.VideoMaxZoomFactor));
                    captureDevice.VideoZoomFactor = zoomFactor;

                    if (m_slider != null) //Synchronize ZoomSlider if its added 
                    {
                        m_slider.Value = zoomFactor;
                    }
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

    public void RemovePinchToZoom()
    {
        if (m_pinchToZoomGestureRecognizer != null)
        {
            PlatformView.RemoveGestureRecognizer(m_pinchToZoomGestureRecognizer);
        }
    }
}