using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Toolbar;

public partial class ToolbarHandler : ViewHandler<Toolbar, Toolbar> 
{
    private static partial void MapGroups(ToolbarHandler handler, Toolbar toolbar)
    {
    }

    private static partial void MapHorizontalAlignment(ToolbarHandler handler, Toolbar toolbar)
    {
    }

    protected override Toolbar CreatePlatformView()
    {
        return new Toolbar();
    }
}
