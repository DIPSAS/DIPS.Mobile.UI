using System.Runtime.CompilerServices;
using Android.Content;
using Android.Views.InputMethods;
using DIPS.Mobile.UI.Components.BottomSheets;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;
using IAViewParent = Android.Views.IViewParent;

namespace DIPS.Mobile.UI.API.Library.Android;

/// <summary>
/// Taken from: https://supportcenter.devexpress.com/ticket/details/t1208656/adding-more-information-on-contentpage-hidesoftinputontapped#c43630fa-4759-4fb3-bc13-593024a70426
/// </summary>
internal static class HideSoftInputOnTapHandlerMappings
{
   private static readonly ConditionalWeakTable<IAViewParent, object> s_activePages = new();

    public static bool RequiresKeyboardDismissal(AView? nativeView)
    {
        var parent = nativeView?.Parent;
        while (parent != null)
        {
            if (s_activePages.TryGetValue(parent, out _))
            {
                return true;
            }
            parent = parent.Parent;
        }
        return false;
    }

    public static void MapHideSoftInputOnTapped(IPageHandler handler, ContentPage page)
    {
        var view = handler.PlatformView;
        
        // Checking ExperimentalFeatures.DictationInTextFields is to make the dictation toggle button in textfields not
        // unfocus the input view. Should be removed and a better solution should be implemented once used in production.
        if (page.HideSoftInputOnTapped && !DUI.IsExperimentalFeatureEnabled(DUI.ExperimentalFeatures.DictationInTextFields))
        {
            s_activePages.TryAdd(view, true);
            page.NavigatedTo += PageNavigatedTo;
            page.NavigatedFrom += PageNavigatedFrom;
        }
        else
        {
            s_activePages.Remove(view);
            page.NavigatedTo -= PageNavigatedTo;
            page.NavigatedFrom -= PageNavigatedFrom;
        }
    }

    private static void PageNavigatedFrom(object? sender, NavigatedFromEventArgs e)
    {
        var page = (ContentPage)sender;

        if (page.Handler != null)
        {
            var nativePage = (ContentViewGroup)page.Handler.PlatformView!;
            s_activePages.Remove(nativePage);
            DismissKeyboard(nativePage);
        }
    }

    private static void PageNavigatedTo(object? sender, NavigatedToEventArgs e)
    {
        var page = (ContentPage)sender;

        if (page.Handler != null)
        {
            var nativePage = (ContentViewGroup)page.Handler.PlatformView!;
            s_activePages.TryAdd(nativePage, true);
            DismissKeyboard(nativePage);
        }
    }

    public static void DismissKeyboard(AView? view)
    {
        var inputMethodManager = view?.Context?.GetSystemService(Context.InputMethodService) as InputMethodManager;
        inputMethodManager?.HideSoftInputFromWindow(view?.WindowToken, 0);
    }

    public static void MapInputIsFocused(IViewHandler handler, IView view)
    {
        // Ignore
    }
}