using DIPS.Mobile.UI.Components.ChipGroup;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Resources.Styles.Chip;
using Microsoft.Maui.Layouts;

namespace DIPS.Mobile.UI.Components.TabView;

public partial class TabView : ContentView
{
    private readonly TabViewItem m_selectedItem = new TabViewItem(new Tab(), new object());
    private readonly FlexLayout m_flexLayout = new () { Wrap = FlexWrap.Wrap, Direction = FlexDirection.Row, AlignItems = FlexAlignItems.Start };
    private readonly List<TabViewItem> m_tabItems = [];

    public TabView()
    { 
        Content = m_flexLayout;
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);
        
        if(args.NewHandler is not null)
            SetTabToggledBasedOnSelectedItem();
    }
    
    private void SetTabToggledBasedOnSelectedItem()
    {

    }
}

public class TabViewItem
{
    public TabViewItem(Tab tab, object obj)
    {
        Tab = tab;
        Obj = obj;
    }

    public Tab Tab { get; }
    public object Obj { get; }
}