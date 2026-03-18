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
    /// <summary>
    /// Maintains a mapping from each ToolbarButton to its native UIBarButtonItem
    /// so we can incrementally add/remove items without rebuilding everything.
    /// </summary>
    private readonly Dictionary<ToolbarButton, UIBarButtonItem> m_buttonItemMap = new();

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
        SubscribeToItemPropertyChanges();
    }

    protected override void DisconnectHandler(UIToolbar platformView)
    {
        UnsubscribeFromItemPropertyChanges();
        m_buttonItemMap.Clear();
        base.DisconnectHandler(platformView);
    }

    private static partial void MapGroups(ToolbarHandler handler, Toolbar toolbar)
    {
        handler.UnsubscribeFromItemPropertyChanges();
        handler.UpdateItems();
        handler.SubscribeToItemPropertyChanges();
    }

    private static partial void MapHorizontalAlignment(ToolbarHandler handler, Toolbar toolbar)
    {
        // Alignment changes require rebuilding items (FlexibleSpace placement)
        handler.ApplyItemsToToolbar(animated: false);
    }

    /// <summary>
    /// Called when a single button's IsVisible changes. Instead of rebuilding all native items,
    /// we just re-apply the current items list with animation — smooth insert/remove.
    /// </summary>
    partial void OnToolbarButtonVisibilityChanged(ToolbarButton toolbarButton)
    {
        // If the button just became visible, create its native item
        if (toolbarButton.IsVisible && !m_buttonItemMap.ContainsKey(toolbarButton))
        {
            m_buttonItemMap[toolbarButton] = CreateBarButtonItem(toolbarButton);
        }

        ApplyItemsToToolbar(animated: true);
    }

    private void UpdateItems()
    {
        if (PlatformView is null || VirtualView is null)
            return;

        m_buttonItemMap.Clear();

        // Create native items for all buttons (including hidden ones — we keep them ready)
        foreach (var group in VirtualView.Groups)
        {
            foreach (var button in group.Items)
            {
                m_buttonItemMap[button] = CreateBarButtonItem(button);
            }
        }

        ApplyItemsToToolbar(animated: false);
    }

    /// <summary>
    /// Builds the toolbar items array from the current groups, filtering by IsVisible,
    /// and sets them on the UIToolbar. Uses animated: true for smooth transitions.
    /// </summary>
    private void ApplyItemsToToolbar(bool animated)
    {
        if (PlatformView is null || VirtualView is null)
            return;

        var contentItems = new List<UIBarButtonItem>();

        for (var g = 0; g < VirtualView.Groups.Count; g++)
        {
            var group = VirtualView.Groups[g];
            var visibleButtons = group.Items.Where(b => b.IsVisible).ToList();

            foreach (var button in visibleButtons)
            {
                if (m_buttonItemMap.TryGetValue(button, out var barItem))
                {
                    contentItems.Add(barItem);
                }
            }

            // Add separator between groups only if both have visible items
            if (g < VirtualView.Groups.Count - 1 && visibleButtons.Count > 0)
            {
                var nextGroup = VirtualView.Groups[g + 1];
                if (nextGroup.Items.Any(b => b.IsVisible))
                {
                    var separator = new UIBarButtonItem(UIBarButtonSystemItem.FixedSpace);
                    separator.Width = 16;
                    contentItems.Add(separator);
                }
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
