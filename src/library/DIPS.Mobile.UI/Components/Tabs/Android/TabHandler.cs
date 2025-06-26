using Android.Text;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Extensions.Android;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Google.Android.Material.Tabs;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Tabs;

public class TabHandler : ViewHandler<Tab, Google.Android.Material.Tabs.TabItem>
{
    public TabHandler() : base(PropertyMapper)
    {
    }

    public static readonly IPropertyMapper<Tab, TabHandler> PropertyMapper = new PropertyMapper<Tab, TabHandler>(ViewMapper)
    {
        [nameof(TabItem.Text)] = MapTitle,
    };
    
    
    protected override Google.Android.Material.Tabs.TabItem CreatePlatformView() => new(Context);

    protected void ConnectHandler(Google.Android.Material.Tabs.TabItem platformView)
    {
        //base.ConnectHandler(platformView);
        platformView.SetPadding((int)Sizes.GetSize(SizeName.content_margin_small).ToMauiPixel(), (int)Sizes.GetSize(SizeName.content_margin_xsmall).ToMauiPixel(), (int)Sizes.GetSize(SizeName.content_margin_small).ToMauiPixel(), (int)Sizes.GetSize(SizeName.content_margin_xsmall).ToMauiPixel());
        
        var fontManager = MauiContext?.Services.GetRequiredService<IFontManager>();
    }

    private static void MapTitle(TabHandler handler, Tab tab)
    {
        handler.PlatformView.Text = new Java.Lang.String(tab.Title);    
    }
    
    protected void DisconnectHandler(Google.Android.Material.Tabs.TabItem platformView)
    {
        //base.DisconnectHandler(platformView);
    }
}