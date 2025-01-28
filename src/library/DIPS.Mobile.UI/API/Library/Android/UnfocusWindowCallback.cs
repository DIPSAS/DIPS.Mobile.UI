using Android.OS;
using Android.Views;
using Android.Views.Accessibility;
using View = Android.Views.View;
using Window = Android.Views.Window;

namespace DIPS.Mobile.UI.API.Library.Android;

internal class UnfocusWindowCallback : Java.Lang.Object, Window.ICallback
{
    private readonly Window m_window;
    private readonly Window.ICallback m_originalCallback;
    private long m_touchDownTime;

    public UnfocusWindowCallback(Window window, Window.ICallback originalCallback)
    {
        m_window = window;
        m_originalCallback = originalCallback;
    }

    public bool DispatchGenericMotionEvent(MotionEvent? e)
    {
        return m_originalCallback.DispatchGenericMotionEvent(e);
    }

    public bool DispatchKeyEvent(KeyEvent? e)
    {
        return m_originalCallback.DispatchKeyEvent(e);
    }

    public bool DispatchKeyShortcutEvent(KeyEvent? e)
    {
        return m_originalCallback.DispatchKeyShortcutEvent(e);
    }

    public bool DispatchPopulateAccessibilityEvent(AccessibilityEvent? e)
    {
        return m_originalCallback.DispatchPopulateAccessibilityEvent(e);
    }

    public bool DispatchTouchEvent(MotionEvent? e) => e?.Action switch
    {
        MotionEventActions.Down => DispatchMotionDown(e),
        MotionEventActions.Up => DispatchMotionUp(e),
        _ => m_originalCallback.DispatchTouchEvent(e)
    };

    private bool DispatchMotionDown(MotionEvent motionEvent)
    {
        m_touchDownTime = SystemClock.ElapsedRealtime();
        return m_originalCallback.DispatchTouchEvent(motionEvent);
    }

    private bool DispatchMotionUp(MotionEvent motionEvent)
    {
        var focused = m_window.CurrentFocus;
        var dispatchTouchEventResult = m_originalCallback.DispatchTouchEvent(motionEvent);

        // if not focused, or dispatch is canceled
        if (focused is null || !dispatchTouchEventResult || SystemClock.ElapsedRealtime() - m_touchDownTime > 150)
        {
            // just return
            return dispatchTouchEventResult;
        }

        var touchCoordinates = new int[2];
        focused.GetLocationOnScreen(touchCoordinates);
        var x = motionEvent.RawX + focused.Left - touchCoordinates[0];
        var y = motionEvent.RawY + focused.Top - touchCoordinates[1];

        //If the touch position is outside the element then we may want to hide the keyboard and remove focus from the element
        var touchedOutsideFocusedElement = x < focused.Left || x > focused.Right || y < focused.Top || y > focused.Bottom;
        if (touchedOutsideFocusedElement)
        {
            HideFocusHelper.HideFocus(focused, m_window.CurrentFocus);
        }

        return true;
    }
    

    public bool DispatchTrackballEvent(MotionEvent? e)
    {
        return m_originalCallback.DispatchTrackballEvent(e);
    }

    public void OnActionModeFinished(ActionMode? mode)
    {
        m_originalCallback.OnActionModeFinished(mode);
    }

    public void OnActionModeStarted(ActionMode? mode)
    {
        m_originalCallback.OnActionModeStarted(mode);
    }

    public void OnAttachedToWindow()
    {
        m_originalCallback.OnAttachedToWindow();
    }

    public void OnContentChanged()
    {
        m_originalCallback.OnContentChanged();
    }

    public bool OnCreatePanelMenu(int featureId, IMenu? menu)
    {
        return m_originalCallback.OnCreatePanelMenu(featureId, menu);
    }

    public View? OnCreatePanelView(int featureId)
    {
        return m_originalCallback.OnCreatePanelView(featureId);
    }

    public void OnDetachedFromWindow()
    {
        m_originalCallback.OnDetachedFromWindow();
    }

    public bool OnMenuItemSelected(int featureId, IMenuItem item)
    {
        return m_originalCallback.OnMenuItemSelected(featureId, item);
    }

    public bool OnMenuOpened(int featureId, IMenu menu)
    {
        return m_originalCallback.OnMenuOpened(featureId, menu);
    }

    public void OnPanelClosed(int featureId, IMenu? menu)
    {
        m_originalCallback.OnPanelClosed(featureId, menu);
    }

    public bool OnPreparePanel(int featureId, View? view, IMenu menu)
    {
        return m_originalCallback.OnPreparePanel(featureId, view, menu);
    }

    public bool OnSearchRequested()
    {
        return m_originalCallback.OnSearchRequested();
    }

    public bool OnSearchRequested(SearchEvent? searchEvent)
    {
        return m_originalCallback.OnSearchRequested(searchEvent);
    }

    public void OnWindowAttributesChanged(WindowManagerLayoutParams? attrs)
    {
        m_originalCallback.OnWindowAttributesChanged(attrs);
    }

    public void OnWindowFocusChanged(bool hasFocus)
    {
        m_originalCallback.OnWindowFocusChanged(hasFocus);
    }

    public ActionMode? OnWindowStartingActionMode(ActionMode.ICallback? callback, ActionModeType type)
    {
        return m_originalCallback.OnWindowStartingActionMode(callback, type);
    }

    public ActionMode? OnWindowStartingActionMode(ActionMode.ICallback? callback)
    {
        return m_originalCallback.OnWindowStartingActionMode(callback);
    }
}