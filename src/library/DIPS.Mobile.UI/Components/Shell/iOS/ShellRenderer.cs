using System.Reflection;
using DIPS.Mobile.UI.Components.ContextMenus.iOS;
using DIPS.Mobile.UI.Components.Pages;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Platform.Compatibility;
using UIKit;

namespace DIPS.Mobile.UI.Components.Shell;

public partial class ShellRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.ShellRenderer
{
    protected override IShellPageRendererTracker CreatePageRendererTracker()
    {
        return new CustomShellPageRendererTracker(this);
    }
}

internal class CustomShellPageRendererTracker : ShellPageRendererTracker
{
    public CustomShellPageRendererTracker(IShellContext context) : base(context)
    {
    }

    protected override void UpdateToolbarItems()
    {
        base.UpdateToolbarItems();
        
        if (ViewController.NavigationItem == null)
        {
            return;
        }

        if (ViewController.NavigationItem.RightBarButtonItems != null)
        {
            foreach (var t in ViewController.NavigationItem.RightBarButtonItems)
                t.Dispose();
        }

        List<UIBarButtonItem>? primaries = null;
        if (Page.ToolbarItems.Count > 0)
        {
            foreach (var item in Page.ToolbarItems.OrderBy(x => x.Priority))
            {
                if (item is not ContextMenuToolbarItem contextMenuToolbarItem)
                {
                    (primaries ??= []).Add(item.ToUIBarButtonItem(false, true));
                }
                else
                {
                    (primaries ??= []).Add(ToContextMenuBarButtonItem(contextMenuToolbarItem));
                }
            }

            if (primaries != null)
                primaries.Reverse();
        }

        ViewController.NavigationItem.SetRightBarButtonItems(primaries == null ? [] : primaries.ToArray(), false);
    }
    
    private static UIBarButtonItem ToContextMenuBarButtonItem(ContextMenuToolbarItem toolbarItem)
    {
        var dict = ContextMenuHelper.CreateMenuItems(toolbarItem.ContextMenu.ItemsSource!, toolbarItem.ContextMenu);
        if (toolbarItem.IconImageSource is FileImageSource fileImageSource)
        {
            return new UIBarButtonItem(UIImage.FromBundle(fileImageSource), UIMenu.Create(toolbarItem.ContextMenu.Title, dict.Select(k => k.Value).ToArray()));
        }

        return new UIBarButtonItem(toolbarItem.Text, UIMenu.Create(toolbarItem.ContextMenu.Title, dict.Select(k => k.Value).ToArray()));
    }
}