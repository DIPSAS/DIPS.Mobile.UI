using Android.Widget;

namespace DIPS.Mobile.UI.Components.Searching.Android;

public class InternalSearchBar : Microsoft.Maui.Controls.SearchBar
{
    /// <summary>
    /// Removing underline
    /// </summary>
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if(Handler is not Microsoft.Maui.Handlers.SearchBarHandler searchBarHandler)
            return;
     
        var linearLayout = searchBarHandler.PlatformView.GetChildAt(0) as LinearLayout;  
        linearLayout = linearLayout?.GetChildAt(2) as LinearLayout;  
        linearLayout = linearLayout?.GetChildAt(1) as LinearLayout;  
        linearLayout!.Background = null; 
    }
}