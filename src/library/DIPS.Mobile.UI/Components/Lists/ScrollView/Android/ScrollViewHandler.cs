using Android.Views;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class ScrollViewHandler
{
    private static partial void MapShouldBounce(ScrollViewHandler handler,
        Microsoft.Maui.Controls.ScrollView virtualView)
    {
        if (virtualView is ScrollView scrollView)
        {
            handler.PlatformView.OverScrollMode = scrollView.ShouldBounce ? OverScrollMode.Always : OverScrollMode.Never;
        }
    }
}