using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Pages;

public partial class ContentPage
{
    private AView? m_toolbarPlatformView;
    private Java.Lang.Object? m_scrollTracker;
    private AView? m_scrollTrackingTarget;

    private partial void AttachBottomToolbarOnPlatform()
    {
        if (BottomToolbar is null || Handler?.MauiContext is not { } mauiContext)
            return;

        // Remove existing if re-attaching
        DetachBottomToolbarOnPlatform();

        m_toolbarPlatformView = BottomToolbar.ToPlatform(mauiContext);

        var pageView = Handler.PlatformView as AView;
        if (pageView is null)
            return;

        // The page view may not be attached to the window yet when OnHandlerChanged fires.
        // Wait for it to be attached, then add the toolbar to its parent container.
        if (!pageView.IsAttachedToWindow)
        {
            pageView.ViewAttachedToWindow += OnPageViewAttachedToWindow;
        }
        else
        {
            AddToolbarToPageParent(pageView);
        }
    }

    private void OnPageViewAttachedToWindow(object? sender, AView.ViewAttachedToWindowEventArgs e)
    {
        if (sender is AView pageView)
        {
            pageView.ViewAttachedToWindow -= OnPageViewAttachedToWindow;
            AddToolbarToPageParent(pageView);
        }
    }

    private void AddToolbarToPageParent(AView pageView)
    {
        if (m_toolbarPlatformView is null)
            return;

        // Remove from any existing parent first — ToPlatform() may have attached it internally
        (m_toolbarPlatformView.Parent as ViewGroup)?.RemoveView(m_toolbarPlatformView);

        // Walk up from the page view to find the nearest FrameLayout we can overlay into
        var container = FindFrameLayoutParent(pageView);
        if (container is null)
        {
            // Fallback to Activity root
            container = Platform.CurrentActivity?.FindViewById<FrameLayout>(Android.Resource.Id.Content);
        }

        if (container is null)
            return;

        var density = Platform.AppContext.Resources!.DisplayMetrics!.Density;
        var marginH = (int)(16 * density); // M3 spec: minimum 16dp horizontal margin
        var marginBottom = (int)(16 * density); // M3 spec: 16dp above gesture indicator

        var lp = new FrameLayout.LayoutParams(
            ViewGroup.LayoutParams.MatchParent,
            ViewGroup.LayoutParams.WrapContent,
            GravityFlags.Bottom);
        lp.SetMargins(marginH, 0, marginH, marginBottom);

        // Ensure the parent doesn't clip the toolbar's elevation shadow
        container.SetClipChildren(false);
        container.SetClipToPadding(false);

        container.AddView(m_toolbarPlatformView, lp);
    }

    private partial void DetachBottomToolbarOnPlatform()
    {
        if (Handler?.PlatformView is AView pageView)
        {
            pageView.ViewAttachedToWindow -= OnPageViewAttachedToWindow;
        }

        if (m_toolbarPlatformView is null)
            return;

        (m_toolbarPlatformView.Parent as ViewGroup)?.RemoveView(m_toolbarPlatformView);
        m_toolbarPlatformView = null;

        // Disconnect the toolbar's handler to release platform resources
        BottomToolbar?.Handler?.DisconnectHandler();
    }

    private static ViewGroup? FindFrameLayoutParent(AView? view)
    {
        var current = view?.Parent;
        while (current is not null)
        {
            // FragmentContainerView extends FrameLayout but only accepts Fragment-associated views
            if (current is FrameLayout frameLayout and not FragmentContainerView)
                return frameLayout;

            current = (current as AView)?.Parent;
        }

        return null;
    }

    /// <summary>
    /// Enables scroll direction tracking by attaching a touch listener to the
    /// platform view of the referenced <see cref="VisualElement"/>.
    /// Similar to iOS's UIPanGestureRecognizer — detects vertical drag direction
    /// from touch events on the outer view, regardless of what scrollable child is inside.
    /// </summary>
    private partial void EnableScrollTrackingForView(VisualElement view)
    {
        DisableScrollTracking();

        var platformView = view.Handler?.PlatformView as AView;
        if (platformView is null)
            return;

        m_scrollTrackingTarget = platformView;

        var tracker = new TouchScrollTracker(this);
        m_scrollTracker = tracker;
        platformView.SetOnTouchListener(tracker);
    }

    private partial void DisableScrollTracking()
    {
        if (m_scrollTrackingTarget is not null && m_scrollTracker is not null)
        {
            m_scrollTrackingTarget.SetOnTouchListener(null);
        }

        m_scrollTracker = null;
        m_scrollTrackingTarget = null;
    }

    /// <summary>
    /// Detects vertical drag direction from touch events on any view.
    /// Returns false from OnTouch so touch events continue to propagate to children.
    /// This is the Android equivalent of iOS's UIPanGestureRecognizer.
    /// </summary>
    private class TouchScrollTracker : Java.Lang.Object, AView.IOnTouchListener
    {
        private readonly ContentPage m_page;
        private float m_startY;
        private float m_lastReportedY;

        public TouchScrollTracker(nint handle, Android.Runtime.JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public TouchScrollTracker(ContentPage page)
        {
            m_page = page;
        }

        public bool OnTouch(AView? v, MotionEvent? e)
        {
            if (e is null)
                return false;

            switch (e.ActionMasked)
            {
                case MotionEventActions.Down:
                    m_startY = e.RawY;
                    m_lastReportedY = e.RawY;
                    break;

                case MotionEventActions.Move:
                    var deltaFromLast = e.RawY - m_lastReportedY;
                    var density = v?.Context?.Resources?.DisplayMetrics?.Density ?? 1f;
                    var thresholdPx = 10 * density; // 10dp threshold

                    if (Math.Abs(deltaFromLast) > thresholdPx)
                    {
                        // Negative delta = finger moved up = scrolling down (content going up)
                        m_page.OnScrollDirectionDetected(isScrollingDown: deltaFromLast < 0);
                        m_lastReportedY = e.RawY;
                    }
                    break;
            }

            // Return false so touch events continue to the actual scrollable child
            return false;
        }
    }
}
