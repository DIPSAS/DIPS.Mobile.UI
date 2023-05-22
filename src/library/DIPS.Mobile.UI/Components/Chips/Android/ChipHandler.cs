using Android.Content.Res;
using DIPS.Mobile.UI.Platforms.Android;
using DIPS.Mobile.UI.Resources.Colors;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using TextAlignment = Android.Views.TextAlignment;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Chips;

public partial class ChipHandler : ViewHandler<Chip, Google.Android.Material.Chip.Chip>
{

    protected override Google.Android.Material.Chip.Chip CreatePlatformView()
    {
        return new Google.Android.Material.Chip.Chip(Context) {  };
    }
    
    protected override void ConnectHandler(Google.Android.Material.Chip.Chip platformView)
    {
        base.ConnectHandler(platformView);
        
        platformView.SetDefaultChipAttributes();
    }
    
    private static partial void MapTitle(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.Text = chip.Title;
    }

}