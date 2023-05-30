using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Effects.DUIImageEffect;

public partial class DUIImageEffect
{
    public static readonly BindableProperty ImageColorProperty = BindableProperty.CreateAttached("ImageColor",
        typeof(Color),
        typeof(DUIImageEffect),
        Colors.GetColor(ColorName.color_system_black),
        propertyChanged:OnImageColorChanged);
}