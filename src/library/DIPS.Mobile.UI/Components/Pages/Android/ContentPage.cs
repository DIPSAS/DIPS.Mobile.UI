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
    /// Enables scroll direction tracking by attaching a scroll listener to the
    /// platform view of the referenced <see cref="VisualElement"/>.
    /// Uses <see cref="RecyclerView.AddOnScrollListener"/> for RecyclerView-backed views
    /// (CollectionView) and <see cref="ViewTreeObserver.IOnScrollChangedListener"/> for
    /// all others (ScrollView, etc.).
    /// </summary>
    private partial void EnableScrollTrackingForView(VisualElement view)
    {
        DisableScrollTracking();

        var platformView = view.Handler?.PlatformView as AView;
        if (platformView is null)
            return;

        m_scrollTrackingTarget = platformView;

        if (platformView is RecyclerView rv)
        {
            var tracker = new RecyclerViewScrollTracker(this);
            m_scrollTracker = tracker;
            rv.AddOnScrollListener(tracker);
        }
        else
        {
            var tracker = new ViewScrollTracker(this, platformView);
            m_scrollTracker = tracker;
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
    /// Tracks scroll direction for views that expose <see cref="AView.ScrollY"/>
    /// (e.g., NestedScrollView / ScrollView) via <see cref="ViewTreeObserver.IOnScrollChangedListener"/>.
    /// This listener is additive and does not interfere with MAUI's internal scroll handling.
    /// </summary>
    private class ViewScrollTracker : Java.Lang.Object, ViewTreeObserver.IOnScrollChangedListener
    {
        private readonly ContentPage m_page;
        private readonly AView m_view;
        private int m_lastScrollY;

        // Required by Android runtime for Java peer re-creation
        public ViewScrollTracker(nint handle, Android.Runtime.JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public ViewScrollTracker(ContentPage page, AView view)
        {
            m_page = page;
            m_view = view;
            m_lastScrollY = view.ScrollY;
        }

        public void OnScrollChanged()
        {
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
    /// Tracks scroll direction for <see cref="RecyclerView"/>-backed views (CollectionView)
    /// using <see cref="RecyclerView.AddOnScrollListener"/> which is additive (multiple listeners allowed).
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
