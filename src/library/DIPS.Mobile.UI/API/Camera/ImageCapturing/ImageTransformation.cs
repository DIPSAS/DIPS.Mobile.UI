using DIPS.Mobile.UI.API.Library;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public class ImageTransformation
{
    public ImageTransformation(OrientationDegree orientationDegree, string orientationDisplayName)
    {
        OrientationDegree = orientationDegree;
        OrientationDisplayName = orientationDisplayName;
    }
    
    public OrientationDegree OrientationDegree { get; set; }
    public string OrientationDisplayName { get; }
}