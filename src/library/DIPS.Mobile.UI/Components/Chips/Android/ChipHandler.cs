using DIPS.Mobile.UI.Components.Chips.Android;
using Microsoft.Maui.Handlers;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Chips;

public partial class ChipHandler : ViewHandler<Chip, Google.Android.Material.Chip.Chip>
{
    protected override Google.Android.Material.Chip.Chip CreatePlatformView()
    {
        return new Google.Android.Material.Chip.Chip(Context);
    }
    
    protected override void ConnectHandler(Google.Android.Material.Chip.Chip platformView)
    {
        base.ConnectHandler(platformView);
        platformView.SetDefaultChipAttributes();
        platformView.Click += OnChipTapped;
    }

    protected override void DisconnectHandler(Google.Android.Material.Chip.Chip platformView)
    {
        base.DisconnectHandler(platformView);
        platformView.Click -= OnChipTapped;
    }

    private static partial void MapTitle(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.Text = chip.Title;
    }

}