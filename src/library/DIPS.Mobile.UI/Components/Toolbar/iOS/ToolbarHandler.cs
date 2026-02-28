using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Toolbar;

public partial class ToolbarHandler : ViewHandler<Toolbar, UIToolbar>
{
    protected override UIToolbar CreatePlatformView()
    {
        var toolbar = new UIToolbar();
        toolbar.BarTintColor = Resources.Colors.Colors.GetColor(ColorName.color_surface_default).ToPlatform();
        toolbar.Translucent = false;
        return toolbar;
    }

    protected override void ConnectHandler(UIToolbar platformView)
    {
        base.ConnectHandler(platformView);
        UpdateButtons();
    }

    private static partial void MapButtons(ToolbarHandler handler, Toolbar toolbar)
    {
        handler.UpdateButtons();
    }

    private void UpdateButtons()
    {
        var items = new List<UIBarButtonItem>();

        // Add a flexible space before the first button to help center/distribute them
        items.Add(new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace));

        foreach (var toolbarButton in VirtualView.Buttons)
        {
            items.Add(CreateBarButtonItem(toolbarButton));
            items.Add(new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace));
        }

        PlatformView.SetItems(items.ToArray(), false);
    }

    private static UIBarButtonItem CreateBarButtonItem(ToolbarButton toolbarButton)
    {
        UIImage? icon = null;
        if (DUI.TryGetUIImageFromImageSource(toolbarButton.Icon, out var uiImage))
        {
            icon = uiImage?.WithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
        }

        var item = new UIBarButtonItem(icon, UIBarButtonItemStyle.Plain, (_, _) =>
        {
            toolbarButton.Command?.Execute(toolbarButton.CommandParameter);
        });

        item.Enabled = toolbarButton.IsEnabled;
        item.TintColor = Resources.Colors.Colors.GetColor(ColorName.color_icon_action).ToPlatform();

        if (!string.IsNullOrEmpty(toolbarButton.Title))
        {
            item.AccessibilityLabel = toolbarButton.Title;
        }

        return item;
    }
}
