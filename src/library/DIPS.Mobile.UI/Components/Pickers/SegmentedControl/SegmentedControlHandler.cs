using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Pickers.SegmentedControl;

public partial class SegmentedControlHandler
{
    public static readonly IPropertyMapper<SegmentedControl, SegmentedControlHandler> SegmentedControlPropertyMapper = new PropertyMapper<SegmentedControl, SegmentedControlHandler>(ViewMapper)
    {
        [nameof(SegmentedControl.Segments)] = MapSegments
    };

    public static partial void MapSegments(SegmentedControlHandler handler, SegmentedControl segmentedControl);

    public SegmentedControlHandler() : base(SegmentedControlPropertyMapper)
    {
    }
}