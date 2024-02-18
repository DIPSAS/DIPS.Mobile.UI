using AVFoundation;
using CoreAnimation;
using CoreGraphics;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = Microsoft.Maui.Graphics.Colors;
using ContentView = Microsoft.Maui.Platform.ContentView;

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
                Margin = new Thickness(0,0,0,40)
            };

            slider.ValueChanged += (_, _) =>
            {
                captureDevice.LockForConfiguration(out var error);
                captureDevice.VideoZoomFactor = (float)slider.Value;
                captureDevice.UnlockForConfiguration();
            };

            contentView.Content = slider;
        }
    }
}