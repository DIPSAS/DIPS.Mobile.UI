namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;

/// <summary>
/// Determines what should happen after people have confirmed their capture.
/// </summary>
public enum PostCaptureAction
{
    /// <summary>
    /// The camera will automatically close.
    /// </summary>
    /// <remarks> No need for you to call <see cref="ImageCapture.Stop" /></remarks>
    Close = 0,
    /// <summary>
    /// The camera will continue so people can capture more images.
    /// </summary>
    Continue=1
}