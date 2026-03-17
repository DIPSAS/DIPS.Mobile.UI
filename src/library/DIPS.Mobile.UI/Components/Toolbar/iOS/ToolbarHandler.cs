using CoreAnimation;
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

public partial class ToolbarHandler : ViewHandler<Toolbar, UIView>
{
    private CapsuleView? m_capsuleContainer;
    private UIVisualEffectView? m_glassEffectView;
    private UIStackView? m_stackView;

    protected override UIView CreatePlatformView()
    {
        m_capsuleContainer = new CapsuleView();
        m_capsuleContainer.ClipsToBounds = false;
        m_capsuleContainer.Layer.CornerCurve = CACornerCurve.Continuous;
        m_capsuleContainer.SetContentHuggingPriority((float)UILayoutPriority.Required, UILayoutConstraintAxis.Horizontal);

        // Glass background
        if (OperatingSystem.IsIOSVersionAtLeast(26))
        {
            m_glassEffectView = new UIVisualEffectView(new UIGlassEffect());
        }
        else
        {
            m_glassEffectView = new UIVisualEffectView(UIBlurEffect.FromStyle(UIBlurEffectStyle.SystemThinMaterial));
        }

        m_glassEffectView.TranslatesAutoresizingMaskIntoConstraints = false;
        m_glassEffectView.ClipsToBounds = true;
        m_glassEffectView.Layer.CornerCurve = CACornerCurve.Continuous;
        m_capsuleContainer.AddSubview(m_glassEffectView);
        m_capsuleContainer.SetGlassEffectView(m_glassEffectView);

        // Stack view for individual buttons
        m_stackView = new UIStackView();
        m_stackView.TranslatesAutoresizingMaskIntoConstraints = false;
        m_stackView.Axis = UILayoutConstraintAxis.Horizontal;
        m_stackView.Spacing = 4;
        m_stackView.Alignment = UIStackViewAlignment.Center;
        m_stackView.Distribution = UIStackViewDistribution.Fill;
        m_capsuleContainer.AddSubview(m_stackView);

        NSLayoutConstraint.ActivateConstraints(
        [
            // Glass fills capsule
            m_glassEffectView.LeadingAnchor.ConstraintEqualTo(m_capsuleContainer.LeadingAnchor),
            m_glassEffectView.TrailingAnchor.ConstraintEqualTo(m_capsuleContainer.TrailingAnchor),
            m_glassEffectView.TopAnchor.ConstraintEqualTo(m_capsuleContainer.TopAnchor),
            m_glassEffectView.BottomAnchor.ConstraintEqualTo(m_capsuleContainer.BottomAnchor),

            // Stack view pinned with padding
            m_stackView.LeadingAnchor.ConstraintEqualTo(m_capsuleContainer.LeadingAnchor, 6),
            m_stackView.TrailingAnchor.ConstraintEqualTo(m_capsuleContainer.TrailingAnchor, -6),
            m_stackView.TopAnchor.ConstraintEqualTo(m_capsuleContainer.TopAnchor, 6),
            m_stackView.BottomAnchor.ConstraintEqualTo(m_capsuleContainer.BottomAnchor, -6),
        ]);

        return m_capsuleContainer;
    }

    protected override void ConnectHandler(UIView platformView)
    {
        base.ConnectHandler(platformView);
        UpdateButtons();
    }

    protected override void DisconnectHandler(UIView platformView)
    {
        base.DisconnectHandler(platformView);

        if (m_stackView is not null)
        {
            foreach (var subview in m_stackView.ArrangedSubviews)
            {
                m_stackView.RemoveArrangedSubview(subview);
                subview.RemoveFromSuperview();
                subview.Dispose();
            }

            m_stackView.RemoveFromSuperview();
            m_stackView.Dispose();
            m_stackView = null;
        }

        m_glassEffectView?.RemoveFromSuperview();
        m_glassEffectView?.Dispose();
        m_glassEffectView = null;

        m_capsuleContainer = null;
    }

    private static partial void MapButtons(ToolbarHandler handler, Toolbar toolbar)
    {
        handler.UpdateButtons();
    }

    private static partial void MapHorizontalAlignment(ToolbarHandler handler, Toolbar toolbar)
    {
        handler.UpdateButtons();
    }

    private void UpdateButtons()
    {
        if (m_stackView is null || VirtualView is null)
            return;

        // Clear existing buttons
        foreach (var subview in m_stackView.ArrangedSubviews)
        {
            m_stackView.RemoveArrangedSubview(subview);
            subview.RemoveFromSuperview();
            subview.Dispose();
        }

        var isCompact = VirtualView.HorizontalAlignment != ToolbarHorizontalAlignment.Center;

        // In full-width mode, distribute buttons evenly
        m_stackView.Distribution = isCompact
            ? UIStackViewDistribution.Fill
            : UIStackViewDistribution.EqualSpacing;

        foreach (var toolbarButton in VirtualView.Buttons)
        {
            var button = CreateButton(toolbarButton);
            m_stackView.AddArrangedSubview(button);
        }
    }

    private static UIButton CreateButton(ToolbarButton toolbarButton)
    {
        var button = new UIButton(UIButtonType.System);
        button.TranslatesAutoresizingMaskIntoConstraints = false;

        UIImage? icon = null;
        if (DUI.TryGetUIImageFromImageSource(toolbarButton.Icon, out var uiImage) && uiImage is not null)
        {
            var targetSize = new CGSize(22, 22);
            var renderer = new UIGraphicsImageRenderer(targetSize);
            var scaledImage = renderer.CreateImage(ctx =>
            {
                uiImage.Draw(new CGRect(CGPoint.Empty, targetSize));
            });
            icon = scaledImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
        }

        if (icon is not null)
        {
            button.SetImage(icon, UIControlState.Normal);
        }

        button.TintColor = Colors.GetColor(ColorName.color_icon_action).ToPlatform();

        // 44x44 minimum touch target
        NSLayoutConstraint.ActivateConstraints(
        [
            button.WidthAnchor.ConstraintGreaterThanOrEqualTo(44),
            button.HeightAnchor.ConstraintGreaterThanOrEqualTo(44),
        ]);

        // Accessibility
        if (!string.IsNullOrEmpty(toolbarButton.Title))
        {
            button.AccessibilityLabel = toolbarButton.Title;
        }

        button.Enabled = toolbarButton.IsEnabled;

        // Context menu support
        if (toolbarButton.Menu is { } contextMenu && contextMenu.ItemsSource is { Count: > 0 })
        {
            var menuItems = ContextMenuHelper.CreateMenuItems(contextMenu.ItemsSource, contextMenu);
            var uiMenu = UIMenu.Create(menuItems.Select(kvp => kvp.Value).ToArray());
            button.Menu = uiMenu;
            button.ShowsMenuAsPrimaryAction = true;
        }
        else
        {
            button.PrimaryActionTriggered += (_, _) =>
            {
                toolbarButton.Command?.Execute(toolbarButton.CommandParameter);
            };
        }

        return button;
    }
}

/// <summary>
/// A UIView subclass that maintains a pill-shaped corner radius equal to half its height.
/// </summary>
internal class CapsuleView : UIView
{
    private UIVisualEffectView? m_glassEffectView;

    internal void SetGlassEffectView(UIVisualEffectView glassEffectView)
    {
        m_glassEffectView = glassEffectView;
    }

    public override CGSize IntrinsicContentSize => CGSize.Empty;

    public override void LayoutSubviews()
    {
        base.LayoutSubviews();

        var cornerRadius = Bounds.Size.Height / 2;
        Layer.CornerRadius = cornerRadius;

        if (m_glassEffectView is not null)
        {
            m_glassEffectView.Layer.CornerRadius = cornerRadius;
        }
    }
}