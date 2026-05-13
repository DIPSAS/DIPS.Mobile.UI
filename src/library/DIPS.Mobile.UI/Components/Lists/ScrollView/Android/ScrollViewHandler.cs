using Android.Views;
using Android.Views.InputMethods;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class ScrollViewHandler
{
    private static partial void MapShouldBounce(ScrollViewHandler handler,
        Microsoft.Maui.Controls.ScrollView virtualView)
    {
        if (virtualView is ScrollView scrollView)
        {
            handler.PlatformView.OverScrollMode = scrollView.ShouldBounce ? OverScrollMode.Always : OverScrollMode.Never;
        }
    }
    
    private static partial void MapRemoveFocusOnScroll(ScrollViewHandler handler,
        Microsoft.Maui.Controls.ScrollView virtualView)
    {
        if (virtualView is not ScrollView scrollView)
            return;

        if (scrollView.RemoveFocusOnScroll)
        {
            scrollView.Scrolled += OnScrollViewScrolledForKeyboardDismiss;
        }
        else
        {
            scrollView.Scrolled -= OnScrollViewScrolledForKeyboardDismiss;
        }
    }

    protected override void DisconnectHandler(MauiScrollView platformView)
    {
        if (VirtualView is ScrollView scrollView)
        {
            scrollView.Scrolled -= OnScrollViewScrolledForKeyboardDismiss;
        }
    }

    private static void OnScrollViewScrolledForKeyboardDismiss(object? sender, ScrolledEventArgs e)
    {
        if (sender is not ScrollView { Handler: ScrollViewHandler handler } scrollView)
            return;

        var context = handler.PlatformView.Context;
        if (context == null)
            return;

        var imm = (InputMethodManager?)context.GetSystemService(global::Android.Content.Context.InputMethodService);
        imm?.HideSoftInputFromWindow(handler.PlatformView.WindowToken, HideSoftInputFlags.None);
    }
}