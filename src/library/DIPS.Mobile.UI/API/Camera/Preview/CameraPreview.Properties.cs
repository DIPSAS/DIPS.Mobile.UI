namespace DIPS.Mobile.UI.API.Camera.Preview;

public partial class CameraPreview
{
    /// <summary>
    /// Determines if the view should act as if it is covering the entire screen
    /// </summary>
    public bool IsInFullscreen { get; set; } = true;
    
    /// <summary>
    /// Determines if people has used the zoom functionality.
    /// </summary>
    public bool HasZoomed { get; internal set; }
}