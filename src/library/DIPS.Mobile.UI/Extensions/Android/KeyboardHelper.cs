using Android.Views;
using Android.Views.InputMethods;
using AView = Android.Views.View;

namespace DIPS.Mobile.UI.Extensions.Android;

/// <summary>
/// Android utilities for managing the soft keyboard.
/// </summary>
public static class KeyboardHelper
{
    /// <summary>
    /// Hides the soft keyboard and clears focus from the currently focused view.
    /// Does nothing if the keyboard is not currently visible.
    /// <para>
    /// Uses <see cref="InputMethodManager.IsAcceptingText"/> to detect keyboard visibility
    /// instead of <c>Activity.CurrentFocus</c>, which is often <c>null</c> in .NET MAUI.
    /// </para>
    /// <para>
    /// When the <paramref name="view"/> is a <see cref="ViewGroup"/> (e.g. RecyclerView, ScrollView),
    /// descendant focusability is temporarily blocked before clearing focus. This prevents Android
    /// from cycling focus back to the same input field.
    /// </para>
    /// </summary>
    /// <param name="view">
    /// The view whose window token is used to hide the keyboard.
    /// If the view is a <see cref="ViewGroup"/>, focus traversal is also scoped to this group
    /// to find and clear the focused child.
    /// </param>
    public static void HideKeyboardAndClearFocus(AView view)
    {
        var activity = Platform.CurrentActivity;
        if (activity == null)
            return;

        var imm = activity.GetSystemService(global::Android.Content.Context.InputMethodService) as InputMethodManager;
        if (imm is not { IsAcceptingText: true })
            return;

        imm.HideSoftInputFromWindow(view.WindowToken, HideSoftInputFlags.None);

        if (view is ViewGroup viewGroup)
        {
            // First try to find and clear focus among descendants (e.g. search bar in header)
            var focusedChild = viewGroup.FindFocus();
            if (focusedChild != null)
            {
                var previousFocusability = viewGroup.DescendantFocusability;
                viewGroup.DescendantFocusability = DescendantFocusability.BlockDescendants;
                focusedChild.ClearFocus();
                viewGroup.DescendantFocusability = previousFocusability;
                return;
            }
        }

        // Focused view is outside the provided view (or view is not a ViewGroup) — search from root
        activity.Window?.DecorView.FindFocus()?.ClearFocus();
    }
}
