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
        [nameof(Tab.Title)] = MapTitle,
        [nameof(Tab.IsSelected)] = MapIsSelected,
    };
    
    protected override Google.Android.Material.Tabs.TabItem CreatePlatformView() => new(Context);

    protected void ConnectHandler(Google.Android.Material.Tabs.TabItem platformView)
    {
        base.ConnectHandler(platformView);
        platformView.SetPadding((int)Sizes.GetSize(SizeName.content_margin_small).ToMauiPixel(), (int)Sizes.GetSize(SizeName.content_margin_xsmall).ToMauiPixel(), (int)Sizes.GetSize(SizeName.content_margin_small).ToMauiPixel(), (int)Sizes.GetSize(SizeName.content_margin_xsmall).ToMauiPixel());
        platformView.Click += OnTabTapped;
    }

    private static void MapTitle(TabHandler handler, Tab tab)
    {
        var counterText = tab.Counter.Length > 0 ? $" ({tab.Counter})" : string.Empty;
        var combinedText = $"{tab.Title}{counterText}";

        handler.PlatformView.Text = new Java.Lang.String(combinedText);    
    }
    
    private static void MapIsSelected(TabHandler handler, Tab tab)
    {
        handler.PlatformView.Selected = tab.IsSelected;
    }
    
    private void OnTabTapped(object? sender, EventArgs e)
    {
        OnTabTapped();
    }
    
    internal void OnTabTapped()
    {
        VirtualView.SendTapped();
    }
    
    protected void DisconnectHandler(Google.Android.Material.Tabs.TabItem platformView)
    {
        base.DisconnectHandler(platformView);
        platformView.Click -= OnTabTapped;
    }
}