using Android.Views;
using AndroidX.RecyclerView.Widget;
using DIPS.Mobile.UI.Extensions.Android;

namespace DIPS.Mobile.UI.Components.Lists;

internal class KeyboardDismissOnScrollListener : RecyclerView.OnScrollListener, RecyclerView.IOnItemTouchListener
{
    private bool m_hasHiddenKeyboard;
    private bool m_userHasDragged;

    public override void OnScrollStateChanged(RecyclerView recyclerView, int newState)
    {
        base.OnScrollStateChanged(recyclerView, newState);

        if (newState == RecyclerView.ScrollStateIdle)
        {
            m_userHasDragged = false;
            m_hasHiddenKeyboard = false;
        }
    }

    public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
    {
        base.OnScrolled(recyclerView, dx, dy);

        if (!m_userHasDragged || recyclerView.ScrollState != RecyclerView.ScrollStateDragging || m_hasHiddenKeyboard || dy == 0)
            return;

        m_hasHiddenKeyboard = true;

        KeyboardHelper.HideKeyboardAndClearFocus(recyclerView);
    }

    public bool OnInterceptTouchEvent(RecyclerView recyclerView, MotionEvent motionEvent)
    {
        UpdateUserDragState(recyclerView, motionEvent);
        return false;
    }

    public void OnTouchEvent(RecyclerView recyclerView, MotionEvent motionEvent)
    {
        UpdateUserDragState(recyclerView, motionEvent);
    }

    public void OnRequestDisallowInterceptTouchEvent(bool disallowIntercept)
    {
    }

    private void UpdateUserDragState(RecyclerView recyclerView, MotionEvent motionEvent)
    {
        switch (motionEvent.Action)
        {
            case MotionEventActions.Down:
                m_userHasDragged = false;
                m_hasHiddenKeyboard = false;
                break;
            case MotionEventActions.Move:
                m_userHasDragged = true;
                break;
            case MotionEventActions.Up:
            case MotionEventActions.Cancel:
                if (recyclerView.ScrollState == RecyclerView.ScrollStateIdle)
                {
                    m_userHasDragged = false;
                    m_hasHiddenKeyboard = false;
                }
                break;
        }
    }
}
