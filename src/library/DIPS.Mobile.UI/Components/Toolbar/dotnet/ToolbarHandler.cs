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

    partial void OnToolbarButtonVisibilityChanged(ToolbarButton toolbarButton)
    {
    }

    protected override Toolbar CreatePlatformView()
    {
        return new Toolbar();
    }
}
