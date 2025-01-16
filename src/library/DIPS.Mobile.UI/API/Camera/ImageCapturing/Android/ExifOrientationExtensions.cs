using AndroidX.ExifInterface.Media;
using DIPS.Mobile.UI.API.Library;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public static class ExifOrientationExtensions
{
    public static OrientationDegree ToTrueOrientationDegree(this ExifInterface exifInterface)
    {
        var orientation = exifInterface.GetAttributeInt(ExifInterface.TagOrientation, (int)OrientationDegree.Orientation0);
        return orientation switch
        {
            ExifInterface.OrientationRotate90 => OrientationDegree.Orientation0,
            ExifInterface.OrientationRotate180 => OrientationDegree.Orientation90,
            ExifInterface.OrientationRotate270 => OrientationDegree.Orientation180,
            ExifInterface.OrientationNormal => OrientationDegree.Orientation270,
            _ => OrientationDegree.Orientation0
        };
    }
}