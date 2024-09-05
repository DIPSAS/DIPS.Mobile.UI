using Android.Graphics;
using Android.Graphics.Drawables;
using AndroidX.Camera.Core;
using AndroidX.Core.Content;
using DIPS.Mobile.UI.API.Camera.Shared.Android;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : CameraFragment
{
    private partial Task PlatformStart()
    {
        return m_cameraPreview != null ? base.TryStart(m_cameraPreview, CameraUseCase.ImageCapture) : Task.CompletedTask;
    }

    private partial Task PlatformStop()
    {
        return base.TryStop();
    }

    public override async void OnStarted()
    {
        await Task.Delay(1500);
        
        CameraController?.TakePicture(ContextCompat.GetMainExecutor(Context),new ImageCaptureCallback(OnImageCaptured));
    }

    private async void OnImageCaptured(IImageProxy image)
    {
        var bitmapImage = image.ToBitmap();
        

        PreviewView?.Overlay?.Add(new BitmapDrawable(Context?.Resources, bitmapImage));

        using var stream = new MemoryStream();
        await bitmapImage.CompressAsync(Bitmap.CompressFormat.Png!, 100, stream);
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