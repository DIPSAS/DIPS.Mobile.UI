using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.API.Diagnostics;

public static partial class LayoutDiagnosticsService
{
    private static LayoutDiagnosticsOverlay? s_overlay;
    private static UIWindow? s_overlayWindow;
    
    private static partial void AttachOverlay()
    {
        s_overlay = new LayoutDiagnosticsOverlay();
        
        var scene = DUI.RootController?.Window?.WindowScene;
        if (scene is null)
            return;
        
        s_overlayWindow = new OverlayWindow(scene)
        {
            WindowLevel = UIWindowLevel.Alert - 1,
            BackgroundColor = UIColor.Clear,
            UserInteractionEnabled = true,
            Hidden = false
        };
        
        var rootVc = new PassthroughViewController();
        s_overlayWindow.RootViewController = rootVc;
        
        var uiView = s_overlay.ToPlatform(DUI.GetCurrentMauiContext!);
        uiView.Tag = LayoutDiagnosticsOverlay.OverlayIdentifier;
        uiView.TranslatesAutoresizingMaskIntoConstraints = false;
        
        rootVc.View!.AddSubview(uiView);
        
        // Pin to all edges — MAUI's layout system positions content internally
        // via HorizontalOptions.End + VerticalOptions.Start + Margin on the overlay
        NSLayoutConstraint.ActivateConstraints([
            uiView.TopAnchor.ConstraintEqualTo(rootVc.View.TopAnchor),
            uiView.LeadingAnchor.ConstraintEqualTo(rootVc.View.LeadingAnchor),
            uiView.TrailingAnchor.ConstraintEqualTo(rootVc.View.TrailingAnchor),
            uiView.BottomAnchor.ConstraintEqualTo(rootVc.View.BottomAnchor)
        ]);
    }

    private static partial void RemoveOverlay()
    {
        if (s_overlayWindow is not null)
        {
            s_overlayWindow.Hidden = true;
            s_overlayWindow.RootViewController = null;
            s_overlayWindow.Dispose();
            s_overlayWindow = null;
        }
        
        s_overlay = null;
    }

    private static partial void UpdateOverlay(LayoutDiagnosticsSnapshot snapshot)
    {
        if (s_overlay is null)
            return;

        MainThread.BeginInvokeOnMainThread(() => s_overlay.UpdateWithSnapshot(snapshot));
    }

    private static partial void SetOverlayRecording()
    {
        if (s_overlay is null)
            return;

        MainThread.BeginInvokeOnMainThread(() => s_overlay.UpdateRecording());
    }

    private static partial void SetOverlayStopped()
    {
        if (s_overlay is null)
            return;

        MainThread.BeginInvokeOnMainThread(() => s_overlay.UpdateStopped());
    }
    
    /// <summary>
    /// A UIWindow that passes through touches on empty areas.
    /// Only the overlay content receives touch events.
    /// </summary>
    private class OverlayWindow(UIWindowScene scene) : UIWindow(scene)
    {
        public override UIView? HitTest(CGPoint point, UIEvent? uievent)
        {
            var hit = base.HitTest(point, uievent);
            // Pass through touches on empty areas: window, VC view, or the full-screen MAUI container
            if (hit is null || hit == this || hit == RootViewController?.View 
                || hit.Tag == LayoutDiagnosticsOverlay.OverlayIdentifier)
                return null;
            return hit;
        }
    }
    
    /// <summary>
    /// A view controller with a transparent view for hosting the overlay.
    /// </summary>
    private class PassthroughViewController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View!.BackgroundColor = UIColor.Clear;
            View.UserInteractionEnabled = true;
        }
    }
}
