using Microsoft.Maui.Handlers;
using UIKit;

namespace DIPS.Mobile.UI.API.Library.iOS;

/// <summary>
/// Taken from: https://supportcenter.devexpress.com/ticket/details/t1208656/adding-more-information-on-contentpage-hidesoftinputontapped#c43630fa-4759-4fb3-bc13-593024a70426
/// </summary>
internal static class HideSoftInputOnTapHandlerMappings
{
    public static void MapHideSoftInputOnTapped(IPageHandler handler, ContentPage page)
    {
        UIView view = handler.PlatformView;

        var existingDismissKeyboardTapGestureRecognizer = view.GestureRecognizers?.OfType<DismissKeyboardTapGestureRecognizer>().FirstOrDefault();

        // Temporary change to make the dictation toggle button in textfields not unfocus the input view. Should be removed and 
        // a better solution should be implemented once used in production.
        if (DUI.IsExperimentalFeatureEnabled(DUI.ExperimentalFeatures.DictationInTextFields))
        {
            if (existingDismissKeyboardTapGestureRecognizer is null) return;
            
            view.RemoveGestureRecognizer(existingDismissKeyboardTapGestureRecognizer);

            return;
        };
        
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