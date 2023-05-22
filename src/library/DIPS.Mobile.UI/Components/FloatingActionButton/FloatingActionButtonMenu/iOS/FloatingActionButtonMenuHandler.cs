using Microsoft.Maui.Handlers;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.FloatingActionButton.FloatingActionButtonMenu;

public partial class FloatingActionButtonMenuHandler : ViewHandler<FloatingActionButtonMenu, UIButton>
{
    protected override UIButton CreatePlatformView()
    {
        return new UIButton();
    }
}