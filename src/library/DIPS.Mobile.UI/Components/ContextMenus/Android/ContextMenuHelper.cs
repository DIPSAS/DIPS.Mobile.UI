using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using AndroidX.Core.View;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.ContextMenus.Android;

internal static class ContextMenuHelper
{
    internal static Dictionary<IContextMenuItem, IMenuItem> CreateMenuItems(IEnumerable<IContextMenuItem> contextMenuItems,
        ContextMenu contextMenu, PopupMenu popupMenu, int groupIndex = 0)
    {
        var dict = new Dictionary<IContextMenuItem, IMenuItem>();
        var items = contextMenuItems.ToList();
        var hasAtleastOneItemAlongsideGroup = 
            items.Count > 1 
            && items.Any(i => i is ContextMenuItem) 
            && items.Count(i => i is ContextMenuGroup) == 1; //If at least one item alongside menu group
        
        foreach (var contextMenuItem in items)
        {
            if (!contextMenuItem.IsVisible)
                continue;
            
            var index = items.IndexOf(contextMenuItem);
            if (contextMenuItem is ContextMenuGroup contextMenuGroup)
            {
                contextMenuGroup.Parent = contextMenu;
                groupIndex += 1;

                if (items.Count(i => i is ContextMenuGroup) >
                    1 || hasAtleastOneItemAlongsideGroup) //If there is more than one group or one group alongside items, add the group title and group the items
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
                    
                    if (contextMenuGroup.Icon is not null && !contextMenuGroup.IsCheckable) //Android does not seem to handle checkable group + icon on group
                    {
                        UpdateMenuGroup(contextMenuGroup, groupMenu);
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
                        (contextMenuItemInGroup as ContextMenuItem)!.Parent  = contextMenuGroup;
                        dict.Add(contextMenuItemInGroup, pair.Value);
                    }
                  
                    if (contextMenuGroup.IsCheckable)
                    {
                        popupMenu.Menu.SetGroupCheckable(groupIndex, contextMenuGroup.IsCheckable, false);
                    }
                }
            }
            else if (contextMenuItem is ContextMenuSeparatorItem)
            {
                popupMenu.Menu!.SetGroupDividerEnabled(true);
                groupIndex++;
            }
            else
            {
                var menuItem = popupMenu.Menu.Add(groupIndex, index, Menu.None, (contextMenuItem as ContextMenuItem)!.Title);
                UpdateMenuItem(contextMenu, groupIndex, contextMenuItem as ContextMenuItem, menuItem);

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

        if (contextMenuItem.IsDestructive)
        {
            var s = new SpannableString(contextMenuItem.Title);
            s.SetSpan(new ForegroundColorSpan(Colors.GetColor(ColorName.color_text_danger).ToPlatform()), 0, s.Length(), 0);
            menuItem.SetTitle(s);
            menuItem.SetIconTintList(Colors.GetColor(ColorName.color_text_danger).ToDefaultColorStateList());
        }

        
        TrySetChecked(contextMenu, menuItem, contextMenuItem);
        if (groupIndex == 0) //Not in a group
        {
            contextMenuItem.Parent = contextMenu;
        }
    }

    private static void UpdateMenuGroup(ContextMenuItem contextMenuItem, ISubMenu subMenu)
    {
        var id = (string.IsNullOrEmpty(contextMenuItem.AndroidOptions.IconResourceName))
            ? DUI.GetResourceId(GetIconName(contextMenuItem),"drawable")
            : DUI.GetResourceId(contextMenuItem.AndroidOptions.IconResourceName,"drawable");
        
        if (id != null) //Icon not set by consumer or icon not found
        {
            var icon = Platform.AppContext.GetDrawable((int)id);
            subMenu.SetIcon(icon);
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