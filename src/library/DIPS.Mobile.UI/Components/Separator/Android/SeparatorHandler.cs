using Google.Android.Material.Divider;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Separator;

public partial class SeparatorHandler : ViewHandler<Separator, MaterialDivider>
{
    protected override MaterialDivider CreatePlatformView()
    {
        return new MaterialDivider(Context);
    }

    public static partial void MapOverrideBackgroundColor(SeparatorHandler handler, Separator separator)
    {
        handler.PlatformView.SetBackgroundColor(separator.BackgroundColor.ToPlatform());
    }
}