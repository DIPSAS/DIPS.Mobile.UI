namespace DIPS.Mobile.UI.Effects.Layout;

public partial class Layout
{
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.CreateAttached("CornerRadius",
        typeof(CornerRadius),
        typeof(Layout),
        new CornerRadius(0),
        propertyChanged: OnCornerRadiusPropertiesChanged);
}