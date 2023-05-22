using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Handlers;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.FloatingActionButton.FloatingActionButtonMenu;

public partial class FloatingActionButtonMenuHandler : ViewHandler<FloatingActionButtonMenu, Only_Here_For_UnitTests>
{
    protected override Only_Here_For_UnitTests CreatePlatformView()
    {
        return new Only_Here_For_UnitTests();
    }
}