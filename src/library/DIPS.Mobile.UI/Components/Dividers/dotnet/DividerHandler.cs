using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Dividers;

public partial class DividerHandler : ViewHandler<Divider, Only_Here_For_UnitTests>
{
    protected override Only_Here_For_UnitTests CreatePlatformView() => new Only_Here_For_UnitTests();

    private static partial void MapOverrideBackgroundColor(DividerHandler handler, Divider divider)
    {
    }

}