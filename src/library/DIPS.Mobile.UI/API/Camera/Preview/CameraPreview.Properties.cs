namespace DIPS.Mobile.UI.API.Camera.Preview;

public partial class CameraPreview
{
    public static readonly BindableProperty TargetResolutionProperty = BindableProperty.Create(
        nameof(TargetResolution),
        typeof(Size),
        typeof(CameraPreview),
        new Size(int.MaxValue, int.MaxValue));

    /// <summary>
    /// Sets the target resolution of the camera
    /// </summary>
    public Size TargetResolution
    {
        get => (Size)GetValue(TargetResolutionProperty);
        set => SetValue(TargetResolutionProperty, value);
    }

    /// <summary>
    /// Determines if the view should act as if it is covering the entire screen
    /// </summary>
    public bool IsInFullscreen { get; set; } = true;
    
    
}