using System.Diagnostics;
using Android.Graphics;
using Android.Graphics.Drawables;
using AndroidX.Camera.Core;
using AndroidX.Camera.Core.ResolutionSelector;
using AndroidX.Camera.Video;
using AndroidX.Camera.View;
using AndroidX.Core.Content;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared.Android;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Size = Android.Util.Size;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : CameraFragment
{
    private AndroidX.Camera.Core.ImageCapture? m_cameraCaptureUseCase;

    private partial Task PlatformStart()
    {
        var resolutionSelector = new ResolutionSelector.Builder().SetResolutionStrategy(new ResolutionStrategy(new Size(1280, 720), ResolutionStrategy.FallbackRuleClosestLower)).Build();
        m_cameraCaptureUseCase = new AndroidX.Camera.Core.ImageCapture.Builder()
            .SetResolutionSelector(resolutionSelector)
            .Build();
        return m_cameraPreview != null ? base.SetupCameraAndTryStartUseCase(m_cameraPreview, m_cameraCaptureUseCase) : Task.CompletedTask;
    }

    private partial Task PlatformStop()
    {
        return base.TryStop();
    }

    public override async void OnStarted()
    {
        if (m_cameraPreview?.Handler is CameraPreviewHandler previewHandler)
        {
            previewHandler.AddView(new Button
            {
                Text = "Capture"
            });
        }

        await Task.Delay(800);
        m_cameraCaptureUseCase?.TakePicture(ContextCompat.GetMainExecutor(Context),
            new ImageCaptureCallback(OnImageCaptured));
        
        
    }

    private async void OnImageCaptured(IImageProxy image)
    {
        //REMEMBER ROTATION.
        var bitmapImage = image.ToBitmap();
        using var stream = new MemoryStream();
        var stopWatch = Stopwatch.StartNew();
        await bitmapImage.CompressAsync(Bitmap.CompressFormat.Png!, 100, stream);
        stopWatch.Stop();
        Console.WriteLine($"Captured: {stopWatch.ElapsedMilliseconds}ms");
        InvokeOnImageCaptured(new CapturedImage(stream.ToArray()));
    }
}

internal class ImageCaptureCallback : AndroidX.Camera.Core.ImageCapture.OnImageCapturedCallback
{
    private Action<IImageProxy>? m_invokeOnImageCaptured;

    public ImageCaptureCallback(Action<IImageProxy> invokeOnImageCaptured)
    {
        m_invokeOnImageCaptured = invokeOnImageCaptured;
    }
    
    public override async void OnCaptureSuccess(IImageProxy image)
    {
        m_invokeOnImageCaptured?.Invoke(image);
        base.OnCaptureSuccess(image);
        image?.Close();
    }

    protected override void Dispose(bool disposing)
    {
        m_invokeOnImageCaptured = null;
        base.Dispose(disposing);
    }
}