using Android.Views;
using Android.Views.InputMethods;
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

        var context = v.Context;
        if (context == null)
            return;

        var imm = context.GetSystemService(global::Android.Content.Context.InputMethodService) as InputMethodManager;
        imm?.HideSoftInputFromWindow(v.WindowToken, HideSoftInputFlags.None);
        
        // Clear focus from the currently focused view (e.g. SearchBar)
        if (v is ViewGroup viewGroup)
        {
            var previousFocusability = viewGroup.DescendantFocusability;
            viewGroup.DescendantFocusability = DescendantFocusability.BlockDescendants;
            viewGroup.FindFocus()?.ClearFocus();
            viewGroup.DescendantFocusability = previousFocusability;
        }
    }
}
