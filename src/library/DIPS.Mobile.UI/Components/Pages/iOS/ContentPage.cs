using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Resources.Colors;
using CoreGraphics;
using Microsoft.Maui.Platform;
using ObjCRuntime;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pages;

public partial class ContentPage
{
    // We create our own UINavigationController solely to get a system-managed UIToolbar.
    // MAUI's NavigationRenderer completely overrides UINavigationController's toolbar mechanism,
    // so we cannot use it. By hosting our own nav controller, the toolbar gets Liquid Glass
    // on iOS 26+ just like Apple intended — because UIKit manages it.
    private UINavigationController? _toolbarNavController;
    private UIViewController? _toolbarHostVC;
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
        _toolbarHostVC!.SetToolbarItems(items.ToArray(), false);
    }

    private void EnsureNativeBottomToolbar(UIView pageView)
    {
        if (_toolbarNavController is not null)
            return;

        // Create a dummy VC to host toolbar items on
        _toolbarHostVC = new UIViewController();
        _toolbarHostVC.View!.BackgroundColor = UIColor.Clear;

        // Create our own UINavigationController — this gives us a system-managed toolbar
        // that gets Liquid Glass on iOS 26+ automatically.
        // MAUI's NavigationRenderer blocks the standard approach, so we host our own.
        _toolbarNavController = new UINavigationController(_toolbarHostVC);
        _toolbarNavController.SetToolbarHidden(false, false);

        // Hide the navigation bar — we only want the toolbar
        _toolbarNavController.SetNavigationBarHidden(true, false);

        // Explicitly configure toolbar appearance — use system default so iOS 26
        // renders the glass capsule background (not transparent or opaque overrides)
        var toolbar = _toolbarNavController.Toolbar;
        toolbar.Translucent = true;
        var toolbarAppearance = new UIToolbarAppearance();
        toolbarAppearance.ConfigureWithDefaultBackground();
        toolbar.StandardAppearance = toolbarAppearance;
        toolbar.ScrollEdgeAppearance = toolbarAppearance;
        if (toolbar.RespondsToSelector(new Selector("compactScrollEdgeAppearance")))
        {
            toolbar.CompactScrollEdgeAppearance = toolbarAppearance;
        }

        _toolbarNavController.View!.TranslatesAutoresizingMaskIntoConstraints = false;

        // Add to the parent VC as a child view controller for proper containment.
        // This is important — the system needs proper VC containment to apply Liquid Glass.
        var parentVC = FindViewController();
        if (parentVC is not null)
        {
            parentVC.AddChildViewController(_toolbarNavController);
            parentVC.View!.AddSubview(_toolbarNavController.View);
            _toolbarNavController.DidMoveToParentViewController(parentVC);
        }
        else
        {
            // Fallback: add to window
            var window = pageView.Window ?? UIApplication.SharedApplication.KeyWindow;
            var targetView = window ?? pageView;
            targetView.AddSubview(_toolbarNavController.View);
        }

        // Pin the nav controller's view to all edges of the parent.
        // Let the system manage the toolbar's position and size naturally.
        // The toolbar will sit at the bottom; the rest of the view area above it
        // is the host VC's view (transparent, so content shows through).
        var containerView = _toolbarNavController.View.Superview!;
        _bottomToolbarConstraints =
        [
            _toolbarNavController.View.LeadingAnchor.ConstraintEqualTo(containerView.LeadingAnchor),
            _toolbarNavController.View.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor),
            _toolbarNavController.View.BottomAnchor.ConstraintEqualTo(containerView.BottomAnchor),
            _toolbarNavController.View.TopAnchor.ConstraintEqualTo(containerView.TopAnchor)
        ];
        NSLayoutConstraint.ActivateConstraints(_bottomToolbarConstraints);

        // DEBUG: Dump toolbar diagnostics after layout
        _toolbarNavController.View.LayoutIfNeeded();
        DumpToolbarDiagnostics();
    }

    private void DumpToolbarDiagnostics()
    {
        if (_toolbarNavController is null) return;

        var toolbar = _toolbarNavController.Toolbar;
        var info = $"=== TOOLBAR DIAGNOSTICS ===\n";
        info += $"Toolbar frame: {toolbar.Frame}\n";
        info += $"Toolbar bounds: {toolbar.Bounds}\n";
        info += $"Toolbar translucent: {toolbar.Translucent}\n";
        info += $"Toolbar hidden: {toolbar.Hidden}\n";
        info += $"Toolbar alpha: {toolbar.Alpha}\n";
        info += $"Toolbar barStyle: {toolbar.BarStyle}\n";
        info += $"Toolbar barTintColor: {toolbar.BarTintColor}\n";
        info += $"Toolbar backgroundColor: {toolbar.BackgroundColor}\n";
        info += $"Toolbar clipsToBounds: {toolbar.ClipsToBounds}\n";
        info += $"Toolbar items count: {toolbar.Items?.Length ?? 0}\n";
        info += $"NavController view frame: {_toolbarNavController.View!.Frame}\n";
        info += $"HostVC view frame: {_toolbarHostVC?.View?.Frame}\n";
        info += $"NavController view userInteraction: {_toolbarNavController.View.UserInteractionEnabled}\n";
        info += $"Toolbar userInteraction: {toolbar.UserInteractionEnabled}\n";

        // Check for glass-related selectors
        var glassSelectors = new[]
        {
            "preferredGlassEffect", "glassEffect", "_glassEffect",
            "setPreferredGlassEffect:", "_setGlassEffect:",
            "glassContainerView", "_backgroundEffectView",
            "preferredBarStyle", "_effectiveGlassEffect",
            "backgroundEffect", "setBackgroundEffect:",
            "_liquidGlassFrame", "_liquidGlassStyle",
            "preferredBackgroundStyle", "setPreferredBackgroundStyle:",
            "_backdropStyle", "setBackdropStyle:",
        };

        info += "\n=== GLASS SELECTORS ===\n";
        foreach (var sel in glassSelectors)
        {
            var responds = toolbar.RespondsToSelector(new Selector(sel));
            if (responds)
                info += $"  ✅ {sel}\n";
        }

        // Dump toolbar subview hierarchy
        info += "\n=== TOOLBAR SUBVIEWS ===\n";
        info += DumpViewHierarchy(toolbar, 0);

        // Also dump navcontroller.toolbar parent
        info += $"\n=== TOOLBAR SUPERVIEW ===\n";
        info += $"SuperView: {toolbar.Superview?.GetType().Name} frame={toolbar.Superview?.Frame}\n";

        System.Diagnostics.Debug.WriteLine(info);
        Console.WriteLine(info);

        // Also show as alert for easy reading on device
        var alert = UIAlertController.Create("Toolbar Info", info, UIAlertControllerStyle.Alert);
        alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
        _toolbarNavController.PresentViewController(alert, true, null);
    }

    private static string DumpViewHierarchy(UIView view, int depth)
    {
        var indent = new string(' ', depth * 2);
        var result = $"{indent}{view.GetType().Name} (ObjC: {view.Class.Name}) frame={view.Frame} alpha={view.Alpha} hidden={view.Hidden} clips={view.ClipsToBounds}\n";
        foreach (var subview in view.Subviews)
        {
            result += DumpViewHierarchy(subview, depth + 1);
        }
        return result;
    }

    private void RemoveNativeBottomToolbar()
    {
        if (_toolbarNavController is null)
            return;

        if (_bottomToolbarConstraints is not null)
        {
            NSLayoutConstraint.DeactivateConstraints(_bottomToolbarConstraints);
            _bottomToolbarConstraints = null;
        }

        _toolbarNavController.WillMoveToParentViewController(null);
        _toolbarNavController.View?.RemoveFromSuperview();
        _toolbarNavController.RemoveFromParentViewController();
        _toolbarNavController.Dispose();
        _toolbarNavController = null;

        _toolbarHostVC?.Dispose();
        _toolbarHostVC = null;
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
        if (DUI.TryGetUIImageFromImageSource(toolbarButton.Icon, out var uiImage))
        {
            icon = uiImage?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
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
