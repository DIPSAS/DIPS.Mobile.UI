namespace DIPS.Mobile.UI.Components.Toolbar;

public partial class ToolbarHandler
{
    public ToolbarHandler() : base(PropertyMapper)
    {
    }

    public static IPropertyMapper<Toolbar, ToolbarHandler> PropertyMapper =
        new PropertyMapper<Toolbar, ToolbarHandler>
        {
            [nameof(Toolbar.Groups)] = MapGroups,
            [nameof(Toolbar.HorizontalAlignment)] = MapHorizontalAlignment,
        };

    private static partial void MapGroups(ToolbarHandler handler, Toolbar toolbar);
    private static partial void MapHorizontalAlignment(ToolbarHandler handler, Toolbar toolbar);
}
