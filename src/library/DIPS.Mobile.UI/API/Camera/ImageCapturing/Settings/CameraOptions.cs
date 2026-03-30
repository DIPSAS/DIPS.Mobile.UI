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

    public int? MaxHeightOrWidth { get; set; }
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
