namespace DIPS.Mobile.UI.Components.Dividers;

public partial class DividerHandler
{
    public DividerHandler() : base(PropertyMapper)
    {
    }

    public static IPropertyMapper<Divider, DividerHandler> PropertyMapper = new PropertyMapper<Divider, DividerHandler>(ViewMapper)
    {
        [nameof(Divider.BackgroundColor)] = MapOverrideBackgroundColor
    };

    private static partial void MapOverrideBackgroundColor(DividerHandler handler, Divider divider);

}