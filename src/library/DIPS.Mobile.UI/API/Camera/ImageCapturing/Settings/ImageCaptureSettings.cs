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
    /// Text shown on the cancel button. Defaults to the localized "Cancel" string.
    /// </summary>  
    /// <remarks>Default is <see cref="DUILocalizedStrings.Cancel"/></remarks>
    public string CancelButtonText { get; set; } = DUILocalizedStrings.Cancel;
    
    public PostCaptureAction PostCaptureAction { get; set; }

    /// <summary>
    /// Determines whether the camera operates in single-capture (with confirmation) or multi-capture (fire-and-continue) mode.
    /// </summary>
    /// <remarks>
    /// When set to <see cref="CaptureMode.Multi"/>, <see cref="PostCaptureAction"/> is ignored.
    /// In Multi mode, each capture immediately fires the <see cref="DidCaptureImage"/> callback.
    /// A "Done" button and a "Cancel" button are shown so the consumer can distinguish how the user exited.
    /// </remarks>
    public CaptureMode CaptureMode { get; set; }

    /// <summary>
    /// The command executed when the user taps the "Done" button in <see cref="CaptureMode.Multi"/> mode.
    /// </summary>
    /// <remarks>Only used in Multi mode. In Single mode this is ignored.</remarks>
    public ICommand? FinishedButtonCommand { get; set; }

    /// <summary>
    /// When <c>true</c> and <see cref="CaptureMode"/> is <see cref="CaptureMode.Multi"/>,
    /// each captured image is shown for confirmation before the callback fires and the camera returns to streaming.
    /// </summary>
    /// <remarks>Only used in Multi mode. In Single mode confirmation is always shown.</remarks>
    public bool RequiresConfirmation { get; set; }

    public int? MaxHeightOrWidth { get; set; }
    public bool CanChangeMaxHeightOrWidth { get; set; }
    internal CameraInfo CameraInfo { get; } = new();
}





internal class CameraInfo
{
    internal Size CurrentCameraResolution { get; set; }
}