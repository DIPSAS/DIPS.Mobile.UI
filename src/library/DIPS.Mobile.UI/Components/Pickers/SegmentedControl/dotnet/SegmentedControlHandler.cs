using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Pickers.SegmentedControl;

public partial class SegmentedControlHandler : ViewHandler<SegmentedControl, Only_Here_For_UnitTests>
{
    public static partial void MapSegments(SegmentedControlHandler handler, SegmentedControl segmentedControl) => throw new Only_Here_For_UnitTests();

    protected override Only_Here_For_UnitTests CreatePlatformView() => new();
}