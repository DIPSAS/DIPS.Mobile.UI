using AVFoundation;
using AVKit;
using CoreGraphics;
using CoreMedia;
using CoreVideo;
using DIPS.Mobile.UI.Internal.Logging;
using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.API.PictureInPicture;

public static partial class PipService
{
    private static AVPictureInPictureController? s_pipController;
    private static PipSampleBufferView? s_pipView;
    private static PipPlaybackDelegate? s_playbackDelegate;
    private static PipControllerDelegate? s_controllerDelegate;

    private static readonly CGColorSpace s_rgbColorSpace = CGColorSpace.CreateDeviceRGB();

    public static partial bool IsSupported =>
        AVPictureInPictureController.IsPictureInPictureSupported();

    public static partial void Enter() => Enter(9, 16);

    public static partial void Enter(int ratioWidth, int ratioHeight)
    {
        if (!IsSupported)
            return;

        var window = GetKeyWindow();
        if (window is null)
            return;

        SetupAudioSession();

        var windowWidth = window.Bounds.Width;
        var windowHeight = windowWidth * ratioHeight / ratioWidth;
        var frameSize = new CGSize(windowWidth, windowHeight);

        s_pipView?.Dispose();
        s_pipView = new PipSampleBufferView(frameSize);

        s_playbackDelegate?.Dispose();
        s_playbackDelegate = new PipPlaybackDelegate(window, frameSize, s_pipView.SampleBufferDisplayLayer);

        s_controllerDelegate?.Dispose();
        s_controllerDelegate = new PipControllerDelegate();

        var contentSource = new AVPictureInPictureControllerContentSource(
            s_pipView.SampleBufferDisplayLayer,
            s_playbackDelegate);

        s_pipController?.Dispose();
        s_pipController = new AVPictureInPictureController(contentSource)
        {
            Delegate = s_controllerDelegate
        };

        s_playbackDelegate.PushCurrentFrame();

        if (s_pipController.PictureInPicturePossible)
        {
            s_pipController.StartPictureInPicture();
        }
    }

    private static UIWindow? GetKeyWindow()
    {
        foreach (var scene in UIApplication.SharedApplication.ConnectedScenes)
        {
            if (scene is UIWindowScene windowScene &&
                windowScene.ActivationState == UISceneActivationState.ForegroundActive)
            {
                return windowScene.Windows.FirstOrDefault(w => w.IsKeyWindow);
            }
        }
        return null;
    }

    private static void SetupAudioSession()
    {
        var audioSession = AVAudioSession.SharedInstance();
        if (!audioSession.SetCategory(AVAudioSessionCategory.Playback,
                AVAudioSessionCategoryOptions.MixWithOthers, out var categoryError))
        {
            DUILogService.LogError<PipService>($"PiP: Failed to set AVAudioSession category: {categoryError?.LocalizedDescription}");
        }

        if (!audioSession.SetActive(true, out var activeError))
        {
            DUILogService.LogError<PipService>($"PiP: Failed to activate AVAudioSession: {activeError?.LocalizedDescription}");
        }
    }

    private sealed class PipSampleBufferView : UIView
    {
        public AVSampleBufferDisplayLayer SampleBufferDisplayLayer { get; }

        public PipSampleBufferView(CGSize size) : base(new CGRect(CGPoint.Empty, size))
        {
            SampleBufferDisplayLayer = new AVSampleBufferDisplayLayer();
            SampleBufferDisplayLayer.Frame = Bounds;
            Layer.AddSublayer(SampleBufferDisplayLayer);
        }
    }

    [Register("PipPlaybackDelegate")]
    private sealed class PipPlaybackDelegate : NSObject, IAVPictureInPictureSampleBufferPlaybackDelegate
    {
        private readonly UIView m_sourceView;
        private readonly CGSize m_frameSize;
        private readonly AVSampleBufferDisplayLayer m_displayLayer;

        public PipPlaybackDelegate(UIView sourceView, CGSize frameSize, AVSampleBufferDisplayLayer displayLayer)
        {
            m_sourceView = sourceView;
            m_frameSize = frameSize;
            m_displayLayer = displayLayer;
        }

        public void PushCurrentFrame()
        {
            var sampleBuffer = CreateSampleBuffer();
            if (sampleBuffer is not null)
            {
                m_displayLayer.Enqueue(sampleBuffer);
                sampleBuffer.Dispose();
            }
        }

        /// <summary>
        /// Static snapshot display — play/pause controls are not applicable.
        /// </summary>
        [Export("pictureInPictureController:setPlaying:")]
        public void SetPlaying(AVPictureInPictureController pictureInPictureController, bool playing) { }

        /// <summary>
        /// Static snapshot display is always "paused" from PiP's perspective.
        /// </summary>
        [Export("pictureInPictureControllerIsPlaybackPaused:")]
        public bool IsPlaybackPaused(AVPictureInPictureController pictureInPictureController) => true;

        /// <summary>
        /// Static snapshot display has no seekable time range — skip is a no-op.
        /// </summary>
        [Export("pictureInPictureController:skipByInterval:completionHandler:")]
        public void SkipByInterval(AVPictureInPictureController pictureInPictureController, CMTime skipInterval,
            Action completionHandler)
        {
            completionHandler();
        }

        /// <summary>
        /// Returns an indefinite time range so the PiP controller does not show a playback position.
        /// </summary>
        [Export("pictureInPictureControllerTimeRangeForPlayback:")]
        public CMTimeRange GetTimeRange(AVPictureInPictureController pictureInPictureController) =>
            new CMTimeRange { Start = CMTime.Zero, Duration = CMTime.Indefinite };

        [Export("pictureInPictureController:didTransitionToRenderSize:")]
        public void DidTransitionToRenderSize(AVPictureInPictureController pictureInPictureController,
            CMVideoDimensions newRenderSize) { }

        private CMSampleBuffer? CreateSampleBuffer()
        {
            var width = (nint)m_frameSize.Width;
            var height = (nint)m_frameSize.Height;

            if (width <= 0 || height <= 0)
                return null;

            var pixelBufferAttrs = new CVPixelBufferAttributes
            {
                CGImageCompatibility = true,
                CGBitmapContextCompatibility = true
            };

            var pixelBuffer = new CVPixelBuffer(width, height, CVPixelFormatType.CV32BGRA, pixelBufferAttrs);
            pixelBuffer.Lock(CVOptionFlags.None);

            try
            {
                using var bitmapContext = new CGBitmapContext(
                    pixelBuffer.BaseAddress,
                    width, height,
                    8,
                    pixelBuffer.BytesPerRow,
                    s_rgbColorSpace,
                    CGBitmapFlags.ByteOrder32Little | CGBitmapFlags.NoneSkipFirst);

                bitmapContext.TranslateCTM(0, height);
                bitmapContext.ScaleCTM(1, -1);
                m_sourceView.Layer.RenderInContext(bitmapContext);
            }
            finally
            {
                pixelBuffer.Unlock(CVOptionFlags.None);
            }

            var formatDescription = CMVideoFormatDescription.CreateForImageBuffer(pixelBuffer);
            if (formatDescription is null)
            {
                pixelBuffer.Dispose();
                return null;
            }

            var timing = new CMSampleTimingInfo
            {
                Duration = CMTime.Invalid,
                PresentationTimeStamp = CMTime.Zero,
                DecodeTimeStamp = CMTime.Invalid
            };

            CMSampleBuffer.CreateReadyWithImageBuffer(pixelBuffer, formatDescription, timing, out var sampleBuffer);
            formatDescription.Dispose();
            pixelBuffer.Dispose();

            return sampleBuffer;
        }
    }

    [Register("PipControllerDelegate")]
    private sealed class PipControllerDelegate : AVPictureInPictureControllerDelegate
    {
        public override void DidStartPictureInPicture(AVPictureInPictureController pictureInPictureController)
        {
            NotifyPipModeChanged(true);
        }

        public override void DidStopPictureInPicture(AVPictureInPictureController pictureInPictureController)
        {
            NotifyPipModeChanged(false);
        }
    }
}
