using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using AndroidX.RecyclerView.Widget;
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
    /// Enables scroll direction tracking on the platform view of the referenced <see cref="VisualElement"/>.
    /// Finds the actual scrollable descendant (RecyclerView, ScrollView, NestedScrollView) inside
    /// the target view tree — necessary because controls like Telerik RadPdfViewer handle scrolling
    /// in an internal child view, not the outer container.
    /// Attaches the ViewTreeObserver listener to the outer view (fires for any descendant scroll)
    /// but reads scroll position from the found scrollable child.
    /// </summary>
    private partial void EnableScrollTrackingForView(VisualElement view)
    {
        DisableScrollTracking();

        var platformView = view.Handler?.PlatformView as AView;
        if (platformView is null)
            return;

        m_scrollTrackingTarget = platformView;

        // Find the actual scrollable child to read scroll position from.
        // The ViewTreeObserver callback fires for any descendant scroll,
        // but we need the right view to read ScrollY from.
        var scrollableView = FindScrollableDescendant(platformView) ?? platformView;

        if (scrollableView is RecyclerView rv)
        {
            var tracker = new RecyclerViewScrollTracker(this);
            m_scrollTracker = tracker;
            rv.AddOnScrollListener(tracker);
            // Also track the RecyclerView so we can remove the listener
            m_scrollTrackingTarget = rv;
        }
        else
        {
            var tracker = new ViewScrollTracker(this, scrollableView);
            m_scrollTracker = tracker;
            // Attach to the OUTER view's ViewTreeObserver — it fires for descendant scrolls too
            platformView.ViewTreeObserver?.AddOnScrollChangedListener(tracker);
        }
    }

    private partial void DisableScrollTracking()
    {
        if (m_scrollTracker is ViewScrollTracker vst && m_scrollTrackingTarget is not null)
        {
            m_scrollTrackingTarget.ViewTreeObserver?.RemoveOnScrollChangedListener(vst);
            vst.Dispose();
        }
        else if (m_scrollTracker is RecyclerViewScrollTracker rst && m_scrollTrackingTarget is RecyclerView rv)
        {
            rv.RemoveOnScrollListener(rst);
            rst.Dispose();
        }

        m_scrollTracker = null;
        m_scrollTrackingTarget = null;
    }

    /// <summary>
    /// Walks the view tree depth-first to find the first scrollable descendant
    /// (RecyclerView, ScrollView, NestedScrollView).
    /// These are the only standard scroll containers in Android — any third-party control
    /// (Telerik, etc.) uses one of them internally.
    /// </summary>
    private static AView? FindScrollableDescendant(AView view)
    {
        if (view is RecyclerView or Android.Widget.ScrollView or AndroidX.Core.Widget.NestedScrollView)
            return view;

        if (view is not ViewGroup viewGroup)
            return null;

        for (var i = 0; i < viewGroup.ChildCount; i++)
        {
            var child = viewGroup.GetChildAt(i);
            if (child is null)
                continue;

            var found = FindScrollableDescendant(child);
            if (found is not null)
                return found;
        }

        return null;
    }

    /// <summary>
    /// Tracks scroll direction by reading ScrollY from the actual scrollable view.
    /// Attached to the outer view's ViewTreeObserver which fires for descendant scrolls.
    /// </summary>
    private class ViewScrollTracker : Java.Lang.Object, ViewTreeObserver.IOnScrollChangedListener
    {
        private ContentPage? m_page;
        private AView? m_view;
        private int m_lastScrollY;

        // Required by Android runtime for Java peer re-creation.
        // Fields will be null — OnScrollChanged guards against this.
        public ViewScrollTracker(nint handle, Android.Runtime.JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public ViewScrollTracker(ContentPage page, AView scrollableView)
        {
            m_page = page;
            m_view = scrollableView;
            m_lastScrollY = scrollableView.ScrollY;
        }

        public void OnScrollChanged()
        {
            if (m_view is null || m_page is null)
                return;

            var currentY = m_view.ScrollY;
            var delta = currentY - m_lastScrollY;

            // ~3dp threshold to avoid noise
            if (Math.Abs(delta) > 10)
            {
                m_page.OnScrollDirectionDetected(isScrollingDown: delta > 0);
                m_lastScrollY = currentY;
            }
        }
    }

    /// <summary>
    /// Tracks scroll direction for RecyclerView-backed views (CollectionView, etc.)
    /// using the additive OnScrollListener API.
    /// </summary>
    private class RecyclerViewScrollTracker : RecyclerView.OnScrollListener
    {
        private readonly ContentPage m_page;
        private int m_accumulatedDy;

        public RecyclerViewScrollTracker(ContentPage page)
        {
            m_page = page;
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            m_accumulatedDy += dy;

            var density = recyclerView.Context?.Resources?.DisplayMetrics?.Density ?? 1f;
            var thresholdPx = (int)(10 * density); // 10dp threshold

            if (Math.Abs(m_accumulatedDy) > thresholdPx)
            {
                m_page.OnScrollDirectionDetected(isScrollingDown: m_accumulatedDy > 0);
                m_accumulatedDy = 0;
            }
        }
    }
}
