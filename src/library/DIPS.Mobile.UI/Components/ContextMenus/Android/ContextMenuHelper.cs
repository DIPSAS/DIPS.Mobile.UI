using Android.Views;
using Android.Widget;
using AndroidX.Core.View;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.ContextMenus.Android;

internal static class ContextMenuHelper
{
    internal static Dictionary<ContextMenuItem, IMenuItem> CreateMenuItems(IEnumerable<ContextMenuItem> contextMenuItems,
        ContextMenu contextMenu, PopupMenu popupMenu, int groupIndex = 0)
    {
        var dict = new Dictionary<ContextMenuItem, IMenuItem>();
        var items = contextMenuItems.ToList();

        foreach (var contextMenuItem in items)
        {
            var index = items.IndexOf(contextMenuItem);
            if (contextMenuItem is ContextMenuGroup contextMenuGroup)
            {
                contextMenuGroup.Parent = contextMenu;
                groupIndex += 1;

                if (items.Count(i => i is ContextMenuGroup) >
                    1) //If there is more than one group, add the group title and group the items
                {
                    var groupMenu = popupMenu.Menu.AddSubMenu(groupIndex, index, Menu.None,
                        contextMenuGroup.Title);

                    if (groupMenu == null) continue;
                    if (contextMenuGroup.ItemsSource == null) continue;

                    foreach (var contextMenuItemInGroup in contextMenuGroup.ItemsSource)
                    {
                        var contextMenuItemInGroupIndex =
                            contextMenuGroup.ItemsSource.IndexOf(contextMenuItemInGroup);
                        var menuItem = groupMenu.Add(groupIndex, contextMenuItemInGroupIndex, Menu.None,
                            contextMenuItemInGroup.Title);
                        UpdateMenuItem(contextMenu, groupIndex, contextMenuItemInGroup, menuItem);
                        dict.Add(contextMenuItemInGroup, menuItem);
                    }


                    if (contextMenuGroup.IsCheckable)
                    {
                        groupMenu.SetGroupCheckable(groupIndex, contextMenuGroup.IsCheckable, false);
                    }
                }
                else //Only one group, add this to the root of the menu so the user does not have to tap an extra time to get to the items.
                {
                    var newDict = CreateMenuItems(contextMenuGroup.ItemsSource, contextMenu,
                        popupMenu,
                        groupIndex);
                    foreach (var pair in newDict)
                    {
                        var contextMenuItemInGroup = pair.Key;
                        contextMenuItemInGroup.Parent = contextMenuGroup;
                        dict.Add(contextMenuItemInGroup, pair.Value);
                    }
                  
                    if (contextMenuGroup.IsCheckable)
                    {
                        popupMenu.Menu.SetGroupCheckable(groupIndex, contextMenuGroup.IsCheckable, false);
                    }
                }
            }
            else
            {
                var menuItem = popupMenu.Menu.Add(groupIndex, index, Menu.None, contextMenuItem.Title);
                UpdateMenuItem(contextMenu, groupIndex, contextMenuItem, menuItem);

                dict.Add(contextMenuItem, menuItem);
            }
        }

        MenuCompat.SetGroupDividerEnabled(popupMenu.Menu, true);
        return dict;
    }

    private static void UpdateMenuItem(ContextMenu contextMenu, int groupIndex,
        ContextMenuItem contextMenuItem, IMenuItem menuItem)
    {
        
            var id = (string.IsNullOrEmpty(contextMenuItem.AndroidOptions.IconResourceName))
                ? DUI.GetResourceId(GetIconName(contextMenuItem),"drawable")
                : DUI.GetResourceId(contextMenuItem.AndroidOptions.IconResourceName,"drawable");
        
            if (id != null) //Icon not set by consumer or icon not found
            {
                var icon = Platform.AppContext.GetDrawable((int)id);
                menuItem.SetIcon(icon);
                
            }

        TrySetChecked(contextMenu, menuItem, contextMenuItem);
        if (groupIndex == 0) //Not in a group
        {
            contextMenuItem.Parent = contextMenu;
        }
    }

    private static string GetIconName(ContextMenuItem contextMenuItem)
    {
        if (contextMenuItem.Icon == null) return string.Empty;

        if (contextMenuItem.Icon is not FileImageSource fileImageSource)
        {
            return string.Empty;
        }

        var pngEnding = ".png";
        return fileImageSource.File.EndsWith(pngEnding) ? fileImageSource.File.Replace(pngEnding, "") : string.Empty;
    }

    private static void TrySetChecked(ContextMenu contextMenu, IMenuItem menuItem,
        ContextMenuItem contextMenuItem)
    {
        menuItem?.SetCheckable(contextMenuItem.IsCheckable);
        if (contextMenuItem.IsChecked) //If an item is checked, reset the rest in the group
        {
            contextMenu.ResetIsCheckedForTheRest(contextMenuItem);
        }

        menuItem?.SetChecked(contextMenuItem.IsChecked);
    }
}