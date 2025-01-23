using Microsoft.Maui.Handlers;
using UIKit;

namespace DIPS.Mobile.UI.API.Library.iOS;

internal static class HideSoftInputOnTapHandlerMappings
{
    public static void MapHideSoftInputOnTapped(IPageHandler handler, ContentPage page)
    {
        UIView view = handler.PlatformView;

        var existingDismissKeyboardTapGestureRecognizer = view.GestureRecognizers?.OfType<DismissKeyboardTapGestureRecognizer>().FirstOrDefault();
        if (page.HideSoftInputOnTapped)
        {
            if (existingDismissKeyboardTapGestureRecognizer is null)
            {
                view.AddGestureRecognizer(new DismissKeyboardTapGestureRecognizer(() => view.EndEditing(true)));
            }
        }
        else
        {
            if (existingDismissKeyboardTapGestureRecognizer is not null)
            {
                view.RemoveGestureRecognizer(existingDismissKeyboardTapGestureRecognizer);
            }
        }
    }

    public static void MapInputIsFocused(IViewHandler handler, IView view)
    {
        // Ignore
    }

    private sealed class DismissKeyboardTapGestureRecognizer : UITapGestureRecognizer
    {
        public DismissKeyboardTapGestureRecognizer(Action action) : base(action)
        {
            CancelsTouchesInView = false;
        }
    }
}