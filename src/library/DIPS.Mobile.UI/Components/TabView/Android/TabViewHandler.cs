using Android.Widget;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Google.Android.Material.Tabs;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.TabView.Android;
using Microsoft.Maui.Handlers;

public class TabViewHandler : ViewHandler<TabView, Google.Android.Material.Tabs.TabLayout>
{
    private TabLayout m_tabLayout;
    public TabViewHandler() : base(PropertyMapper)
    {
    }
    
    public static readonly IPropertyMapper<TabView, TabViewHandler> PropertyMapper = new PropertyMapper<TabView, TabViewHandler>(ViewMapper)
    {
        [nameof(TabView.ItemsSource)] = MapItemsSource,
        [nameof(TabView.SelectedTabIndex)] = MapSelectedTabIndex
    };

    protected override TabLayout CreatePlatformView()
    {
        var tabLayout = new TabLayout(Context);
        tabLayout.TabMode = TabLayout.ModeScrollable;
        tabLayout.SetBackgroundColor(Colors.GetColor(ColorName.color_palette_neutral_transparent).ToPlatform());
        var density = Context.Resources.DisplayMetrics.Density;
        tabLayout.SetPadding(0, 0, 0, (int)(Sizes.GetSize(SizeName.size_2) * density));
        m_tabLayout = tabLayout;
        
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
    
    async void OnTabSelected(object sender, TabLayout.TabSelectedEventArgs e)
    {
        if (await TrySwitchTab())
        {
            return;
        }

        var index = e.Tab.Position;
        if (VirtualView.ItemsSource != null && index >= 0 && index < VirtualView.ItemsSource.Count)
        {
            VirtualView.SetSelectedTabIndex(index);
        }
    }

    // This method is called when the user tries to switch tabs, to check if a confirmation is needed and if switching shall proceed.
    private async Task<bool> TrySwitchTab()
    {
        if (VirtualView.CanSwitchTab != null)
        {
            UpdateTabColor();
            var result = await VirtualView.CanSwitchTab(VirtualView.SelectedTabIndex);
            if (!result)
            {
                m_tabLayout.TabSelected -= OnTabSelected; // Temporarily remove the event handler to prevent recursion
                m_tabLayout.SelectTab(m_tabLayout.GetTabAt(VirtualView.SelectedTabIndex));
                UpdateTabColor();
                m_tabLayout.TabSelected += OnTabSelected;

                return true;
            }
        }

        return false;
    }

    public static void MapItemsSource(TabViewHandler handler, TabView tabView)
    {
        handler.UpdateItemsSource();
    }

    public static void MapSelectedTabIndex(TabViewHandler handler, TabView tabView)
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
            
            var counterString = item.Counter is null ? "" : ", " + item.Counter + DUILocalizedStrings.Elements;
            var semanticDescriptionCombinedText = item.Title + counterString;
            tab.SetContentDescription(semanticDescriptionCombinedText);
            platformView.AddTab(tab);
        }

        UpdateTabColor();
    }

    void UpdateSelectedItem()
    {
        var platformView = PlatformView;
        var selectedTabIndex = VirtualView.SelectedTabIndex;

        if (VirtualView.ItemsSource == null)
            return;

        if (selectedTabIndex >= 0 && selectedTabIndex < platformView.TabCount)
        {
            var tab = platformView.GetTabAt(selectedTabIndex);
            tab?.Select();
        }
        UpdateTabColor();
    }
    
    //m_tabLayout.SetTabTextColors is not working dynamically, so must set for each tab, fix in future versions?
    private void UpdateTabColor()
    {
        var fontManager = MauiContext?.Services.GetRequiredService<IFontManager>();

        for (var i = 0; i < m_tabLayout.TabCount; i++)
        {
            var tab = m_tabLayout.GetTabAt(i);
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