using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.SegmentedControl;

public class ProSegmentedControl : View
{
    public static readonly BindableProperty SegmentsProperty = BindableProperty.Create(
        nameof(Segments),
        typeof(IEnumerable<string>),
        typeof(ProSegmentedControl));

    public IEnumerable<string> Segments
    {
        get => (IEnumerable<string>)GetValue(SegmentsProperty);
        set => SetValue(SegmentsProperty, value);
    }


    public event EventHandler<SegmentChangedArgs>? SegmentChanged;

    public static readonly BindableProperty SegmentChangedCommandProperty = BindableProperty.Create(
        nameof(SegmentChangedCommand),
        typeof(ICommand),
        typeof(ProSegmentedControl));

    public ICommand SegmentChangedCommand
    {
        get => (ICommand)GetValue(SegmentChangedCommandProperty);
        set => SetValue(SegmentChangedCommandProperty, value);
    }

    public void RaiseSegmentChanged(int segmentIndex)
    {
        SegmentChanged?.Invoke(this, new SegmentChangedArgs(segmentIndex, Segments.ToList()[segmentIndex]));
        SegmentChangedCommand?.Execute(segmentIndex);
    }
}

public class SegmentChangedArgs
{
    public int Index { get; }
    public string Segment { get; }

    public SegmentChangedArgs(int index, string segment)
    {
        Index = index;
        Segment = segment;
    }
}