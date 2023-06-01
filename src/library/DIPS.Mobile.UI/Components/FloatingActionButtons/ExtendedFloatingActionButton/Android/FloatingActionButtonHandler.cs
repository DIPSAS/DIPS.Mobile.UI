using Android.Content.Res;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingActionButton.Android;

public class FloatingActionButtonHandler : ViewHandler<ExtendedFloatingActionButton.ExtendedFloatingActionButton, Google.Android.Material.FloatingActionButton.ExtendedFloatingActionButton>
{
    public FloatingActionButtonHandler() : base(ViewMapper)
    {
    }

    protected override Google.Android.Material.FloatingActionButton.ExtendedFloatingActionButton CreatePlatformView()
    {
        var fab = new Google.Android.Material.FloatingActionButton.ExtendedFloatingActionButton(Context);
        fab.Text = "Hei!";
        var icon = Icons.GetIcon(IconName.ascending_fill);
        return fab;
    }

    protected override void ConnectHandler(Google.Android.Material.FloatingActionButton.ExtendedFloatingActionButton platformView)
    {
        base.ConnectHandler(platformView);
        
        var colorStateList = new ColorStateList(
            new[] { new int[] { } },
            new[] { (int)Colors.GetColor(ColorName.color_system_white).ToPlatform() });

        platformView.BackgroundTintList = colorStateList;
        //platformView.SetBackgroundColor(Colors.GetColor(ColorName.color_system_black).ToPlatform());
        platformView.Extend();
    }
}