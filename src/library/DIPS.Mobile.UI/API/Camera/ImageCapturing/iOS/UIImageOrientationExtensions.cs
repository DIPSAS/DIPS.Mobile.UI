using DIPS.Mobile.UI.API.Library;
using UIKit;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public static class UIImageOrientationExtensions
{
    /// <summary>
    /// Converts a <see cref="UIImageOrientation"/> to the correct <see cref="OrientationDegree"/>
    /// </summary>
    public static OrientationDegree ToTrueOrientationDegree(this UIImageOrientation uiImageOrientation) => uiImageOrientation switch
    {
        UIImageOrientation.Up => OrientationDegree.Orientation90,
        UIImageOrientation.Right => OrientationDegree.Orientation0,
        UIImageOrientation.Left => OrientationDegree.Orientation180,
        UIImageOrientation.Down => OrientationDegree.Orientation270
    };
    
    /// <summary>
    /// Converts a <see cref="UIImageOrientation"/> to the correct <see cref="OrientationDegree"/>
    /// </summary>
    public static OrientationDegree ToOrientationDegree(this UIImageOrientation uiImageOrientation) => uiImageOrientation switch
    {
        UIImageOrientation.Up => OrientationDegree.Orientation0,
        UIImageOrientation.Right => OrientationDegree.Orientation90,
        UIImageOrientation.Down => OrientationDegree.Orientation180,
        UIImageOrientation.Left => OrientationDegree.Orientation270
    };
    
    public static UIImageOrientation ToUiImageOrientation(this OrientationDegree orientationDegree) => orientationDegree switch
    {
        OrientationDegree.Orientation0 => UIImageOrientation.Up,
        OrientationDegree.Orientation90 => UIImageOrientation.Right,
        OrientationDegree.Orientation180 => UIImageOrientation.Down,
        OrientationDegree.Orientation270 => UIImageOrientation.Left
    };
}