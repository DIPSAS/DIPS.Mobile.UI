using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.API.Diagnostics;

public static partial class LayoutDiagnosticsService
{
    private static LayoutDiagnosticsOverlay? s_overlay;
    
    private static partial void AttachOverlay()
    {
        s_overlay = new LayoutDiagnosticsOverlay();
        
        var rootView = DUI.RootController;
        if (rootView is null)
            return;
        
        var uiView = s_overlay.ToPlatform(DUI.GetCurrentMauiContext!);
        uiView.Tag = LayoutDiagnosticsOverlay.OverlayIdentifier;
        uiView.TranslatesAutoresizingMaskIntoConstraints = false;
        
        rootView.AddSubview(uiView);
        
        NSLayoutConstraint.ActivateConstraints([
            uiView.TopAnchor.ConstraintEqualTo(rootView.SafeAreaLayoutGuide.TopAnchor, 8),
            uiView.TrailingAnchor.ConstraintEqualTo(rootView.SafeAreaLayoutGuide.TrailingAnchor, -8)
        ]);
    }

    private static partial void RemoveOverlay()
    {
        var rootView = DUI.RootController;
        if (rootView is null)
            return;

        foreach (var subview in rootView.Subviews)
        {
            if (subview.Tag == LayoutDiagnosticsOverlay.OverlayIdentifier)
            {
                subview.RemoveFromSuperview();
                break;
            }
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
}
