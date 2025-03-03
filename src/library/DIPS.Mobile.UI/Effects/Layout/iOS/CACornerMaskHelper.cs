using CoreAnimation;

namespace DIPS.Mobile.UI.Effects.Layout;

public static class CACornerMaskHelper
{
    public static CACornerMask GetCACornerMask(CornerRadius cornerRadius)
    {
        var mask = (CACornerMask)0;
        if (cornerRadius.TopLeft > 0)
        {
            mask |= CACornerMask.MinXMinYCorner;
        }
        if (cornerRadius.TopRight > 0)
        {
            mask |= CACornerMask.MaxXMinYCorner;
        }
        if (cornerRadius.BottomLeft > 0)
        {
            mask |= CACornerMask.MinXMaxYCorner;
        }
        if (cornerRadius.BottomRight > 0)
        {
            mask |= CACornerMask.MaxXMaxYCorner;
        }
        return mask;
    } 
}