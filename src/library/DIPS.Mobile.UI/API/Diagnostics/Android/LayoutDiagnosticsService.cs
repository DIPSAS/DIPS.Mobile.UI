using Android.Views;
using DIPS.Mobile.UI.Extensions.Android;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.API.Diagnostics;

public static partial class LayoutDiagnosticsService
{
    private static LayoutDiagnosticsOverlay? s_overlay;
    private static AView? s_nativeOverlay;
    
    private static partial void AttachOverlay()
    {
        s_overlay = new LayoutDiagnosticsOverlay();
        
        var activity = Platform.CurrentActivity;
        if (activity is null)
            return;

        var mauiContext = API.Library.DUI.GetCurrentMauiContext;
        if (mauiContext is null)
            return;
        
        s_nativeOverlay = s_overlay.ToPlatform(mauiContext);
        s_nativeOverlay.Id = LayoutDiagnosticsOverlay.OverlayIdentifier;
        
        AddOverlayToActivityContent();
    }

    private static partial void RemoveOverlay()
    {
        if (s_nativeOverlay is not null)
        {
            (s_nativeOverlay.Parent as ViewGroup)?.RemoveView(s_nativeOverlay);
            s_nativeOverlay = null;
        }
        
        s_overlay = null;
    }
    
    /// <summary>
    /// Moves the overlay into a dialog's window so it remains visible above it.
    /// Called by <see cref="DIPS.Mobile.UI.API.Library.FragmentLifeCycleCallback"/> when any DialogFragment starts.
    /// </summary>
    internal static void ElevateAboveDialog(Android.App.Dialog? dialog)
    {
        if (!IsInitialized || s_nativeOverlay is null || dialog?.Window?.DecorView is not ViewGroup decorView)
            return;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (s_nativeOverlay is null)
                return;
            
            (s_nativeOverlay.Parent as ViewGroup)?.RemoveView(s_nativeOverlay);
            decorView.AddView(s_nativeOverlay, CreateOverlayLayoutParams());
        });
    }
    
    /// <summary>
    /// Restores the overlay to the activity's content view.
    /// Called by <see cref="DIPS.Mobile.UI.API.Library.FragmentLifeCycleCallback"/> when any DialogFragment is destroyed.
    /// </summary>
    internal static void RestoreToActivityContent()
    {
        if (!IsInitialized || s_nativeOverlay is null)
            return;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (s_nativeOverlay is null)
                return;
            
            (s_nativeOverlay.Parent as ViewGroup)?.RemoveView(s_nativeOverlay);
            AddOverlayToActivityContent();
        });
    }
    
    private static void AddOverlayToActivityContent()
    {
        if (s_nativeOverlay is null)
            return;
        
        var activity = Platform.CurrentActivity;
        var contentView = activity?.FindViewById<ViewGroup>(Android.Resource.Id.Content);
        if (contentView is null)
            return;
        
        contentView.AddView(s_nativeOverlay, CreateOverlayLayoutParams());
    }
    
    private static Android.Widget.FrameLayout.LayoutParams CreateOverlayLayoutParams()
    {
        return new Android.Widget.FrameLayout.LayoutParams(
            ViewGroup.LayoutParams.MatchParent,
            ViewGroup.LayoutParams.MatchParent);
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
