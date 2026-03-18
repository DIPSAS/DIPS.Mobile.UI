using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.ContextMenus.Android;
using DIPS.Mobile.UI.Resources.Colors;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using ImageButton = Android.Widget.ImageButton;
using Orientation = Android.Widget.Orientation;

namespace DIPS.Mobile.UI.Components.Toolbar;

/// <summary>
/// Android implementation of the Toolbar handler using a Material 3 Floating Toolbar pattern.
/// Creates a pill-shaped container with icon/text buttons and group separators.
/// </summary>
public partial class ToolbarHandler : ViewHandler<Toolbar, FrameLayout>
{
    private LinearLayout? m_pillLayout;

    protected override FrameLayout CreatePlatformView()
    {
        var container = new FrameLayout(Context);
        // Allow the elevation shadow to render outside the container bounds
        container.SetClipChildren(false);
        container.SetClipToPadding(false);
        var shadowPad = DpToPx(8);
        container.SetPadding(shadowPad, shadowPad, shadowPad, shadowPad);

        m_pillLayout = new LinearLayout(Context)
        {
            Orientation = Orientation.Horizontal,
        };
        m_pillLayout.SetGravity(GravityFlags.CenterVertical);

        // M3 Floating Toolbar: pill-shaped container with elevation
        var background = new GradientDrawable();
        background.SetCornerRadius(DpToPx(32)); // Fully rounded (64dp height / 2)
        background.SetColor(Colors.GetColor(ColorName.color_surface_default).ToPlatform());
        m_pillLayout.Background = background;
        m_pillLayout.Elevation = DpToPx(6);
        m_pillLayout.OutlineProvider = new RoundedOutlineProvider(DpToPx(32));
        m_pillLayout.ClipToOutline = false;

        // M3 spec: equal padding inside the floating toolbar
        var padding = DpToPx(8);
        m_pillLayout.SetPadding(padding, padding, padding, padding);

        // Default alignment: center
        var layoutParams = new FrameLayout.LayoutParams(
            ViewGroup.LayoutParams.WrapContent,
            DpToPx(64), // M3 default toolbar height
            GravityFlags.CenterHorizontal);

        container.AddView(m_pillLayout, layoutParams);

        Console.WriteLine($"[Toolbar] CreatePlatformView: container={container.GetType().Name}, pill={m_pillLayout.GetType().Name}");

        return container;
    }

    protected override void ConnectHandler(FrameLayout platformView)
    {
        base.ConnectHandler(platformView);
        UpdateItems();
    }

    protected override void DisconnectHandler(FrameLayout platformView)
    {
        m_pillLayout?.RemoveAllViews();
        base.DisconnectHandler(platformView);
    }

    private static partial void MapGroups(ToolbarHandler handler, Toolbar toolbar)
    {
        handler.UpdateItems();
    }

    private static partial void MapHorizontalAlignment(ToolbarHandler handler, Toolbar toolbar)
    {
        handler.UpdateAlignment();
    }

    private void UpdateAlignment()
    {
        if (m_pillLayout?.LayoutParameters is not FrameLayout.LayoutParams lp || VirtualView is null)
            return;

        lp.Gravity = VirtualView.HorizontalAlignment switch
        {
            ToolbarHorizontalAlignment.Start => GravityFlags.Start,
            ToolbarHorizontalAlignment.End => GravityFlags.End,
            _ => GravityFlags.CenterHorizontal,
        };

        m_pillLayout.LayoutParameters = lp;
    }

    private void UpdateItems()
    {
        if (m_pillLayout is null || VirtualView is null)
        {
            Console.WriteLine($"[Toolbar] UpdateItems: early exit — pill={m_pillLayout}, VirtualView={VirtualView}");
            return;
        }

        Console.WriteLine($"[Toolbar] UpdateItems: Groups.Count={VirtualView.Groups.Count}");
        m_pillLayout.RemoveAllViews();

        for (var g = 0; g < VirtualView.Groups.Count; g++)
        {
            var group = VirtualView.Groups[g];

            foreach (var button in group.Items)
            {
                m_pillLayout.AddView(CreateButtonView(button));
            }

            // Add separator between groups (not after the last group)
            if (g < VirtualView.Groups.Count - 1)
            {
                m_pillLayout.AddView(CreateGroupSeparator());
            }
        }

        UpdateAlignment();
        Console.WriteLine($"[Toolbar] UpdateItems done: pill.ChildCount={m_pillLayout.ChildCount}, pill.Visibility={m_pillLayout.Visibility}");
    }

    private AView CreateButtonView(ToolbarButton toolbarButton)
    {
        var hasMenu = toolbarButton.Menu is { ItemsSource.Count: > 0 };
        var hasIcon = toolbarButton.Icon is not null;

        // Icon buttons take priority; text-only buttons are used when there's no icon
        if (hasIcon)
        {
            return CreateIconButton(toolbarButton, hasMenu);
        }

        return CreateTextButton(toolbarButton, hasMenu);
    }

    private AView CreateIconButton(ToolbarButton toolbarButton, bool hasMenu)
    {
        var size = DpToPx(48);
        var button = new ImageButton(Context);
        button.LayoutParameters = new LinearLayout.LayoutParams(size, size)
        {
            Gravity = GravityFlags.CenterVertical,
        };
        button.SetScaleType(ImageView.ScaleType.CenterInside);

        // Circular ripple background
        button.Background = CreateRippleDrawable();

        // Set icon
        if (DUI.TryGetDrawableFromFileImageSource(toolbarButton.Icon, out var drawable) && drawable is not null)
        {
            drawable.SetTint(Colors.GetColor(ColorName.color_icon_action).ToPlatform());
            button.SetImageDrawable(drawable);
        }

        button.ContentDescription = toolbarButton.Title;
        button.Enabled = toolbarButton.IsEnabled;

        if (hasMenu)
        {
            button.Click += (_, _) => ShowContextMenu(button, toolbarButton);
        }
        else
        {
            button.Click += (_, _) => toolbarButton.Command?.Execute(toolbarButton.CommandParameter);
        }

        return button;
    }

    private AView CreateTextButton(ToolbarButton toolbarButton, bool hasMenu)
    {
        var button = new TextView(Context);
        button.Text = toolbarButton.Title;
        button.SetTextColor(Colors.GetColor(ColorName.color_text_action).ToPlatform());
        button.Gravity = GravityFlags.Center;

        var height = DpToPx(48);
        var padH = DpToPx(16);
        button.LayoutParameters = new LinearLayout.LayoutParams(
            ViewGroup.LayoutParams.WrapContent, height)
        {
            Gravity = GravityFlags.CenterVertical,
        };
        button.SetPadding(padH, 0, padH, 0);
        button.SetMinWidth(0);

        // Rounded ripple background
        button.Background = CreateRippleDrawable();
        button.Clickable = true;
        button.Focusable = true;

        button.Enabled = toolbarButton.IsEnabled;
        button.ContentDescription = toolbarButton.Title;

        if (hasMenu)
        {
            button.Click += (_, _) => ShowContextMenu(button, toolbarButton);
        }
        else
        {
            button.Click += (_, _) => toolbarButton.Command?.Execute(toolbarButton.CommandParameter);
        }

        return button;
    }

    private AView CreateGroupSeparator()
    {
        var separator = new AView(Context);
        var width = DpToPx(1);
        var height = DpToPx(24);
        var margin = DpToPx(4);

        var lp = new LinearLayout.LayoutParams(width, height)
        {
            Gravity = GravityFlags.CenterVertical,
        };
        lp.SetMargins(margin, 0, margin, 0);
        separator.LayoutParameters = lp;

        separator.SetBackgroundColor(Colors.GetColor(ColorName.color_border_default).ToPlatform());

        return separator;
    }

    private static void ShowContextMenu(AView anchor, ToolbarButton toolbarButton)
    {
        if (toolbarButton.Menu?.ItemsSource is null)
            return;

        var handler = new ContextMenuPlatformEffect.ContextMenuHandler(
            toolbarButton.Menu, anchor);
        handler.OpenContextMenu(anchor, EventArgs.Empty);
    }

    private RippleDrawable CreateRippleDrawable()
    {
        var rippleColor = ColorStateList.ValueOf(
            Colors.GetColor(ColorName.color_surface_default_selected).ToPlatform());
        var mask = new GradientDrawable();
        mask.SetShape(ShapeType.Oval);
        mask.SetColor(Android.Graphics.Color.White);
        return new RippleDrawable(rippleColor, null, mask);
    }

    private int DpToPx(float dp)
    {
        return (int)(dp * Context.Resources!.DisplayMetrics!.Density + 0.5f);
    }

    /// <summary>
    /// Provides a rounded outline for elevation shadow rendering.
    /// </summary>
    private class RoundedOutlineProvider(int radius) : Android.Views.ViewOutlineProvider
    {
        public override void GetOutline(Android.Views.View? view, Android.Graphics.Outline? outline)
        {
            if (view is null || outline is null)
                return;

            outline.SetRoundRect(0, 0, view.Width, view.Height, radius);
        }
    }
}
