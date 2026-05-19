using Android.Views.InputMethods;
using AndroidX.RecyclerView.Widget;

namespace DIPS.Mobile.UI.Components.Lists;

internal class KeyboardDismissOnScrollListener : RecyclerView.OnScrollListener
{
    private bool m_hasHiddenKeyboard;

    public override void OnScrollStateChanged(RecyclerView recyclerView, int newState)
    {
        base.OnScrollStateChanged(recyclerView, newState);

        if (newState == RecyclerView.ScrollStateIdle)
            m_hasHiddenKeyboard = false;
    }

    public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
    {
        base.OnScrolled(recyclerView, dx, dy);

        if (recyclerView.ScrollState == RecyclerView.ScrollStateIdle || m_hasHiddenKeyboard || dy == 0)
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
