namespace DIPS.Mobile.UI.Components.Toolbar;

public partial class ToolbarHandler
{
    public ToolbarHandler() : base(PropertyMapper)
    {
    }

    public static IPropertyMapper<Toolbar, ToolbarHandler> PropertyMapper =
        new PropertyMapper<Toolbar, ToolbarHandler>
        {
            [nameof(Toolbar.Buttons)] = MapButtons
        };

    private static partial void MapButtons(ToolbarHandler handler, Toolbar toolbar);
}
