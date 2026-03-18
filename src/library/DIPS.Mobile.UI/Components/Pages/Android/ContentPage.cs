using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Pages;

public partial class ContentPage
{
    private AView? m_toolbarPlatformView;

    private partial void AttachBottomToolbarOnPlatform()
    {
        try
        {
            if (BottomToolbar is null || Handler?.MauiContext is not { } mauiContext)
                return;

            // Remove existing if re-attaching
            DetachBottomToolbarOnPlatform();

            m_toolbarPlatformView = BottomToolbar.ToPlatform(mauiContext);
            Console.WriteLine($"[Toolbar] ToPlatform done, parent={m_toolbarPlatformView?.Parent?.GetType().Name}");

            var pageView = Handler.PlatformView as AView;
            if (pageView is null)
                return;

            // The page view may not be attached to the window yet when OnHandlerChanged fires.
            // Wait for it to be attached, then add the toolbar to its parent container.
            if (!pageView.IsAttachedToWindow)
            {
                Console.WriteLine("[Toolbar] Page not attached yet, waiting...");
                pageView.ViewAttachedToWindow += OnPageViewAttachedToWindow;
            }
            else
            {
                Console.WriteLine("[Toolbar] Page already attached, adding now");
                AddToolbarToPageParent(pageView);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Toolbar] CRASH in AttachBottomToolbarOnPlatform: {ex}");
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
        try
        {
            if (m_toolbarPlatformView is null)
                return;

            // Remove from any existing parent first — ToPlatform() may have attached it internally
            Console.WriteLine($"[Toolbar] Before remove: parent={m_toolbarPlatformView.Parent?.GetType().Name}");
            (m_toolbarPlatformView.Parent as ViewGroup)?.RemoveView(m_toolbarPlatformView);

            // Walk up from the page view to find the nearest FrameLayout we can overlay into
            var container = FindFrameLayoutParent(pageView);
            Console.WriteLine($"[Toolbar] FindFrameLayoutParent returned: {container?.GetType().Name}");
            if (container is null)
            {
                // Fallback to Activity root
                container = Platform.CurrentActivity?.FindViewById<FrameLayout>(Android.Resource.Id.Content);
                Console.WriteLine($"[Toolbar] Fallback container: {container?.GetType().Name}");
            }

            if (container is null)
                return;

            var density = Platform.AppContext.Resources!.DisplayMetrics!.Density;
            var margin16 = (int)(16 * density);

            // Account for the system navigation bar (gesture indicator) at the bottom
            var navBarHeight = 0;
            var resourceId = Platform.AppContext.Resources.GetIdentifier("navigation_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                navBarHeight = Platform.AppContext.Resources.GetDimensionPixelSize(resourceId);
            }

            var lp = new FrameLayout.LayoutParams(
                ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent,
                GravityFlags.Bottom);
            lp.SetMargins(margin16, 0, margin16, margin16 + navBarHeight);

            Console.WriteLine($"[Toolbar] About to AddView to {container.GetType().Name}, childCount={container.ChildCount}");
            container.AddView(m_toolbarPlatformView, lp);
            Console.WriteLine($"[Toolbar] AddView succeeded, container childCount={container.ChildCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Toolbar] CRASH in AddToolbarToPageParent: {ex}");
        }
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
}
