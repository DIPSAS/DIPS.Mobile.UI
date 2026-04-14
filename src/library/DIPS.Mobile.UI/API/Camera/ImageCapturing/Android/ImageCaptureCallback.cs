using AndroidX.Camera.Core;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

internal class ImageCaptureCallback : AndroidX.Camera.Core.ImageCapture.OnImageCapturedCallback
{
    private Action? m_onCaptureStarted;
    private Action<IImageProxy>? m_onImageCaptured;
    private Action<ImageCaptureException>? m_onImageCaptureFailed;

    public ImageCaptureCallback(
        Action onCaptureStarted,
        Action<IImageProxy> onImageCaptured,
        Action<ImageCaptureException> onImageCaptureFailed)
    {
        m_onCaptureStarted = onCaptureStarted;
        m_onImageCaptured = onImageCaptured;
        m_onImageCaptureFailed = onImageCaptureFailed;
    }

    /// <summary>
    /// Called when the camera hardware has started exposing the sensor.
    /// Use this to display shutter animation or similar feedback.
    ///
    /// </summary>
    /// <remarks>
    /// From docs:
    /// " For a regular capture request, this callback is invoked right as the capture of a frame begins, so it is the
    /// most appropriate time for playing a shutter sound, or triggering UI indicators of capture. "
    /// </remarks>
    public override void OnCaptureStarted()
    {
        m_onCaptureStarted?.Invoke();
        base.OnCaptureStarted();
    }

    public override void OnError(ImageCaptureException exception)
    {
        m_onImageCaptureFailed?.Invoke(exception);
        base.OnError(exception);
        ClearDelegates();
    }

    public override void OnCaptureSuccess(IImageProxy image)
    {
        m_onImageCaptured?.Invoke(image);
        base.OnCaptureSuccess(image);
        image?.Close();
        ClearDelegates();
    }

    private void ClearDelegates()
    {
        m_onCaptureStarted = null;
        m_onImageCaptured = null;
        m_onImageCaptureFailed = null;
    }

    protected override void Dispose(bool disposing)
    {
        ClearDelegates();
        base.Dispose(disposing);
    }
}