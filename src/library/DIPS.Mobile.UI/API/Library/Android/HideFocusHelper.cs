using AView = Android.Views.View;

namespace DIPS.Mobile.UI.API.Library.Android;

internal static class HideFocusHelper
{
    internal static void HideFocus(AView? view, AView? currentFocus = null)
    {
        if (HideSoftInputOnTapHandlerMappings.RequiresKeyboardDismissal(view))
        {
            Task.Run(async () =>
            {
                // Give it a main thread cycle to update the focus
                await Task.Yield();

                var focusChanged = currentFocus != view && currentFocus != null;
                if (!focusChanged)
                {
                    // focus stayed on the same item, even if user tapped away
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        // Dismiss keyboard and remove focus
                        HideSoftInputOnTapHandlerMappings.DismissKeyboard(view);
                        view.ClearFocus();
                    });
                }
            });
        }
    }
}