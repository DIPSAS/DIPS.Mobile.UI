using Android.Views.InputMethods;
using AndroidX.RecyclerView.Widget;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Lists;

internal class KeyboardDismissOnScrollListener : RecyclerView.OnScrollListener
{
    public override void OnScrollStateChanged(RecyclerView recyclerView, int newState)
    {
        base.OnScrollStateChanged(recyclerView, newState);

        if (newState != RecyclerView.ScrollStateDragging)
            return;

        var context = recyclerView.Context;
        if (context == null)
            return;

        var imm = (InputMethodManager?)context.GetSystemService(global::Android.Content.Context.InputMethodService);
        imm?.HideSoftInputFromWindow(recyclerView.WindowToken, HideSoftInputFlags.None);
    }
}
