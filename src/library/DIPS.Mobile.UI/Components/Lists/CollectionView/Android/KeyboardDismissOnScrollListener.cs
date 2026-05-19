using Android.Views;
using Android.Views.InputMethods;
using AndroidX.RecyclerView.Widget;

namespace DIPS.Mobile.UI.Components.Lists;

internal class KeyboardDismissOnScrollListener : RecyclerView.OnScrollListener, RecyclerView.IOnItemTouchListener
{
    private bool m_isUserTouching;
    private bool m_hasHiddenKeyboard;

    public bool OnInterceptTouchEvent(RecyclerView rv, MotionEvent e)
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

        return false;
    }

    public void OnTouchEvent(RecyclerView rv, MotionEvent e)
    {
    }

    public void OnRequestDisallowInterceptTouchEvent(bool disallowIntercept)
    {
    }

    public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
    {
        base.OnScrolled(recyclerView, dx, dy);

        if (!m_isUserTouching || m_hasHiddenKeyboard || dy == 0)
            return;

        m_hasHiddenKeyboard = true;

        var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
        var focusedView = activity?.CurrentFocus;
        if (focusedView == null)
            return;

        var imm = activity!.GetSystemService(global::Android.Content.Context.InputMethodService) as InputMethodManager;
        imm?.HideSoftInputFromWindow(focusedView.WindowToken, HideSoftInputFlags.None);
        focusedView.ClearFocus();
    }
}
