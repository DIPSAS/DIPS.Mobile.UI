using Android.Views;
using DIPS.Mobile.UI.Extensions.Android;
using AView = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Lists;

internal class ScrollViewKeyboardDismissOnScrollListener : Java.Lang.Object, AView.IOnTouchListener, AView.IOnScrollChangeListener
{
    private bool m_isUserTouching;
    private bool m_hasHiddenKeyboard;

    public bool OnTouch(AView? v, MotionEvent? e)
    {
        if (e != null)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                case MotionEventActions.Move:
                    m_isUserTouching = true;
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
