namespace DIPS.Mobile.UI.Components.Toolbar;

public partial class ToolbarHandler
{
    public ToolbarHandler() : base(PropertyMapper)
    {
    }

    public static readonly IPropertyMapper<Toolbar, ToolbarHandler> PropertyMapper =
        new PropertyMapper<Toolbar, ToolbarHandler>(ViewMapper)
        {
            [nameof(Toolbar.Buttons)] = MapButtons,
        };

    private static partial void MapButtons(ToolbarHandler handler, Toolbar toolbar);
}
