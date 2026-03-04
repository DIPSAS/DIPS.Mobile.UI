using CoreAnimation;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Resources.Colors;
using CoreGraphics;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pages;

public partial class ContentPage
{
    // The glass capsule container that holds the toolbar
    private UIView? _capsuleContainer;
    private UIVisualEffectView? _glassEffectView;
    private UIToolbar? _bottomToolbar;
    private NSLayoutConstraint[]? _bottomToolbarConstraints;

    private partial void UpdateBottomToolbarOnPlatform()
    {
        Dispatcher.Dispatch(UpdateBottomToolbarOnPlatformCore);
    }

    private void UpdateBottomToolbarOnPlatformCore()
    {
        if (Handler?.PlatformView is not UIView pageView)
            return;

        if (BottomToolbar is not { } toolbar || toolbar.Buttons.Count == 0)
        {
            RemoveNativeBottomToolbar();
            return;
        }

        var items = new List<UIBarButtonItem>();
        items.Add(new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace));

        foreach (var toolbarButton in toolbar.Buttons)
        {
            items.Add(CreateBarButtonItem(toolbarButton));
            items.Add(new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace));
        }

        EnsureNativeBottomToolbar(pageView);
        _bottomToolbar!.SetItems(items.ToArray(), false);
    }

    private void EnsureNativeBottomToolbar(UIView pageView)
    {
        if (_capsuleContainer is not null)
            return;

        var parentVC = FindViewController();
        var containerView = parentVC?.View ?? pageView;

        // 1. Create the capsule container — a rounded rect that clips its children
        _capsuleContainer = new UIView();
        _capsuleContainer.TranslatesAutoresizingMaskIntoConstraints = false;
        _capsuleContainer.ClipsToBounds = false; // Don't clip — allows button press animations to overflow
        _capsuleContainer.Layer.CornerRadius = 30; // Half of 60pt height = true pill shape
        _capsuleContainer.Layer.CornerCurve = CoreAnimation.CACornerCurve.Continuous;
        containerView.AddSubview(_capsuleContainer);

        // 2. Create the glass background using UIVisualEffectView + UIGlassEffect
        if (OperatingSystem.IsIOSVersionAtLeast(26))
        {
            _glassEffectView = new UIVisualEffectView(new UIGlassEffect());
        }
        else
        {
            // Fallback for pre-iOS 26: system thin material blur
            _glassEffectView = new UIVisualEffectView(UIBlurEffect.FromStyle(UIBlurEffectStyle.SystemThinMaterial));
        }
        _glassEffectView.TranslatesAutoresizingMaskIntoConstraints = false;
        _glassEffectView.ClipsToBounds = true;
        _glassEffectView.Layer.CornerRadius = 30; // Match capsule
        _glassEffectView.Layer.CornerCurve = CACornerCurve.Continuous;
        _capsuleContainer.AddSubview(_glassEffectView);

        // 3. Create the toolbar with transparent background inside the capsule
        _bottomToolbar = new UIToolbar();
        _bottomToolbar.TranslatesAutoresizingMaskIntoConstraints = false;
        _bottomToolbar.SetBackgroundImage(new UIImage(), UIToolbarPosition.Any, UIBarMetrics.Default);
        _bottomToolbar.SetShadowImage(new UIImage(), UIToolbarPosition.Any);
        _bottomToolbar.BackgroundColor = UIColor.Clear;
        _capsuleContainer.AddSubview(_bottomToolbar);

        // Layout constraints
        var safeArea = containerView.SafeAreaLayoutGuide;
        _bottomToolbarConstraints =
        [
            // Capsule container: horizontal insets + pinned to safe area bottom
            _capsuleContainer.LeadingAnchor.ConstraintEqualTo(safeArea.LeadingAnchor, 48),
            _capsuleContainer.TrailingAnchor.ConstraintEqualTo(safeArea.TrailingAnchor, -48),
            _capsuleContainer.BottomAnchor.ConstraintEqualTo(safeArea.BottomAnchor, -8),
            _capsuleContainer.HeightAnchor.ConstraintEqualTo(60),

            // Glass effect view fills capsule
            _glassEffectView.LeadingAnchor.ConstraintEqualTo(_capsuleContainer.LeadingAnchor),
            _glassEffectView.TrailingAnchor.ConstraintEqualTo(_capsuleContainer.TrailingAnchor),
            _glassEffectView.TopAnchor.ConstraintEqualTo(_capsuleContainer.TopAnchor),
            _glassEffectView.BottomAnchor.ConstraintEqualTo(_capsuleContainer.BottomAnchor),

            // Toolbar inset inside capsule for horizontal padding, vertically centered
            _bottomToolbar.LeadingAnchor.ConstraintEqualTo(_capsuleContainer.LeadingAnchor, 8),
            _bottomToolbar.TrailingAnchor.ConstraintEqualTo(_capsuleContainer.TrailingAnchor, -8),
            _bottomToolbar.CenterYAnchor.ConstraintEqualTo(_capsuleContainer.CenterYAnchor),
            _bottomToolbar.HeightAnchor.ConstraintEqualTo(44),
        ];
        NSLayoutConstraint.ActivateConstraints(_bottomToolbarConstraints);

        containerView.SetNeedsLayout();
        containerView.LayoutIfNeeded();
    }

    private void RemoveNativeBottomToolbar()
    {
        if (_capsuleContainer is null)
            return;

        if (_bottomToolbarConstraints is not null)
        {
            NSLayoutConstraint.DeactivateConstraints(_bottomToolbarConstraints);
            _bottomToolbarConstraints = null;
        }

        _bottomToolbar?.RemoveFromSuperview();
        _bottomToolbar?.Dispose();
        _bottomToolbar = null;

        _glassEffectView?.RemoveFromSuperview();
        _glassEffectView?.Dispose();
        _glassEffectView = null;

        _capsuleContainer.RemoveFromSuperview();
        _capsuleContainer.Dispose();
        _capsuleContainer = null;
    }

    private partial void HideBottomToolbarOnPlatform()
    {
        RemoveNativeBottomToolbar();
    }

    private UIViewController? FindViewController()
    {
        if (Handler?.PlatformView is not UIView platformView)
            return null;

        var responder = platformView.NextResponder;
        while (responder is not null)
        {
            if (responder is UIViewController vc)
                return vc;
            responder = responder.NextResponder;
        }

        return null;
    }

    private static UIBarButtonItem CreateBarButtonItem(Toolbar.ToolbarButton toolbarButton)
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
