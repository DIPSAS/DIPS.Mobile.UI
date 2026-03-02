namespace DIPS.Mobile.UI.API.PictureInPicture;

/// <summary>
/// Service for entering Picture in Picture (PiP) mode, allowing the app to be displayed
/// in a small floating window while the user interacts with other apps or navigates away.
/// </summary>
public static partial class PipService
{
    /// <summary>
    /// Gets a value indicating whether Picture in Picture mode is supported on this device.
    /// </summary>
    public static partial bool IsSupported { get; }

    /// <summary>
    /// Enters Picture in Picture mode. The app will be displayed in a small floating window
    /// with a default 9:16 (portrait) aspect ratio.
    /// </summary>
    /// <remarks>
    /// On Android, the activity must have <c>android:supportsPictureInPicture="true"</c> in the manifest,
    /// or <c>SupportsPictureInPicture = true</c> in the <c>[Activity]</c> attribute.
    /// On iOS 15+, this captures a snapshot of the current window and displays it in a PiP window using
    /// <c>AVPictureInPictureController</c> with <c>AVSampleBufferDisplayLayer</c>. Requires
    /// <c>UIBackgroundModes</c> audio capability in the app's <c>Info.plist</c>.
    /// </remarks>
    public static partial void Enter();

    /// <summary>
    /// Enters Picture in Picture mode with a specific aspect ratio for the PiP window.
    /// </summary>
    /// <param name="ratioWidth">The width component of the desired aspect ratio.</param>
    /// <param name="ratioHeight">The height component of the desired aspect ratio.</param>
    /// <remarks>
    /// On Android, the activity must have <c>android:supportsPictureInPicture="true"</c> in the manifest,
    /// or <c>SupportsPictureInPicture = true</c> in the <c>[Activity]</c> attribute.
    /// On iOS 15+, this captures a snapshot of the current window and displays it in a PiP window using
    /// <c>AVPictureInPictureController</c> with <c>AVSampleBufferDisplayLayer</c>. Requires
    /// <c>UIBackgroundModes</c> audio capability in the app's <c>Info.plist</c>.
    /// </remarks>
    public static partial void Enter(int ratioWidth, int ratioHeight);

    /// <summary>
    /// Raised when the app enters or exits Picture in Picture mode.
    /// </summary>
    /// <remarks>
    /// On Android, override <c>OnPictureInPictureModeChanged</c> in your <c>MainActivity</c> and call
    /// <see cref="NotifyPipModeChanged"/> for this event to fire.
    /// </remarks>
    public static event EventHandler<bool>? PipModeChanged;

    /// <summary>
    /// Notifies the service that the PiP mode has changed.
    /// Call this from your <c>MainActivity.OnPictureInPictureModeChanged</c> override on Android.
    /// </summary>
    /// <param name="isInPipMode"><c>true</c> if the app entered PiP mode; <c>false</c> if it exited.</param>
    public static void NotifyPipModeChanged(bool isInPipMode)
    {
        PipModeChanged?.Invoke(null, isInPipMode);
    }
}
