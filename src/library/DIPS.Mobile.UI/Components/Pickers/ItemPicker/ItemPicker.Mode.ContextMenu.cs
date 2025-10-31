using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Components.ContextMenus;

namespace DIPS.Mobile.UI.Components.Pickers.ItemPicker;

public partial class ItemPicker
{
    private ContextMenuItem? m_emptyContextMenuItem;
    
    private void UpdateContextMenuItems()
    {
        if (m_contextMenu == null)
        {
            return;
        }
            
        if (m_contextMenu.ItemsSource == null)
        {
            return;
        }
            
        foreach (var item in m_contextMenu.ItemsSource)
        {
            if(item is ContextMenuItem contextMenuItem)
                contextMenuItem.IsChecked = false;
        }

        if (SelectedItem is not null)
        {
            var selectedItem = m_contextMenu.ItemsSource?.FirstOrDefault(i =>
            {
                if (i is ContextMenuItem item)
                {
                    return item.Title != null && 
                           item.Title.Equals(SelectedItem.GetPropertyValue(ItemDisplayProperty), 
                               StringComparison.InvariantCultureIgnoreCase);
                }

                return false;
            });
            if (selectedItem is ContextMenuItem contextMenuItem)
            {
                contextMenuItem.IsChecked = true;
            }
        }
        else if (m_emptyContextMenuItem is not null)
        {
            m_emptyContextMenuItem.IsChecked = true;
        }
            
        m_contextMenu.SendItemsSourceUpdated();
    }

    private void AddContextMenuItems()
    {
        if (ItemsSource == null)
        {
            return;
        }
        m_contextMenu.ItemsSource!.Clear();

        if (AllowEmpty)
        {
            m_emptyContextMenuItem = new ContextMenuItem
            {
                Title = EmptyItemTitle,
                IsChecked = SelectedItem == null,
                IsCheckable = true
            };
            m_contextMenu.ItemsSource?.Add(m_emptyContextMenuItem);
        }
        else
        {
            m_emptyContextMenuItem = null;
        }
        
        foreach (var obj in ItemsSource)
        {
            var itemDisplayName = obj.GetPropertyValue(ItemDisplayProperty);
            m_contextMenu.ItemsSource?.Add(new ContextMenuItem
            {
                Title = itemDisplayName, 
                IsChecked = SelectedItem == obj, 
                IsCheckable = true
            });
        }

        if (AdditionalContextMenuItem is not null)
        {
            m_contextMenu.ItemsSource?.Add(new ContextMenuSeparatorItem());
            m_contextMenu.ItemsSource?.Add(AdditionalContextMenuItem);
        }
            
        m_contextMenu.SendItemsSourceUpdated();
    }

    private void SetSelectedItemBasedOnContextMenuItem(ContextMenuItem item)
    {
        // If the item tapped is the AdditionalContextMenuItem, we do not want to change the SelectedItem
        if (item == AdditionalContextMenuItem)
        {
            return;
        }
        
        if (HasHaptics)
        {
            VibrationService.SelectionChanged();    
        }
            
        // If the SelectedItem is tapped again, we need to update the context menu items, so that the item is still checked
        if (SelectedItem == GetItemFromDisplayProperty(item.Title!))
        {
            UpdateContextMenuItems();    
            return;
        }
            
        SelectedItem = GetItemFromDisplayProperty(item.Title!);
    }
}