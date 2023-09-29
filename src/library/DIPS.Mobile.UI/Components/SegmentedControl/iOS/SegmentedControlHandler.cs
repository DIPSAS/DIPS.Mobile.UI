using Microsoft.Maui.Handlers;
using UIKit;

namespace DIPS.Mobile.UI.Components.SegmentedControl.iOS;

public class SegmentedControlHandler : ViewHandler<ProSegmentedControl, UISegmentedControl>
{
    public SegmentedControlHandler() : base(PropertyMapper)
    {
    }

    public static IPropertyMapper<ProSegmentedControl, SegmentedControlHandler> PropertyMapper =
        new PropertyMapper<ProSegmentedControl, SegmentedControlHandler>()
        {
            [nameof(ProSegmentedControl.Segments)] = MapSegments
        };

    private static void MapSegments(SegmentedControlHandler handler, ProSegmentedControl proSegmentedControl)
    {
        var listOfSegments = proSegmentedControl.Segments.ToList();
        foreach (var segmentTitle in listOfSegments)
        {
            handler.PlatformView.InsertSegment(segmentTitle, listOfSegments.IndexOf(segmentTitle), true);
        }
    }

    protected override UISegmentedControl CreatePlatformView() => new UISegmentedControl();

    protected override void ConnectHandler(UISegmentedControl platformView)
    {
        base.ConnectHandler(platformView);
        PlatformView.ValueChanged += OnValueChanged;
    }

    private void OnValueChanged(object? sender, EventArgs e)
    {
        VirtualView.RaiseSegmentChanged((int)PlatformView.SelectedSegment);
    }

    protected override void DisconnectHandler(UISegmentedControl platformView)
    {
        base.DisconnectHandler(platformView);
        PlatformView.ValueChanged -= OnValueChanged;
    }
}