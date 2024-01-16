using DIPS.Mobile.UI.Components.ContextMenus;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.ContextMenus.iOS;

 internal static class ContextMenuHelper
 {
    internal static Dictionary<IContextMenuItem, UIMenuElement> CreateMenuItems(
        IEnumerable<IContextMenuItem> contextMenuItems,
        ContextMenu contextMenu, Action callBackWhenItemTapped = null, ContextMenuGroup menuGroup = null)
    {
        var dict = new Dictionary<IContextMenuItem, UIMenuElement>();
        var items = contextMenuItems.ToArray();
        var hasAtleastOneItemAlongsideGroup = 
            items.Length > 1 
            && items.Any(i => i is ContextMenuItem) 
            && items.Count(i => i is ContextMenuGroup) == 1; //If at least one item alongside menu group
        
        foreach (var contextMenuItem in items)
        {
            if (!contextMenuItem.IsVisible)
                continue;
            
            UIMenuElement uiMenuElement = null!;
            if (contextMenuItem is ContextMenuGroup contextMenuGroup) //Recursively add menu items from a group
            {
                contextMenuGroup.Parent = contextMenu;
                
                foreach (var c in contextMenuGroup.ItemsSource!)
                {
                    if (!c.IsCheckable && contextMenuGroup.IsCheckable)//Inherit isCheckable from the parent context menu group if the item is not checkable
                    {
                        c.IsCheckable = true;
                    }
                }

                var newDict = CreateMenuItems(contextMenuGroup.ItemsSource, contextMenu, callBackWhenItemTapped, contextMenuGroup);
                if (items.Count(i => i is ContextMenuGroup) >
                    1 || hasAtleastOneItemAlongsideGroup) //If there is more than one group or one group alongside items, add the group title and group the items
                {
                    if (contextMenuGroup.Icon is not null && !contextMenuGroup.IsCheckable) //Check IsCheckable as Android does not seem to handle checkable group + icon on group
                    {
                        var group = (contextMenuItem as ContextMenuGroup)!;
                    
                        UIImage? image = null;
                        if (group.Icon is FileImageSource fileImageSource)
                        {
                            image = UIImage.FromBundle(fileImageSource);
                        }
                    
                        uiMenuElement = UIMenu.Create(contextMenuGroup.Title!, image, UIMenuIdentifier.None, UIMenuOptions.SingleSelection,newDict.Select(k => k.Value).ToArray());
                    }
                    else
                    {
                        uiMenuElement = UIMenu.Create(contextMenuGroup.Title!, newDict.Select(k => k.Value).ToArray());
                    }
                }
                else //Only one group, add this to the root of the menu so the user does not have to tap an extra time to get to the items.
                {
                    foreach (var newD in newDict)
                    {
                        dict.Add(newD.Key, newD.Value);
                    }
                    continue;
                }
            }
            else if (contextMenuItem is ContextMenuSeparatorItem)
            {
                uiMenuElement = UIMenu.Create("", null, UIMenuIdentifier.None, UIMenuOptions.DisplayInline,
                    dict.Select(element => element.Value).ToArray());

                dict.Clear();
            }
            else
            {
                var menuItem = (contextMenuItem as ContextMenuItem)!;
                
                UIImage? image = null;
                if (menuItem.Icon is FileImageSource fileImageSource)
                {
                    image = UIImage.FromBundle(fileImageSource);
                }
                
                if(!string.IsNullOrEmpty(menuItem.iOSOptions.SystemIconName)) //Override image with SF Symbols if this is what the consumer wants
                {

                    var systemImage = UIImage.GetSystemImage(menuItem.iOSOptions.SystemIconName);
                    image = systemImage ?? image;
                }
                
                if (menuItem.IsDestructive)
                {
                    image = image?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate)
                        .ApplyTintColor(UIColor.SystemRed);
                }
                
                var uiAction = UIAction.Create(menuItem.Title!, image, null,
                    uiAction => OnMenuItemClick(uiAction, menuItem, contextMenu, callBackWhenItemTapped));

                if (menuItem.IsDestructive)
                {
                    uiAction.Attributes = UIMenuElementAttributes.Destructive;
                }

                if (menuItem.IsChecked)
                {
                    contextMenu.ResetIsCheckedForTheRest(menuItem);
                }
                
                SetCorrectUiActionState(menuItem, uiAction); //Setting the correct check mark if it can

                uiMenuElement = uiAction;

                if (menuGroup != null)
                {
                    menuItem.Parent = menuGroup;
                }
                else
                {
                    menuItem.Parent = contextMenu;
                }
            }

            dict.Add(contextMenuItem, uiMenuElement);
        }

        return dict;
    }

    private static void OnMenuItemClick(UIAction action, ContextMenuItem tappedContextMenuItem,
        ContextMenu contextMenu, Action? callBackWhenItemTapped)
    {
        
        if (tappedContextMenuItem.IsCheckable)
        {
            var singleCheckMode = tappedContextMenuItem.Parent is ContextMenuGroup {IsCheckable: true};
            
            switch (singleCheckMode)
            {
                //You are unchecking an checked item that when single check mode is active, do not uncheck
                case true when tappedContextMenuItem.IsChecked:
                    return;
                //You are checking an item that is not checked, reset the others
                case true:
                    contextMenu.ResetIsCheckedForTheRest(tappedContextMenuItem);
                    break;
            }
            
            tappedContextMenuItem.IsChecked =
                !tappedContextMenuItem
                    .IsChecked; //Can not change the visuals when the menu is showing as the items are immutable when they are showing
        }

        tappedContextMenuItem.SendClicked(contextMenu);
        callBackWhenItemTapped?.Invoke();
    }

    private static void SetCorrectUiActionState(ContextMenuItem contextMenuItem, UIAction uiAction)
    {
        if (contextMenuItem.IsCheckable)
        {
            uiAction.State = contextMenuItem.IsChecked ? UIMenuElementState.On : UIMenuElementState.Off;
        }
    }
}