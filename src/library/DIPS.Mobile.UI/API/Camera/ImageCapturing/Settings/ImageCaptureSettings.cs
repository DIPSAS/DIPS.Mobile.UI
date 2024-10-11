using System.Windows.Input;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;

public class ImageCaptureSettings
{
    /// <summary>
    /// The command to be executed when the done button is clicked
    /// </summary>
    /// <remarks>The done button will be hidden if this is not set</remarks>
    public ICommand? DoneButtonCommand { get; set; }
    
    /// <summary>
    /// The text to be displayed on the done button
    /// </summary>
    /// <remarks>Default is <see cref="DUILocalizedStrings.Cancel"/></remarks>
    public string DoneButtonText { get; set; } = DUILocalizedStrings.Cancel;
    
    public PostCaptureAction PostCaptureAction { get; set; }
    public int? MaxHeightOrWidth { get; set; }
    public bool CanChangeMaxHeightOrWidth { get; set; }
    internal CameraInfo CameraInfo { get; } = new();
}

internal class CameraInfo
{
    internal Size CurrentCameraResolution { get; set; }
}