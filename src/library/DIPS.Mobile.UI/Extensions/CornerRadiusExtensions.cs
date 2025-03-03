namespace DIPS.Mobile.UI.Extensions;

public static class CornerRadiusExtensions
{
    public static double HighestCornerRadius(this CornerRadius cornerRadius)
    {
        return Math.Max(cornerRadius.TopLeft, Math.Max(cornerRadius.TopRight, Math.Max(cornerRadius.BottomLeft, cornerRadius.BottomRight)));
    }
    
    public static bool IsEmpty(this CornerRadius cornerRadius)
    {
        return cornerRadius is { TopLeft: 0, TopRight: 0, BottomLeft: 0, BottomRight: 0 };
    }
}