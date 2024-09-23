namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public class ImageTransformation(int orientationConstant, string orientationDisplayName)
{
    public int OrientationConstant { get; } = orientationConstant;
    public string OrientationDisplayName { get; } = orientationDisplayName;
}