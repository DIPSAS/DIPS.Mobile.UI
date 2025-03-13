namespace DIPS.Mobile.UI.Effects.Layout;

public partial class Layout
{
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.CreateAttached("CornerRadius",
        typeof(CornerRadius),
        typeof(Layout),
        new CornerRadius(0),
        propertyChanged: OnLayoutPropertiesChanged);
    
    public static readonly BindableProperty AutoCornerRadiusProperty = BindableProperty.CreateAttached("AutoCornerRadius",
        typeof(bool?),
        typeof(Layout),
        null,
        propertyChanged: OnLayoutPropertiesChanged);
}