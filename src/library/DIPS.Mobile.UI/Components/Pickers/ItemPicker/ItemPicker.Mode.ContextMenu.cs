using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Components.ContextMenus;

namespace DIPS.Mobile.UI.Components.Pickers.ItemPicker;

public partial class ItemPicker
{
    private void UpdateContextMenuItems()
    {
        if (m_contextMenu == null ||
            m_contextMenu.ItemsSource!.FirstOrDefault() is not ContextMenuGroup contextMenuGroup)
        {
            return;
        }
            
        if (contextMenuGroup.ItemsSource == null)
        {
            return;
        }
            
        foreach (var item in contextMenuGroup.ItemsSource)
        {
            item.IsChecked = false;
        }

        if (SelectedItem is not null)
        {
            var contextMenuItem = contextMenuGroup.ItemsSource?.FirstOrDefault(i =>
                i.Title != null && i.Title.Equals(SelectedItem.GetPropertyValue(ItemDisplayProperty),
                    StringComparison.InvariantCultureIgnoreCase));
            if (contextMenuItem != null)
            {
                contextMenuItem.IsChecked = true;
            }
        }
            
        m_contextMenu.SendItemsSourceUpdated();
    }

    private void AddContextMenuItems()
    {
        if (ItemsSource == null)
        {
            return;
        }

        var group = new ContextMenuGroup() {IsCheckable = true};
        foreach (var obj in ItemsSource)
        {
            var itemDisplayName = obj.GetPropertyValue(ItemDisplayProperty);
            group.ItemsSource?.Add(new ContextMenuItem()
            {
                Title = itemDisplayName, IsChecked = SelectedItem == obj
            });
        }

        m_contextMenu.ItemsSource!.Clear();
        m_contextMenu.ItemsSource.Add(group);
            
        m_contextMenu.SendItemsSourceUpdated();
    }

    private void SetSelectedItemBasedOnContextMenuItem(ContextMenuItem item)
    {
        if (HasHaptics)
        {
            VibrationService.SelectionChanged();    
        }
            
        SelectedItem = GetItemFromDisplayProperty(item.Title!);
    }
}