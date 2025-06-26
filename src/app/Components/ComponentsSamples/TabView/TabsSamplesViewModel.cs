using System.Collections.ObjectModel;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.TabView;

public class TabsSamplesViewModel : ViewModel
{
    public List<DIPS.Mobile.UI.Components.Tabs.Tab> TabsExample { get; set; }

    public TabsSamplesViewModel()
    {
        TabsExample = new List<DIPS.Mobile.UI.Components.Tabs.Tab>
        {
            new DIPS.Mobile.UI.Components.Tabs.Tab() { Title = "Tab 1", Counter = "3" },
            new DIPS.Mobile.UI.Components.Tabs.Tab() { Title = "Tab 2", Counter = "1" },
            new DIPS.Mobile.UI.Components.Tabs.Tab() { Title = "Tab 3" }
        };
    }
}