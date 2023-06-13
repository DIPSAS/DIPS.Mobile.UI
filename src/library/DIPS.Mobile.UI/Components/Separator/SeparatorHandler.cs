namespace DIPS.Mobile.UI.Components.Separator;

public partial class SeparatorHandler
{
    public SeparatorHandler() : base(PropertyMapper)
    {
    }

    public static IPropertyMapper<Separator, SeparatorHandler> PropertyMapper =
        new PropertyMapper<Separator, SeparatorHandler>(ViewMapper)
        {
            [nameof(Separator.BackgroundColor)] = MapOverrideBackgroundColor
        };

    public static partial void MapOverrideBackgroundColor(SeparatorHandler handler, Separator separator);

}