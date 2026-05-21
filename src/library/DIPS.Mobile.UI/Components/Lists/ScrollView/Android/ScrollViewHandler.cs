using Android.Views;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class ScrollViewHandler
{
    private ScrollViewKeyboardDismissOnScrollListener? m_keyboardDismissOnScrollListener;
    
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
            handler.m_keyboardDismissOnScrollListener = new ScrollViewKeyboardDismissOnScrollListener();
            handler.PlatformView.SetOnTouchListener(handler.m_keyboardDismissOnScrollListener);
            handler.PlatformView.SetOnScrollChangeListener(handler.m_keyboardDismissOnScrollListener);
        }
        else
        {
            handler.PlatformView.SetOnTouchListener(null);
            handler.PlatformView.SetOnScrollChangeListener((global::Android.Views.View.IOnScrollChangeListener?)null);
            handler.m_keyboardDismissOnScrollListener?.Dispose();
            handler.m_keyboardDismissOnScrollListener = null;
        }
    }

    protected override void DisconnectHandler(MauiScrollView platformView)
    {
        platformView.SetOnTouchListener(null);
        platformView.SetOnScrollChangeListener((global::Android.Views.View.IOnScrollChangeListener?)null);
        m_keyboardDismissOnScrollListener?.Dispose();
        m_keyboardDismissOnScrollListener = null;
    }
}