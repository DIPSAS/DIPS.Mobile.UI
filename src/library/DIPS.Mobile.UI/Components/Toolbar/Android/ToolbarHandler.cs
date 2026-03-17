using Google.Android.Material.AppBar;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Toolbar;

public partial class ToolbarHandler : ViewHandler<Toolbar, MaterialToolbar>
{
    private static partial void MapGroups(ToolbarHandler handler, Toolbar toolbar)
    {
        // TODO: Implement Android toolbar (Material 3 Bottom App Bar)
    }

    private static partial void MapHorizontalAlignment(ToolbarHandler handler, Toolbar toolbar)
    {
        // TODO: Implement Android toolbar alignment
    }

    protected override MaterialToolbar CreatePlatformView()
    {
        return new MaterialToolbar(Platform.AppContext);
    }
}
