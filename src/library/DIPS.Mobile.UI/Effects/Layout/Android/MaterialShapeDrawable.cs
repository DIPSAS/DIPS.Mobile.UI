using Android.Content.Res;
using DIPS.Mobile.UI.Extensions.Android;
using Google.Android.Material.Shape;
using Color = Android.Graphics.Color;

namespace DIPS.Mobile.UI.Effects.Layout;

public static class MaterialShapeDrawableHelper
{
    public static MaterialShapeDrawable GetMaterialShapeDrawableFromCornerRadius(CornerRadius cornerRadius)
    {
        var builder = new ShapeAppearanceModel.Builder();

        // iOS does not support uneven corner radius, so we do the same on Android
        var highestCornerRadius = (float)cornerRadius.HighestCornerRadius().ToMauiPixel()!;
        
        if (cornerRadius.TopLeft != 0)
        {
            builder.SetTopLeftCorner(CornerFamily.Rounded, highestCornerRadius);
        }
        if (cornerRadius.TopRight != 0)
        {
            builder.SetTopRightCorner(CornerFamily.Rounded, highestCornerRadius);
        }
        if (cornerRadius.BottomLeft != 0)
        {
            builder.SetBottomLeftCorner(CornerFamily.Rounded, highestCornerRadius);
        }
        if (cornerRadius.BottomRight != 0)
        {
            builder.SetBottomRightCorner(CornerFamily.Rounded, highestCornerRadius);
        }

        return new MaterialShapeDrawable(builder.Build()) { StrokeWidth = 0 };
    }
}