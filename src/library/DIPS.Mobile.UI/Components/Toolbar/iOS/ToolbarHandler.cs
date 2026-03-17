using CoreGraphics;
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
    private NSLayoutConstraint? m_widthConstraint;

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

        if (m_widthConstraint is not null)
        {
            m_widthConstraint.Active = false;
            m_widthConstraint = null;
        }
    }

    private static partial void MapGroups(ToolbarHandler handler, Toolbar toolbar)
    {
        handler.UpdateItems();
    }

    private static partial void MapHorizontalAlignment(ToolbarHandler handler, Toolbar toolbar)
    {
        // Alignment is handled by the ContentPage attachment, not the handler itself
    }

    private void UpdateItems()
    {
        if (PlatformView is null || VirtualView is null)
            return;

        var barItems = new List<UIBarButtonItem>();

        for (var g = 0; g < VirtualView.Groups.Count; g++)
        {
            var group = VirtualView.Groups[g];

            foreach (var button in group.Items)
            {
                barItems.Add(CreateBarButtonItem(button));
            }

            // Add separator between groups (not after the last group)
            if (g < VirtualView.Groups.Count - 1)
            {
                var separator = new UIBarButtonItem(UIBarButtonSystemItem.FixedSpace);
                separator.Width = 16;
                barItems.Add(separator);
            }
        }

        PlatformView.SetItems(barItems.ToArray(), false);

        // Update compact width constraint
        UpdateWidthConstraint();
    }

    private void UpdateWidthConstraint()
    {
        PlatformView.LayoutIfNeeded();
        var fittedSize = PlatformView.SizeThatFits(new CGSize(nfloat.MaxValue, nfloat.MaxValue));

        // Add horizontal padding so items aren't flush against the edges
        var targetWidth = fittedSize.Width + 16;

        if (m_widthConstraint is null)
        {
            m_widthConstraint = PlatformView.WidthAnchor.ConstraintEqualTo(targetWidth);
            m_widthConstraint.Active = true;
        }
        else
        {
            m_widthConstraint.Constant = targetWidth;
        }
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
