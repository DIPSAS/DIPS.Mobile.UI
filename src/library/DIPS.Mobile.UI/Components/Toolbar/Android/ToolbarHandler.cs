using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Transitions;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.ContextMenus.Android;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.Icons;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Icons = DIPS.Mobile.UI.Resources.Icons.Icons;
using ImageButton = Android.Widget.ImageButton;
using Orientation = Android.Widget.Orientation;
using ProgressBar = Android.Widget.ProgressBar;

namespace DIPS.Mobile.UI.Components.Toolbar;

/// <summary>
/// Android implementation of the Toolbar handler using a Material 3 Floating Toolbar pattern.
/// Creates a pill-shaped container with icon/text buttons and group separators.
/// </summary>
public partial class ToolbarHandler : ViewHandler<Toolbar, FrameLayout>
{
    // ── Layout constants (dp) ──────────────────────────────────────────
    private const float PillCornerRadiusDp = 32f;
    private const float PillHeightDp = 64f;
    private const float PillElevationDp = 6f;
    private const float PillPaddingHorizontalDp = 16f;
    private const float PillPaddingVerticalDp = 8f;
    private const float ShadowPaddingDp = 8f;

    private const float ButtonSizeDp = 48f;       // M3 standard touch target
    private const float ButtonMarginDp = 4f;
    private const float TextButtonPaddingHorizontalDp = 16f;

    private const float SpinnerInsetDp = 12f;      // (48 - 24) / 2 → centres 24dp icon in 48dp target

    private const float SeparatorWidthDp = 1f;
    private const float SeparatorHeightDp = 24f;
    private const float SeparatorMarginDp = 12f;

    private const float AnimationSlideExtraDp = 32f;
    // ────────────────────────────────────────────────────────────────────

    private LinearLayout? m_pillLayout;

    /// <summary>
    /// Maps each ToolbarButton to its native Android view for incremental add/remove.
    /// </summary>
    private readonly Dictionary<ToolbarButton, AView> m_buttonViewMap = new();

    /// <summary>
    /// Maps group index to the separator view between group[index] and group[index+1].
    /// </summary>
    private readonly Dictionary<int, AView> m_separatorMap = new();

    protected override FrameLayout CreatePlatformView()
    {
        var container = new FrameLayout(Context);
        // Allow the elevation shadow to render outside the container bounds
        container.SetClipChildren(false);
        container.SetClipToPadding(false);
        var shadowPad = DpToPx(ShadowPaddingDp);
        container.SetPadding(shadowPad, shadowPad, shadowPad, shadowPad);

        m_pillLayout = new LinearLayout(Context)
        {
            Orientation = Orientation.Horizontal,
        };
        m_pillLayout.SetGravity(GravityFlags.CenterVertical);

        // M3 Floating Toolbar: pill-shaped container with elevation
        var background = new GradientDrawable();
        background.SetCornerRadius(DpToPx(PillCornerRadiusDp));
        background.SetColor(Colors.GetColor(ColorName.color_surface_default).ToPlatform());
        m_pillLayout.Background = background;
        m_pillLayout.Elevation = DpToPx(PillElevationDp);
        m_pillLayout.OutlineProvider = new RoundedOutlineProvider(DpToPx(PillCornerRadiusDp));
        m_pillLayout.ClipToOutline = false;

        var padV = DpToPx(PillPaddingVerticalDp);
        var padH = DpToPx(PillPaddingHorizontalDp);
        m_pillLayout.SetPadding(padH, padV, padH, padV);

        // Default alignment: center
        var layoutParams = new FrameLayout.LayoutParams(
            ViewGroup.LayoutParams.WrapContent,
            DpToPx(PillHeightDp),
            GravityFlags.CenterHorizontal);

        container.AddView(m_pillLayout, layoutParams);

        return container;
    }

    protected override void ConnectHandler(FrameLayout platformView)
    {
        base.ConnectHandler(platformView);
        UpdateItems();
        SubscribeToItemPropertyChanges();
    }

    protected override void DisconnectHandler(FrameLayout platformView)
    {
        UnsubscribeFromItemPropertyChanges();
        ClearButtonViews();
        m_buttonViewMap.Clear();
        m_separatorMap.Clear();
        m_pillLayout = null;
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

    /// <summary>
    /// Called when a single button's IsVisible changes. Toggles the native view's Visibility
    /// and updates separators, with a smooth transition animation.
    /// </summary>
    partial void OnToolbarButtonVisibilityChanged(ToolbarButton toolbarButton)
    {
        if (m_pillLayout is null || VirtualView is null)
            return;

        UpdateViewVisibility(animated: true);
    }

    partial void OnToolbarTaskButtonStateChanged(ToolbarTaskButton toolbarTaskButton)
    {
        if (m_pillLayout is null || VirtualView is null)
            return;

        if (!m_buttonViewMap.TryGetValue(toolbarTaskButton, out var currentView))
            return;

        // Find the position of the current view in the pill layout
        var index = m_pillLayout.IndexOfChild(currentView);
        if (index < 0)
            return;

        // Animate the swap
        var transition = new AutoTransition();
        transition.SetDuration(200);
        TransitionManager.BeginDelayedTransition((ViewGroup)m_pillLayout.Parent!, transition);

        // Remove old view
        m_pillLayout.RemoveViewAt(index);
        currentView.Dispose();

        // Create the replacement based on state priority: error > busy > finished > normal
        AView newView;
        if (toolbarTaskButton.Error is { HasError: true })
        {
            newView = CreateErrorView(toolbarTaskButton);
        }
        else if (toolbarTaskButton.IsBusy)
        {
            newView = CreateSpinnerView();
        }
        else if (toolbarTaskButton.IsFinished)
        {
            newView = CreateFinishedView();
        }
        else
        {
            newView = CreateButtonView(toolbarTaskButton);
        }

        m_buttonViewMap[toolbarTaskButton] = newView;
        m_pillLayout.AddView(newView, index);

        // Respect current visibility
        newView.Visibility = toolbarTaskButton.IsVisible ? ViewStates.Visible : ViewStates.Gone;
    }

    private AView CreateSpinnerView()
    {
        var size = DpToPx(ButtonSizeDp);
        var margin = DpToPx(ButtonMarginDp);
        var inset = DpToPx(SpinnerInsetDp);
        var spinner = new ProgressBar(Context, null, Android.Resource.Attribute.ProgressBarStyleSmall);
        spinner.Indeterminate = true;
        spinner.IndeterminateTintList = ColorStateList.ValueOf(
            Colors.GetColor(ColorName.color_icon_subtle).ToPlatform());
        spinner.SetPadding(inset, inset, inset, inset);
        spinner.LayoutParameters = new LinearLayout.LayoutParams(size, size)
        {
            Gravity = GravityFlags.CenterVertical,
        };
        (spinner.LayoutParameters as LinearLayout.LayoutParams)!.SetMargins(margin, 0, margin, 0);

        return spinner;
    }

    private AView CreateFinishedView()
    {
        var size = DpToPx(ButtonSizeDp);
        var margin = DpToPx(ButtonMarginDp);
        var button = new ImageButton(Context);
        button.LayoutParameters = new LinearLayout.LayoutParams(size, size)
        {
            Gravity = GravityFlags.CenterVertical,
        };
        (button.LayoutParameters as LinearLayout.LayoutParams)!.SetMargins(margin, 0, margin, 0);
        button.SetScaleType(ImageView.ScaleType.CenterInside);
        button.SetBackgroundColor(Android.Graphics.Color.Transparent);
        button.Enabled = false;
        button.Clickable = false;

        var iconSource = Icons.GetIcon(IconName.check_line);
        if (DUI.TryGetDrawableFromFileImageSource(iconSource, out var drawable) && drawable is not null)
        {
            drawable.SetTint(new Android.Graphics.Color(60, 179, 113).ToArgb());
            button.SetImageDrawable(drawable);
        }

        return button;
    }

    private AView CreateErrorView(ToolbarTaskButton toolbarTaskButton)
    {
        var size = DpToPx(ButtonSizeDp);
        var margin = DpToPx(ButtonMarginDp);
        var button = new ImageButton(Context);
        button.LayoutParameters = new LinearLayout.LayoutParams(size, size)
        {
            Gravity = GravityFlags.CenterVertical,
        };
        (button.LayoutParameters as LinearLayout.LayoutParams)!.SetMargins(margin, 0, margin, 0);
        button.SetScaleType(ImageView.ScaleType.CenterInside);
        button.Background = CreateRippleDrawable();

        var iconSource = Icons.GetIcon(IconName.important_line);
        if (DUI.TryGetDrawableFromFileImageSource(iconSource, out var drawable) && drawable is not null)
        {
            drawable.SetTint(Android.Graphics.Color.Red.ToArgb());
            button.SetImageDrawable(drawable);
        }

        button.ContentDescription = toolbarTaskButton.Title;
        button.Click += (_, _) => toolbarTaskButton.Error?.ErrorTappedCommand?.Execute(null);

        return button;
    }

    private void UpdateItems()
    {
        if (m_pillLayout is null || VirtualView is null)
            return;

        ClearButtonViews();
        m_buttonViewMap.Clear();
        m_separatorMap.Clear();
        m_pillLayout.RemoveAllViews();

        // Build the full layout with all buttons and separators upfront
        for (var g = 0; g < VirtualView.Groups.Count; g++)
        {
            var group = VirtualView.Groups[g];

            foreach (var button in group.Items)
            {
                var view = CreateViewForButton(button);
                m_buttonViewMap[button] = view;
                m_pillLayout.AddView(view);
            }

            // Add separator between groups (visibility toggled later)
            if (g < VirtualView.Groups.Count - 1)
            {
                var separator = CreateGroupSeparator();
                m_separatorMap[g] = separator;
                m_pillLayout.AddView(separator);
            }
        }

        // Set initial visibility without animation
        UpdateViewVisibility(animated: false);
    }

    /// <summary>
    /// Toggles Visibility (Visible/Gone) on button views and separators based on
    /// the current IsVisible state. When animated, uses TransitionManager for smooth
    /// fade + bounds animation without the "jump" caused by removing/re-adding views.
    /// </summary>
    private void UpdateViewVisibility(bool animated)
    {
        if (m_pillLayout is null || VirtualView is null)
            return;

        if (animated)
        {
            var transition = new AutoTransition();
            transition.SetDuration(200);
            // Animate on the container so the entire pill can fade in/out
            TransitionManager.BeginDelayedTransition((ViewGroup)m_pillLayout.Parent!, transition);
        }

        // Toggle button visibility
        foreach (var (button, view) in m_buttonViewMap)
        {
            view.Visibility = button.IsVisible ? ViewStates.Visible : ViewStates.Gone;
        }

        // Toggle separator visibility — only show if both adjacent groups have visible items
        for (var g = 0; g < VirtualView.Groups.Count - 1; g++)
        {
            if (!m_separatorMap.TryGetValue(g, out var separator))
                continue;

            var currentGroupHasVisible = VirtualView.Groups[g].Items.Any(b => b.IsVisible);
            var nextGroupHasVisible = VirtualView.Groups[g + 1].Items.Any(b => b.IsVisible);

            separator.Visibility = currentGroupHasVisible && nextGroupHasVisible
                ? ViewStates.Visible
                : ViewStates.Gone;
        }

        // Hide the entire pill when no items are visible
        var anyVisible = VirtualView.Groups.Any(g => g.Items.Any(b => b.IsVisible));
        m_pillLayout.Visibility = anyVisible ? ViewStates.Visible : ViewStates.Gone;

        UpdateAlignment();
    }

    /// <summary>
    /// Creates the appropriate native view for a button, considering ToolbarTaskButton states.
    /// </summary>
    private AView CreateViewForButton(ToolbarButton toolbarButton)
    {
        if (toolbarButton is ToolbarTaskButton taskButton)
        {
            if (taskButton.Error is { HasError: true })
            {
                return CreateErrorView(taskButton);
            }

            if (taskButton.IsBusy)
            {
                return CreateSpinnerView();
            }

            if (taskButton.IsFinished)
            {
                return CreateFinishedView();
            }
        }

        return CreateButtonView(toolbarButton);
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
        var size = DpToPx(ButtonSizeDp);
        var margin = DpToPx(ButtonMarginDp);
        var button = new ImageButton(Context);
        button.LayoutParameters = new LinearLayout.LayoutParams(size, size)
        {
            Gravity = GravityFlags.CenterVertical,
        };
        (button.LayoutParameters as LinearLayout.LayoutParams)!.SetMargins(margin, 0, margin, 0);
        button.SetScaleType(ImageView.ScaleType.CenterInside);

        // Circular ripple background
        button.Background = CreateRippleDrawable();

        // Set icon
        if (DUI.TryGetDrawableFromFileImageSource(toolbarButton.Icon, out var drawable) && drawable is not null)
        {
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
        button.Gravity = GravityFlags.Center;

        var height = DpToPx(ButtonSizeDp);
        var padH = DpToPx(TextButtonPaddingHorizontalDp);
        var margin = DpToPx(ButtonMarginDp);
        button.LayoutParameters = new LinearLayout.LayoutParams(
            ViewGroup.LayoutParams.WrapContent, height)
        {
            Gravity = GravityFlags.CenterVertical,
        };
        (button.LayoutParameters as LinearLayout.LayoutParams)!.SetMargins(margin, 0, margin, 0);
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
        separator.ImportantForAccessibility = ImportantForAccessibility.No;
        var width = DpToPx(SeparatorWidthDp);
        var height = DpToPx(SeparatorHeightDp);
        var margin = DpToPx(SeparatorMarginDp);

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
    /// Removes all child views from the pill layout and disposes them to release Java references
    /// and break event handler reference chains.
    /// </summary>
    private void ClearButtonViews()
    {
        if (m_pillLayout is null)
            return;

        for (var i = m_pillLayout.ChildCount - 1; i >= 0; i--)
        {
            var child = m_pillLayout.GetChildAt(i);
            m_pillLayout.RemoveViewAt(i);
            child?.Dispose();
        }
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

    /// <summary>
    /// Animates the toolbar sliding up into view.
    /// </summary>
    internal partial void AnimateShow()
    {
        if (PlatformView is null)
            return;

        PlatformView.Animate()!
            .TranslationY(0)
            .Alpha(1)
            .SetDuration(350)
            .SetInterpolator(new Android.Views.Animations.DecelerateInterpolator(2f))
            .Start();
    }

    /// <summary>
    /// Animates the toolbar sliding down off-screen.
    /// </summary>
    internal partial void AnimateHide()
    {
        if (PlatformView is null)
            return;

        // Slide down by container height + bottom margin to fully clear the screen
        var slideDistance = PlatformView.Height + DpToPx(AnimationSlideExtraDp);

        PlatformView.Animate()!
            .TranslationY(slideDistance)
            .Alpha(0)
            .SetDuration(250)
            .SetInterpolator(new Android.Views.Animations.AccelerateInterpolator(2f))
            .Start();
    }
}
