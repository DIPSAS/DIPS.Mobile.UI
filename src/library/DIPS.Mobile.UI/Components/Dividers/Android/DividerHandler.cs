using Google.Android.Material.Divider;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Dividers.Android;

public class DividerHandler : ViewHandler<Divider, MaterialDivider>
{
    public DividerHandler() : base(PropertyMapper)
    {
    }

    public static IPropertyMapper<Divider, DividerHandler> PropertyMapper = new PropertyMapper<Divider, DividerHandler>(ViewMapper)
        {
            [nameof(Divider.BackgroundColor)] = MapOverrideBackgroundColor
        };

    protected override MaterialDivider CreatePlatformView()
    {
        return new MaterialDivider(Context);
    }

    public static void MapOverrideBackgroundColor(DividerHandler handler, Divider separator)
    {
        handler.PlatformView.SetBackgroundColor(separator.BackgroundColor.ToPlatform());
    }
}