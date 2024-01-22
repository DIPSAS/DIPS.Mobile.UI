namespace DIPS.Mobile.UI.Effects.Layout;

public partial class Layout
{
    public static readonly BindableProperty UniformCornerRadiusProperty = BindableProperty.CreateAttached("UniformCornerRadius",
        typeof(int),
        typeof(Layout),
        0,
        propertyChanged: OnCornerRadiusPropertiesChanged);
}