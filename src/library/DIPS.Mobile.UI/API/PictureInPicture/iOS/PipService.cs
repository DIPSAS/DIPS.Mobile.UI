namespace DIPS.Mobile.UI.API.PictureInPicture;

public static partial class PipService
{
    /// <summary>
    /// Picture in Picture with custom views is not supported on iOS.
    /// </summary>
    public static partial bool IsSupported => false;

    /// <summary>
    /// Not implemented on iOS. Custom view PiP requires platform-specific integration with AVKit.
    /// </summary>
    public static partial void Enter() { }

    /// <inheritdoc cref="Enter()"/>
    public static partial void Enter(int ratioWidth, int ratioHeight) { }
}
