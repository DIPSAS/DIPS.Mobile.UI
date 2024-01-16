using Android.Views;
using Object = Java.Lang.Object;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class ScrollViewHandler
{
    private static partial void MapShouldBounce(ScrollViewHandler handler,
        Microsoft.Maui.Controls.ScrollView virtualView)
    {
        if (virtualView is ScrollView scrollView)
        {
            handler.PlatformView.OverScrollMode = scrollView.ShouldBounce ? OverScrollMode.Always : OverScrollMode.Never;
            handler.PlatformView.SetOnTouchListener(new OnTouchListener(scrollView));
        }
    }
}

public class OnTouchListener : Object, View.IOnTouchListener
{
    private readonly ScrollView m_scrollView;

    public OnTouchListener(ScrollView scrollView)
    {
        m_scrollView = scrollView;
    }

    public bool OnTouch(View? v, MotionEvent? e)
    {
        return !m_scrollView.ScrollEnabled;
    }
}