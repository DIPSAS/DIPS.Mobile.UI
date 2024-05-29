using Android.Widget;
using Google.Android.Material.AppBar;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pages.Android;

public class ContentPageHandler : PageHandler
{
    /// <summary>
    /// Magic taken from: https://stackoverflow.com/questions/75596420/how-do-i-add-a-listener-to-the-android-toolbar-in-maui/76056039#76056039
    /// Sets the toolbar item icon's tint color
    /// </summary>
    protected override void ConnectHandler(ContentViewGroup platformView)
    {
        base.ConnectHandler(platformView);
        
        // If the ContentPage is a regular page, the linearlayout will be null
        var linearLayout = Platform.CurrentActivity?.FindViewById<LinearLayout>(_Microsoft.Android.Resource.Designer.Resource.Id.navigationlayout_appbar);
        
        var child1 = linearLayout?.GetChildAt(0);

        if (child1 is not MaterialToolbar materialToolbar)
            return;
        
        var stateListColor = Colors.GetColor(Shell.Shell.ToolbarTitleTextColorName)
            .ToDefaultColorStateList();
                
        for (var i = 0; i < materialToolbar.Menu?.Size(); i++)
        {
            materialToolbar.Menu.GetItem(i)?.SetIconTintList(stateListColor);
        }
    }
}