using AndroidX.ConstraintLayout.Widget;
using Microsoft.Maui.Handlers;

using FAB = Google.Android.Material.FloatingActionButton.FloatingActionButton;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.FloatingActionButton.FloatingActionButtonMenu;

public partial class FloatingActionButtonMenuHandler : ViewHandler<FloatingActionButtonMenu, ConstraintLayout>
{

    protected override ConstraintLayout CreatePlatformView()
    {
        return new ConstraintLayout(Context);
    }
}