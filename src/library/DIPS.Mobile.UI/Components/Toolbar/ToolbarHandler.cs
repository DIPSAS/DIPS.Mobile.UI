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
            // Prevent MAUI from overriding the native platform view's own background rendering.
            // On iOS, UIToolbar manages its own system appearance (blur/Liquid Glass);
            // on Android, we set the background explicitly in CreatePlatformView.
            ["Background"] = static (_, _) => { },
        };

    private static partial void MapButtons(ToolbarHandler handler, Toolbar toolbar);
}
