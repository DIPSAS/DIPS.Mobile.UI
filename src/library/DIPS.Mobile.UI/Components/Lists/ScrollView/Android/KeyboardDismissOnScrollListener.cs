using Android.Views;
using DIPS.Mobile.UI.Extensions.Android;
using AView = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Lists;

internal class ScrollViewKeyboardDismissOnScrollListener : Java.Lang.Object, AView.IOnTouchListener, AView.IOnScrollChangeListener
{
    private bool m_isUserTouching;
    private bool m_hasHiddenKeyboard;
    private float m_downY;

    public bool OnTouch(AView? v, MotionEvent? e)
    {
        if (e != null)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    m_isUserTouching = true;
                    m_downY = e.RawY;
                    break;
                case MotionEventActions.Move:
                    m_isUserTouching = true;
                    if (!m_hasHiddenKeyboard && v != null)
                    {
                        var distance = Math.Abs(e.RawY - m_downY);
                        var touchSlop = ViewConfiguration.Get(v.Context)?.ScaledTouchSlop ?? 8;
                        if (distance > touchSlop)
                        {
                            m_hasHiddenKeyboard = true;
                            KeyboardHelper.HideKeyboardAndClearFocus(v);
                        }
                    }
                    break;
                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                    m_isUserTouching = false;
                    m_hasHiddenKeyboard = false;
                    break;
            }
        }

        return false; // Don't consume the event
    }

    public void OnScrollChange(AView v, int scrollX, int scrollY, int oldScrollX, int oldScrollY)
    {
        if (!m_isUserTouching || m_hasHiddenKeyboard)
            return;

        m_hasHiddenKeyboard = true;

        KeyboardHelper.HideKeyboardAndClearFocus(v);
    }
}
