using DIPS.Mobile.UI.Components.VirtualListView.Adapters;
using DIPS.Mobile.UI.Resources.Sizes;

namespace Components.ResourcesSamples.Sizes;

public partial class SizesAsFontSize
{
    public SizesAsFontSize()
    {
        InitializeComponent();
    }
    
    public IVirtualListViewAdapter Adapter { get; } = new VirtualListViewAdapter<SizeResource>(SizeResources.Sizes.Where(pair => pair.Value > 0)
        .Select(pair => new SizeResource(pair.Key, pair.Value)).ToList());

    private void VirtualListView_OnOnScrolled(object? sender, ScrolledEventArgs e)
    {
    }
}

internal class SizeResource
{
    public SizeResource(string key, int value)
    {
        Key = key;
        Value = value;
    }
        
    public string Key { get; }
    public int Value { get; }
}
