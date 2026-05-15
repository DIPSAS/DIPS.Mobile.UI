using Android.Views;
using Android.Views.InputMethods;
using AndroidX.RecyclerView.Widget;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Lists;

internal class KeyboardDismissOnScrollListener : RecyclerView.OnScrollListener
{
    private bool m_hasHiddenKeyboard;

    public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
    {
        base.OnScrolled(recyclerView, dx, dy);

        if (m_hasHiddenKeyboard || dy == 0)
            return;

        // Only dismiss on user-initiated drag scrolls, not layout-induced scrolls
        // (e.g. keyboard appearing causes a re-layout that triggers OnScrolled with ScrollState still Idle)
        if (recyclerView.ScrollState != RecyclerView.ScrollStateDragging)
            return;

        m_hasHiddenKeyboard = true;

        var context = recyclerView.Context;
        if (context == null)
            return;

        var imm = context.GetSystemService(global::Android.Content.Context.InputMethodService) as InputMethodManager;
        imm?.HideSoftInputFromWindow(recyclerView.WindowToken, HideSoftInputFlags.None);
        
        // Clear focus from the currently focused view (e.g. SearchBar)
        // Temporarily block descendants from receiving focus so ClearFocus doesn't just re-focus another child
        var previousFocusability = recyclerView.DescendantFocusability;
        recyclerView.DescendantFocusability = DescendantFocusability.BlockDescendants;
        recyclerView.FindFocus()?.ClearFocus();
        recyclerView.DescendantFocusability = previousFocusability;
    }

    public override void OnScrollStateChanged(RecyclerView recyclerView, int newState)
    {
        base.OnScrollStateChanged(recyclerView, newState);

        if (newState == RecyclerView.ScrollStateIdle)
            m_hasHiddenKeyboard = false;
    }
}
