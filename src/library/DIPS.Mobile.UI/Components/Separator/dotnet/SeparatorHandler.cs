using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Separator;

public partial class SeparatorHandler : ViewHandler<Separator, Only_Here_For_UnitTests>
{
    protected override Only_Here_For_UnitTests CreatePlatformView()
    {
        return new Only_Here_For_UnitTests();
    }
}