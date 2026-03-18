using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.ContextMenus.iOS;
using DIPS.Mobile.UI.Resources.Colors;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Toolbar;

public partial class ToolbarHandler : ViewHandler<Toolbar, UIToolbar>
{
    protected override UIToolbar CreatePlatformView()
    {
        var toolbar = new UIToolbar();
        toolbar.TranslatesAutoresizingMaskIntoConstraints = false;

        return toolbar;
    }

    protected override void ConnectHandler(UIToolbar platformView)
    {
        base.ConnectHandler(platformView);
        UpdateItems();
    }

    protected override void DisconnectHandler(UIToolbar platformView)
    {
        base.DisconnectHandler(platformView);
    }

    private static partial void MapGroups(ToolbarHandler handler, Toolbar toolbar)
    {
        handler.UpdateItems();
    }

    private static partial void MapHorizontalAlignment(ToolbarHandler handler, Toolbar toolbar)
    {
        // Alignment changes require rebuilding items (FlexibleSpace placement)
        handler.UpdateItems();
    }

    private void UpdateItems()
    {
        if (PlatformView is null || VirtualView is null)
            return;

        var contentItems = new List<UIBarButtonItem>();

        for (var g = 0; g < VirtualView.Groups.Count; g++)
        {
            var group = VirtualView.Groups[g];

            foreach (var button in group.Items)
            {
                contentItems.Add(CreateBarButtonItem(button));
            }

            // Add separator between groups (not after the last group)
            if (g < VirtualView.Groups.Count - 1)
            {
                var separator = new UIBarButtonItem(UIBarButtonSystemItem.FixedSpace);
                separator.Width = 16;
                contentItems.Add(separator);
            }
        }

        // Use FlexibleSpace to control alignment within the full-width toolbar.
        // The glass capsule only wraps the actual items, not the flex spaces.
        var flexSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
        var barItems = new List<UIBarButtonItem>();

        switch (VirtualView.HorizontalAlignment)
        {
            case ToolbarHorizontalAlignment.Start:
                barItems.AddRange(contentItems);
                barItems.Add(flexSpace);
                break;
            case ToolbarHorizontalAlignment.End:
                barItems.Add(flexSpace);
                barItems.AddRange(contentItems);
                break;
            default: // Center
                barItems.Add(flexSpace);
                barItems.AddRange(contentItems);
                barItems.Add(new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace));
                break;
        }

        PlatformView.SetItems(barItems.ToArray(), true);
    }

    private static UIBarButtonItem CreateBarButtonItem(ToolbarButton toolbarButton)
    {
        UIImage? icon = null;
        if (DUI.TryGetUIImageFromImageSource(toolbarButton.Icon, out var uiImage) && uiImage is not null)
        {
            icon = uiImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
        }

        UIBarButtonItem item;

        var hasMenu = toolbarButton.Menu is { ItemsSource.Count: > 0 };
        var hasIcon = icon is not null;
        var hasTitle = !string.IsNullOrEmpty(toolbarButton.Title);

        if (hasMenu)
        {
            var menuItems = ContextMenuHelper.CreateMenuItems(toolbarButton.Menu!.ItemsSource!, toolbarButton.Menu);
            var uiMenu = UIMenu.Create(menuItems.Select(kvp => kvp.Value).ToArray());

            if (hasIcon)
            {
                item = new UIBarButtonItem(icon, uiMenu);
            }
            else
            {
                item = new UIBarButtonItem(toolbarButton.Title ?? "", UIBarButtonItemStyle.Plain, null);
                item.Menu = uiMenu;
            }
        }
        else if (hasIcon)
        {
            item = new UIBarButtonItem(icon, UIBarButtonItemStyle.Plain, (_, _) =>
            {
                toolbarButton.Command?.Execute(toolbarButton.CommandParameter);
            });
        }
        else
        {
            // Text-only button
            item = new UIBarButtonItem(toolbarButton.Title ?? "", UIBarButtonItemStyle.Plain, (_, _) =>
            {
                toolbarButton.Command?.Execute(toolbarButton.CommandParameter);
            });
        }

        item.Enabled = toolbarButton.IsEnabled;
        item.TintColor = Colors.GetColor(ColorName.color_icon_action).ToPlatform();

        if (hasTitle)
        {
            item.AccessibilityLabel = toolbarButton.Title;
        }

        return item;
    }
}
