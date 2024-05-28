using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.ContextMenus.iOS;
using Microsoft.Maui.Handlers;
using UIKit;
using ContentView = Microsoft.Maui.Platform.ContentView;

namespace DIPS.Mobile.UI.Components.Pages;

public partial class ContentPageHandler : PageHandler
{
    protected override void ConnectHandler(ContentView platformView)
    {
        base.ConnectHandler(platformView);

        if (VirtualView is not Page { ToolbarItems: ObservableCollection<ToolbarItem> toolbarItems })
            return;

        // ToolbarItems are already initialized here, so we need to call directly, but still listen to changes
        toolbarItems.CollectionChanged += ToolbarItemsChanged;
        _ = FindContextMenuToolbarItemsAndSetContextMenusOnThem();
    }

    private void ToolbarItemsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        _ = FindContextMenuToolbarItemsAndSetContextMenusOnThem();
    }

    private async Task FindContextMenuToolbarItemsAndSetContextMenusOnThem()
    {
        if(this is not IPlatformViewHandler { ViewController: not null } platformViewHandler)
        {
            return;
        }

        if (VirtualView is not ContentPage page)
            return;

        // Wait one frame for RightBarButtonItems to be initialized
        await Task.Delay(1);

        var toolbarItemsToReplace = new UIBarButtonItem[page.ToolbarItems.Count];
        var length = platformViewHandler.ViewController.NavigationItem.RightBarButtonItems?.Length ?? 0;
        for (var i = 0; i < length; i++)
        {
            // For some reason RightBarButtonItems is reversed oppose to how they are in ToolbarItems
            var toolbarItem = page.ToolbarItems[length - 1 - i];

            UIBarButtonItem barButtonItem;
            
            if(toolbarItem is not ContextMenuToolbarItem contextMenuToolbarItem)
            {
                barButtonItem = platformViewHandler.ViewController.NavigationItem.RightBarButtonItems![i];
            }
            else
            {
                // Here we need to create a new UIBarButtonItem with the context menu, because setting the menu on the existing UIBarButtonItem does not work (it will default to long-press)
                barButtonItem = ToBarButtonItem(contextMenuToolbarItem, page);
            }
            
            toolbarItemsToReplace[i] = barButtonItem;
        }
       
        platformViewHandler.ViewController.NavigationItem.RightBarButtonItems = toolbarItemsToReplace;
    }

    private static UIBarButtonItem ToBarButtonItem(ContextMenuToolbarItem toolbarItem, ContentPage contentPage)
    {
        var dict = ContextMenuHelper.CreateMenuItems(toolbarItem.ContextMenu.ItemsSource!, toolbarItem.ContextMenu);
        toolbarItem.BindingContext = contentPage.BindingContext;
        if (toolbarItem.IconImageSource is FileImageSource fileImageSource)
        {
            return new UIBarButtonItem(UIImage.FromBundle(fileImageSource), UIMenu.Create(toolbarItem.ContextMenu.Title, dict.Select(k => k.Value).ToArray()));
        }

        return new UIBarButtonItem(toolbarItem.Text, UIMenu.Create(toolbarItem.ContextMenu.Title, dict.Select(k => k.Value).ToArray()));
    }

    protected override void DisconnectHandler(ContentView platformView)
    {
        base.DisconnectHandler(platformView);
        
        if (VirtualView is not Page { ToolbarItems: ObservableCollection<ToolbarItem> toolbarItems })
            return;

        toolbarItems.CollectionChanged -= ToolbarItemsChanged;
    }
}