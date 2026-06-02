namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.StreamingState;

/// <summary>
/// The control shown next to the shutter in the streaming bottom bar.
/// </summary>
internal abstract record StreamingTrailingControl
{
    /// <summary>Flash on/off toggle. Used by single capture and multi capture with per-image confirmation.</summary>
    internal sealed record Flash : StreamingTrailingControl;

    /// <summary>
    /// Finish button that ends the capture session.
    /// </summary>
    internal sealed record FinishCapture(string Text, Action OnTapped) : StreamingTrailingControl;
}
