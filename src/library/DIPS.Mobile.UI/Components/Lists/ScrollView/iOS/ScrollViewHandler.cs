using UIKit;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class ScrollViewHandler
{
    private static partial void MapShouldBounce(ScrollViewHandler handler,
        Microsoft.Maui.Controls.ScrollView virtualView)
    {
        if (handler.PlatformView is { } uiScrollView)
        {
            if (virtualView is ScrollView scrollView)
            {
                uiScrollView.Bounces = scrollView.ShouldBounce;
            }
        }
    }
}