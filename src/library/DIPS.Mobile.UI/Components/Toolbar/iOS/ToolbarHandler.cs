using CoreAnimation;
using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
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
    private UIToolbar? m_nativeToolbar;

    protected override UIView CreatePlatformView()
    {
        // The capsule container is the root platform view.
        // It uses a custom subclass that updates corner radius based on actual height.
        m_capsuleContainer = new CapsuleView();
        m_capsuleContainer.ClipsToBounds = false; // Allow button press animations to overflow
        m_capsuleContainer.Layer.CornerCurve = CACornerCurve.Continuous;

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

        // Native toolbar with transparent background
        m_nativeToolbar = new UIToolbar();
        m_nativeToolbar.TranslatesAutoresizingMaskIntoConstraints = false;
        m_nativeToolbar.SetBackgroundImage(new UIImage(), UIToolbarPosition.Any, UIBarMetrics.Default);
        m_nativeToolbar.SetShadowImage(new UIImage(), UIToolbarPosition.Any);
        m_nativeToolbar.BackgroundColor = UIColor.Clear;
        m_capsuleContainer.AddSubview(m_nativeToolbar);

        // Internal layout — toolbar drives the capsule height with vertical padding
        NSLayoutConstraint.ActivateConstraints(
        [
            // Glass fills capsule
            m_glassEffectView.LeadingAnchor.ConstraintEqualTo(m_capsuleContainer.LeadingAnchor),
            m_glassEffectView.TrailingAnchor.ConstraintEqualTo(m_capsuleContainer.TrailingAnchor),
            m_glassEffectView.TopAnchor.ConstraintEqualTo(m_capsuleContainer.TopAnchor),
            m_glassEffectView.BottomAnchor.ConstraintEqualTo(m_capsuleContainer.BottomAnchor),

            // Toolbar pinned to all edges with padding — drives capsule height
            m_nativeToolbar.LeadingAnchor.ConstraintEqualTo(m_capsuleContainer.LeadingAnchor, 8),
            m_nativeToolbar.TrailingAnchor.ConstraintEqualTo(m_capsuleContainer.TrailingAnchor, -8),
            m_nativeToolbar.TopAnchor.ConstraintEqualTo(m_capsuleContainer.TopAnchor, 8),
            m_nativeToolbar.BottomAnchor.ConstraintEqualTo(m_capsuleContainer.BottomAnchor, -8),
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

        m_nativeToolbar?.RemoveFromSuperview();
        m_nativeToolbar?.Dispose();
        m_nativeToolbar = null;

        m_glassEffectView?.RemoveFromSuperview();
        m_glassEffectView?.Dispose();
        m_glassEffectView = null;

        m_capsuleContainer = null;
    }

    private static partial void MapButtons(ToolbarHandler handler, Toolbar toolbar)
    {
        handler.UpdateButtons();
    }

    private void UpdateButtons()
    {
        if (m_nativeToolbar is null || VirtualView is null)
            return;

        var items = new List<UIBarButtonItem>();
        items.Add(new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace));

        foreach (var toolbarButton in VirtualView.Buttons)
        {
            items.Add(CreateBarButtonItem(toolbarButton));
            items.Add(new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace));
        }

        m_nativeToolbar.SetItems(items.ToArray(), false);
    }

    private static UIBarButtonItem CreateBarButtonItem(ToolbarButton toolbarButton)
    {
        UIImage? icon = null;
        if (DUI.TryGetUIImageFromImageSource(toolbarButton.Icon, out var uiImage) && uiImage is not null)
        {
            // Scale icon to 18pt to match Apple system toolbar sizing
            var targetSize = new CGSize(18, 18);
            var renderer = new UIGraphicsImageRenderer(targetSize);
            var scaledImage = renderer.CreateImage(ctx =>
            {
                uiImage.Draw(new CGRect(CGPoint.Empty, targetSize));
            });
            icon = scaledImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
        }

        var item = new UIBarButtonItem(icon, UIBarButtonItemStyle.Plain, (_, _) =>
        {
            toolbarButton.Command?.Execute(toolbarButton.CommandParameter);
        });

        item.Enabled = toolbarButton.IsEnabled;
        item.TintColor = Colors.GetColor(ColorName.color_icon_action).ToPlatform();

        if (!string.IsNullOrEmpty(toolbarButton.Title))
        {
            item.AccessibilityLabel = toolbarButton.Title;
        }

        return item;
    }
}
/// <summary>
/// A UIView subclass that maintains a pill-shaped corner radius equal to half its height.
/// This ensures the capsule adapts to Dynamic Type and display size changes.
/// </summary>
internal class CapsuleView : UIView
{
    private UIVisualEffectView? m_glassEffectView;

    internal void SetGlassEffectView(UIVisualEffectView glassEffectView)
    {
        m_glassEffectView = glassEffectView;
    }

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