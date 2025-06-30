using Android.Widget;
using Google.Android.Material.Tabs;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.TabView.Android;
using Microsoft.Maui.Handlers;
using DIPS.Mobile.UI.Extensions.Android;

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
            tabLayout.SetTabTextColors(VirtualView.DefaultTextColor.ToPlatform(), VirtualView.SelectedTextColor.ToPlatform());
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
                var tab = platformView.NewTab().SetText(title);
                platformView.AddTab(tab);
            }
            for (int i = 0; i < _tabLayout.TabCount; i++)
            {
                var tab = _tabLayout.GetTabAt(i);
                if (tab != null)
                {
                    TextView customTab = new TextView(Context);
                    customTab.Text = tab.Text;
                    customTab.SetTextAppearance(Resource.Style.TabStyle);
                    tab.SetCustomView(customTab);
                }
            }
            
        }

        void UpdateSelectedItem()
        {
            var platformView = PlatformView;
            var selectedItem = VirtualView.SelectedItem;

            if (selectedItem == null || VirtualView.ItemsSource == null)
                return;
            var newTabItem = new TabItem() { Title = selectedItem.Title, Counter = selectedItem.Counter };
            var index = VirtualView.ItemsSource.IndexOf((TabItem)selectedItem);
            if (index >= 0 && index < platformView.TabCount)
            {
                var tab = platformView.GetTabAt(index);
                tab?.Select();
            }
        }
        
        
}
