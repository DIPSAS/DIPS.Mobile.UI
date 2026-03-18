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
}
