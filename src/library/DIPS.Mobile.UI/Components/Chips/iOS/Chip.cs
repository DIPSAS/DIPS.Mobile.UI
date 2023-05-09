using CoreAnimation;
using CoreGraphics;
using UIKit;

namespace DIPS.Mobile.UI.Components.Chips;

public partial class Chip
{
    partial void UpdateCornerRadius()
    {
        var roundedCornerRadius = RetrieveCommonCornerRadius(CornerRadius);
        if (roundedCornerRadius <= 0)
        {
            return;
        }

        var roundedCorners = RetrieveRoundedCorners(CornerRadius);

        var path = UIBezierPath.FromRoundedRect(Bounds, roundedCorners, new CGSize(roundedCornerRadius, roundedCornerRadius));
        var mask = new CAShapeLayer { Path = path.CGPath };

        var test = Handler.PlatformView;
        //()(Handler.PlatformView).Layer.Mask = mask;
    }
    
    private double RetrieveCommonCornerRadius(CornerRadius cornerRadius)
    {
        var commonCornerRadius = cornerRadius.TopLeft;
        if (commonCornerRadius <= 0)
        {
            commonCornerRadius = cornerRadius.TopRight;
            if (commonCornerRadius <= 0)
            {
                commonCornerRadius = cornerRadius.BottomLeft;
                if (commonCornerRadius <= 0)
                {
                    commonCornerRadius = cornerRadius.BottomRight;
                }
            }
        }

        return commonCornerRadius;
    }
    
    private UIRectCorner RetrieveRoundedCorners(CornerRadius cornerRadius)
    {
        var roundedCorners = default(UIRectCorner);

        if (cornerRadius.TopLeft > 0)
        {
            roundedCorners |= UIRectCorner.TopLeft;
        }

        if (cornerRadius.TopRight > 0)
        {
            roundedCorners |= UIRectCorner.TopRight;
        }

        if (cornerRadius.BottomLeft > 0)
        {
            roundedCorners |= UIRectCorner.BottomLeft;
        }

        if (cornerRadius.BottomRight > 0)
        {
            roundedCorners |= UIRectCorner.BottomRight;
        }

        return roundedCorners;
    }
}