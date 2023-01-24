using System;
using System.Collections.Generic;
using System.Linq;
using DIPS.Mobile.UI.Components.ContextMenu;
using DIPS.Mobile.UI.Extensions;

namespace DIPS.Mobile.UI.Components.Pickers
{
    public partial class Picker
    {
        private static void UpdateContextMenuItems(Picker picker)
        {
            if (picker.SelectedItem == null ||
                picker.m_contextMenuControl.ItemsSource.FirstOrDefault() is not ContextMenuGroup contextMenuGroup)
            {
                return;
            }

            var contextMenuItem = contextMenuGroup.ItemsSource?.FirstOrDefault(i =>
                i.Title != null && i.Title.Equals(picker.SelectedItem.GetPropertyValue(picker.ItemDisplayProperty),
                    StringComparison.InvariantCultureIgnoreCase));
            if (contextMenuItem != null)
            {
                contextMenuItem.IsChecked = true;
            }
        }

        private static void AddContextMenuItems(Picker picker)
        {
            if (picker.ItemsSource == null)
            {
                return;
            }

            var group = new ContextMenuGroup() {IsCheckable = true};
            foreach (var obj in picker.ItemsSource)
            {
                var itemDisplayName = obj.GetPropertyValue(picker.ItemDisplayProperty);
                group.ItemsSource?.Add(new ContextMenuItem()
                {
                    Title = itemDisplayName, IsChecked = picker.SelectedItem == obj
                });
            }

            picker.m_contextMenuControl.ItemsSource.Clear();
            picker.m_contextMenuControl.ItemsSource.Add(group);
        }

        private void SetSelectedItemBasedOnContextMenuItem(ContextMenuItem item)
        {
            SelectedItem = GetItemFromDisplayProperty(item.Title!);
        }
    }
}