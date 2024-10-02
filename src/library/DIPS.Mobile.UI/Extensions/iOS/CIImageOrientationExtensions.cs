using CoreImage;
using UIKit;

namespace DIPS.Mobile.UI.Extensions.iOS;

public static class CIImageOrientationExtensions
{
    public static UIImageOrientation ToUIImageOrientation(this CIImageOrientation? orientation)
    {
        return orientation switch
        {
            CIImageOrientation.TopLeft => UIImageOrientation.Up // TopLeft
            ,
            CIImageOrientation.TopRight => UIImageOrientation.UpMirrored // TopRight
            ,
            CIImageOrientation.BottomRight => UIImageOrientation.Down // BottomRight
            ,
            CIImageOrientation.BottomLeft => UIImageOrientation.DownMirrored // BottomLeft
            ,
            CIImageOrientation.LeftTop => UIImageOrientation.LeftMirrored // LeftTop
            ,
            CIImageOrientation.RightTop => UIImageOrientation.Right // RightTop
            ,
            CIImageOrientation.RightBottom => UIImageOrientation.RightMirrored // RightBottom
            ,
            CIImageOrientation.LeftBottom => UIImageOrientation.Left // LeftBottom
            ,
            _ => UIImageOrientation.Up
        };
    }
}