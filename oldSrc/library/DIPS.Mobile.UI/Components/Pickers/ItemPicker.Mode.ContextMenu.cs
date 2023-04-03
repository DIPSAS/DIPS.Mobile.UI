using System;
using System.Collections.Generic;
using System.Linq;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Pickers
{
    public partial class ItemPicker
    {
        private static void UpdateContextMenuItems(ItemPicker itemPicker)
        {
            if (itemPicker.m_contextMenuControl == null || itemPicker.SelectedItem == null ||
                itemPicker.m_contextMenuControl.ItemsSource.FirstOrDefault() is not ContextMenuGroup contextMenuGroup)
            {
                return;
            }

            var contextMenuItem = contextMenuGroup.ItemsSource?.FirstOrDefault(i =>
                i.Title != null && i.Title.Equals(itemPicker.SelectedItem.GetPropertyValue(itemPicker.ItemDisplayProperty),
                    StringComparison.InvariantCultureIgnoreCase));
            if (contextMenuItem != null)
            {
                contextMenuItem.IsChecked = true;
            }
        }

        private static void AddContextMenuItems(ItemPicker itemPicker)
        {
            if (itemPicker.ItemsSource == null || itemPicker.m_contextMenuControl == null)
            {
                return;
            }

            var group = new ContextMenuGroup() {IsCheckable = true};
            foreach (var obj in itemPicker.ItemsSource)
            {
                var itemDisplayName = obj.GetPropertyValue(itemPicker.ItemDisplayProperty);
                group.ItemsSource?.Add(new ContextMenuItem()
                {
                    Title = itemDisplayName, IsChecked = itemPicker.SelectedItem == obj
                });
            }

            itemPicker.m_contextMenuControl.ItemsSource.Clear();
            itemPicker.m_contextMenuControl.ItemsSource.Add(group);
        }

        private void SetSelectedItemBasedOnContextMenuItem(ContextMenuItem item)
        {
            SelectedItem = GetItemFromDisplayProperty(item.Title!);
        }
    }
}