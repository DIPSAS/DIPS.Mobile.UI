using DIPS.Mobile.UI.Extensions.Android;
using Google.Android.Material.Shape;

namespace DIPS.Mobile.UI.Effects.Layout;

public static class MaterialShapeDrawableHelper
{
    public static MaterialShapeDrawable CreateDrawable(CornerRadius? cornerRadius)
    {
        var builder = new ShapeAppearanceModel.Builder();

        if (cornerRadius is not null)
        {
            // iOS does not support uneven corner radius, so we do the same on Android
            var highestCornerRadius = cornerRadius.Value.HighestCornerRadius().ToMauiPixel()!;

            if (cornerRadius.Value.TopLeft != 0)
            {
                builder.SetTopLeftCorner(CornerFamily.Rounded, highestCornerRadius);
            }
            if (cornerRadius.Value.TopRight != 0)
            {
                builder.SetTopRightCorner(CornerFamily.Rounded, highestCornerRadius);
            }
            if (cornerRadius.Value.BottomLeft != 0)
            {
                builder.SetBottomLeftCorner(CornerFamily.Rounded, highestCornerRadius);
            }

            if (cornerRadius.Value.BottomRight != 0)
            {
                builder.SetBottomRightCorner(CornerFamily.Rounded, highestCornerRadius);
            }
        }

        return new MaterialShapeDrawable(builder.Build());
    }
}