using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Effects.ImageTint;

public partial class ImageTint
{
    public static readonly BindableProperty ImageColorProperty = BindableProperty.CreateAttached("ImageColor",
        typeof(Color),
        typeof(ImageTint),
        Colors.GetColor(ColorName.color_system_black),
        propertyChanged:OnImageColorChanged);
}