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
                if (handler.VirtualView.Orientation == ScrollOrientation.Vertical)
                {
                    uiScrollView.AlwaysBounceVertical = scrollView.ShouldBounce;
                }
                else if (handler.VirtualView.Orientation == ScrollOrientation.Horizontal)
                {
                    uiScrollView.AlwaysBounceHorizontal = scrollView.ShouldBounce;
                }
            }
        }
    }
}