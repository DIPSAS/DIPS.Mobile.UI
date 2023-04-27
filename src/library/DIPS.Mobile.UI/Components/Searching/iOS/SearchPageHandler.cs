using Microsoft.Maui.Handlers;
using UIKit;
using ContentView = Microsoft.Maui.Platform.ContentView;

namespace DIPS.Mobile.UI.Components.Searching.iOS;

public class SearchPageHandler : PageHandler
{
    protected override void ConnectHandler(ContentView platformView)
    {
        base.ConnectHandler(platformView);
        
        if (ViewController != null)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
               
            }
        }
    }
}