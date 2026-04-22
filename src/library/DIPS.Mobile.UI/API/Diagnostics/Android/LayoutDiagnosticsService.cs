using DIPS.Mobile.UI.Extensions.Android;
using Microsoft.Maui.Platform;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.API.Diagnostics;

public static partial class LayoutDiagnosticsService
{
    private static LayoutDiagnosticsOverlay? s_overlay;
    
    private static partial void AttachOverlay()
    {
        s_overlay = new LayoutDiagnosticsOverlay();
        
        var activity = Platform.CurrentActivity;
        if (activity is null)
            return;

        var mauiContext = API.Library.DUI.GetCurrentMauiContext;
        if (mauiContext is null)
            return;
        
        var nativeView = s_overlay.ToPlatform(mauiContext);
        nativeView.Id = LayoutDiagnosticsOverlay.OverlayIdentifier;
        
        var contentView = activity.FindViewById<Android.Views.ViewGroup>(Android.Resource.Id.Content);
        if (contentView is null)
            return;
        
        var layoutParams = new Android.Widget.FrameLayout.LayoutParams(
            Android.Views.ViewGroup.LayoutParams.WrapContent,
            Android.Views.ViewGroup.LayoutParams.WrapContent,
            Android.Views.GravityFlags.Top | Android.Views.GravityFlags.End)
        {
            TopMargin = (int)(54 * activity.Resources!.DisplayMetrics!.Density),
            RightMargin = (int)(8 * activity.Resources!.DisplayMetrics!.Density)
        };
        
        contentView.AddView(nativeView, layoutParams);
    }

    private static partial void RemoveOverlay()
    {
        var activity = Platform.CurrentActivity;
        if (activity is null)
            return;
        
        var contentView = activity.FindViewById<Android.Views.ViewGroup>(Android.Resource.Id.Content);
        var overlayView = contentView?.FindViewById(LayoutDiagnosticsOverlay.OverlayIdentifier);
        if (overlayView is not null)
        {
            contentView!.RemoveView(overlayView);
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
