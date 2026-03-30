namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;

/// <summary>
/// Determines how the camera behaves when the shutter button is tapped.
/// </summary>
public enum CaptureMode
{
    /// <summary>
    /// Single capture mode: after capturing, the user sees a confirmation screen
    /// where they can accept, retake, or edit the photo.
    /// </summary>
    Single = 0,

    /// <summary>
    /// Multi capture mode: after capturing, the image is immediately delivered
    /// via the callback and the camera stays in streaming state for the next capture.
    /// </summary>
    Multi = 1
}
