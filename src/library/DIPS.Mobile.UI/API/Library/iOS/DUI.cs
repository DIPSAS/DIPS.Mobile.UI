using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.API.Library;

public static partial class DUI
{

    public static Action? OnRemoveViewsLocatedOnTopOfPage;
    
    private static partial void RemovePlatformSpecificViewsLocatedOnTopOfPage()
    {
        OnRemoveViewsLocatedOnTopOfPage?.Invoke();
    }

}