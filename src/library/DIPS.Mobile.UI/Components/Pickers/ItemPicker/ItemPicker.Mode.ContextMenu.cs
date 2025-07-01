using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Extensions;

namespace DIPS.Mobile.UI.Components.Pickers.ItemPicker
{
    public partial class ItemPicker
    {
        private void UpdateContextMenuItems()
        {
            if (m_contextMenu?.ItemsSource is null)
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
                        return item.Title != null && item.Title.Equals(SelectedItem.GetPropertyValue(ItemDisplayProperty),
                            StringComparison.InvariantCultureIgnoreCase);
                    }

                    return false;
                });

                if (selectedItem is ContextMenuItem contextMenuItem)
                    contextMenuItem.IsChecked = true;
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
            if (item == AdditionalContextMenuItem)
            {
                return;
            }
            
            if (HasHaptics)
            {
                VibrationService.SelectionChanged();    
            }
            
            SelectedItem = GetItemFromDisplayProperty(item.Title!);
        }
    }
}