using Microsoft.Maui.Handlers;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.SegmentedControl;

public partial class SegmentedControlHandler : ViewHandler<SegmentedControl, UISegmentedControl>
{
    public static partial void MapSegments(SegmentedControlHandler handler, SegmentedControl segmentedControl)
    {
        
    }

    protected override UISegmentedControl CreatePlatformView()
    {
        return new UISegmentedControl();
    }
}