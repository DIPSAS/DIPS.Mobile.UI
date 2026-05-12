using System.Windows.Input;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;

public class CameraOptions
{
    /// <summary>Invoked when the user taps the cancel button.</summary>
    public ICommand? CancelButtonCommand { get; set; }

    /// <summary>Text on the cancel button.</summary>
    /// <remarks>Defaults to <see cref="DUILocalizedStrings.Cancel"/>.</remarks>
    public string CancelButtonText { get; set; } = DUILocalizedStrings.Cancel;

    /// <summary>
    /// Upper bound, in pixels, for the longest side of the captured image. The camera
    /// picks the closest resolution it can produce at or below this value.
    /// </summary>
    /// <remarks>When null, the camera uses the highest resolution the device supports.</remarks>
    public int? MaxHeightOrWidth { get; set; }

    /// <summary>
    /// If true, the user can change <see cref="MaxHeightOrWidth"/> at runtime from the
    /// in-camera settings sheet, and the top toolbar shows a settings icon.
    /// If false, the value is read-only and the toolbar shows an info icon.
    /// </summary>
    public bool CanChangeMaxHeightOrWidth { get; set; }

    internal CameraInfo CameraInfo { get; } = new();
}

public record MultiImageCaptureOptions
{
    /// <summary>
    /// If true, each captured image must be confirmed before it is delivered.
    /// If false, images are delivered as soon as they are captured and the camera stays ready for the next capture.
    /// </summary>
    public bool RequiresConfirmationOnEachImage { get; init; }

    /// <summary>Text on the finished button.</summary>
    /// <remarks>Defaults to <see cref="DUILocalizedStrings.Done"/>.</remarks>
    public string FinishedButtonText { get; init; } = DUILocalizedStrings.Done;

    /// <summary>Invoked when the user taps the finished button to confirm the captured images.</summary>
    public ICommand? FinishedButtonCommand { get; init; }
}

internal class CameraInfo
{
    internal Size CurrentCameraResolution { get; set; }
}
