using AVFoundation;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = Microsoft.Maui.Graphics.Colors;
using ContentView = Microsoft.Maui.Platform.ContentView;

namespace DIPS.Mobile.UI.API.Camera;

public partial class PreviewHandler : ContentViewHandler
{
    private readonly TaskCompletionSource m_hasArrangedSizeTcs = new();
    private Slider? m_slider;

    internal UISlider? UISlider => m_slider?.Handler is SliderHandler sliderHandler ? sliderHandler.PlatformView : null;


    internal async Task<AVCaptureVideoPreviewLayer> WaitForViewLayoutAndAddSessionToPreview(AVCaptureSession session,
        AVLayerVideoGravity videoGravity)
    {
        await m_hasArrangedSizeTcs
            .Task; //Have to wait for the view to have bounds, in case consumers wants to start a session when the view is not ready.
        var previewLayer = new AVCaptureVideoPreviewLayer();
        //This makes sure we display the video feed
        previewLayer.Frame = PlatformView.Bounds;
        previewLayer.Session = session;
        previewLayer.VideoGravity = videoGravity;

        PlatformView.Layer.AddSublayer(previewLayer);
        return previewLayer;
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
            var slider = new Slider
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.End,
                ThumbColor = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_primary_90),
                MinimumTrackColor = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_primary_90),
                Maximum = (float)Math.Min((float)captureDevice.ActiveFormat.VideoMaxZoomFactor, 8.0),
                Minimum = 1,
                Value = (float)captureDevice.VideoZoomFactor,
                Margin = new Thickness(0, 0, 0, 40)
            };

            slider.ValueChanged += (_, _) =>
            {
                captureDevice.LockForConfiguration(out var error);
                captureDevice.VideoZoomFactor = (float)slider.Value;
                captureDevice.UnlockForConfiguration();
            };
            m_slider = slider;
            contentView.Content = slider;
        }
    }

    internal void SetFocusPoint(double x, double y, AVCaptureDevice captureDevice, out string? error)
    {
        var previewSize = PlatformView.Bounds.Size;
        var focusPoint = new CGPoint(x: y / previewSize.Height,
            y: 1.0 - x / previewSize.Width);

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

                captureDevice.UnlockForConfiguration();
            }
            catch (Exception e)
            {
                error = e.Message;
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