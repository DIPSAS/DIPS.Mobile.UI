using Android.Widget;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Google.Android.Material.Tabs;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.TabView.Android;
using Microsoft.Maui.Handlers;

public class TabViewHandler : ViewHandler<TabView, Google.Android.Material.Tabs.TabLayout>
{
    private TabLayout _tabLayout;
    public TabViewHandler() : base(PropertyMapper)
    {
    }
    
    public static readonly IPropertyMapper<TabView, TabViewHandler> PropertyMapper = new PropertyMapper<TabView, TabViewHandler>(ViewMapper)
    {
        [nameof(TabView.ItemsSource)] = MapItemsSource,
        [nameof(TabView.SelectedItem)] = MapSelectedItem
    };

    protected override TabLayout CreatePlatformView()
    {
        var tabLayout = new TabLayout(Context);
        tabLayout.TabMode = TabLayout.ModeScrollable;
        tabLayout.SetBackgroundColor(Colors.GetColor(ColorName.color_palette_neutral_transparent).ToPlatform());
        _tabLayout = tabLayout;
        
        return tabLayout;
    }

    protected async override void ConnectHandler(TabLayout platformView)
    {
        base.ConnectHandler(platformView);

        UpdateItemsSource();
        UpdateSelectedItem();

        platformView.TabSelected += OnTabSelected;
    }

    protected override void DisconnectHandler(TabLayout platformView)
    {
        platformView.TabSelected -= OnTabSelected;
        base.DisconnectHandler(platformView);
    }

    void OnTabSelected(object sender, TabLayout.TabSelectedEventArgs e)
    {
        var index = e.Tab.Position;
        if (VirtualView.ItemsSource != null && index >= 0 && index < VirtualView.ItemsSource.Count)
        {
            VirtualView.SelectedItem = VirtualView.ItemsSource[index];
        }
    }

    public static void MapItemsSource(TabViewHandler handler, TabView tabView)
    {
        handler.UpdateItemsSource();
    }

    public static void MapSelectedItem(TabViewHandler handler, TabView tabView)
    {
        handler.UpdateSelectedItem();
    }

    public void UpdateItemsSource()
    {
        var platformView = PlatformView;
        platformView.RemoveAllTabs();

        if (VirtualView.ItemsSource == null)
            return;

        foreach (var item in VirtualView.ItemsSource)
        {
            var title = item.Title;
            title += item.Counter is null or 0 ? "" : " ("+ item.Counter + ")";
            var tab = platformView.NewTab().SetText(title);
            platformView.AddTab(tab);
        }

        UpdateTabColor();
    }

    void UpdateSelectedItem()
    {
        var platformView = PlatformView;
        var selectedItem = VirtualView.SelectedItem;

        if (selectedItem == null || VirtualView.ItemsSource == null)
            return;

        var index = VirtualView.ItemsSource.IndexOf(selectedItem);
        if (index >= 0 && index < platformView.TabCount)
        {
            var tab = platformView.GetTabAt(index);
            tab?.Select();
        }
        UpdateTabColor();
    }
    
    //_tabLayout.SetTabTextColors is not working dynamically, so must set for each tab, fix in future versions?
    private void UpdateTabColor()
    {
        var fontManager = MauiContext?.Services.GetRequiredService<IFontManager>();

        for (var i = 0; i < _tabLayout.TabCount; i++)
        {
            var tab = _tabLayout.GetTabAt(i);
            if (tab != null)
            {
                var customTab = new TextView(Context);
                customTab.Text = tab.Text;
                customTab.SetTextAppearance(Resource.Style.TabStyle);
                if (tab.IsSelected)
                {
                    customTab.SetTextColor(VirtualView.SelectedTextColor.ToPlatform());
                    customTab.UpdateFont(textStyle: new Label { Style = VirtualView.SelectedTextStyle }, fontManager!);
                }
                else
                {
                    customTab.SetTextColor(VirtualView.DefaultTextColor.ToPlatform());
                    customTab.UpdateFont(textStyle: new Label { Style = VirtualView.DefaultTextStyle }, fontManager!);
                }
                
                tab.SetCustomView(customTab);
            }
        }
        

    }
}