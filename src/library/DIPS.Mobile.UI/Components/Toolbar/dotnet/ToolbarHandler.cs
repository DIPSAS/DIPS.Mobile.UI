using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Toolbar;

public partial class ToolbarHandler : ViewHandler<Toolbar, object>
{
    protected override object CreatePlatformView() => throw new Only_Here_For_UnitTests();

    private static partial void MapButtons(ToolbarHandler handler, Toolbar toolbar) =>
        throw new Only_Here_For_UnitTests();
}
