using Google.Android.Material.Divider;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Dividers;

public partial class DividerHandler : ViewHandler<Divider, MaterialDivider>
{
    protected override MaterialDivider CreatePlatformView()
    {
        return new MaterialDivider(Context);
    }

    private static partial void MapOverrideBackgroundColor(DividerHandler handler, Divider divider)
    {
        handler.PlatformView.SetBackgroundColor(divider.BackgroundColor.ToPlatform());
    }
}