using DIPS.Mobile.UI.Resources.Sizes;

namespace Components.ResourcesSamples.Sizes;

public partial class SizesAsFontSize
{
    public SizesAsFontSize()
    {
        InitializeComponent();
    }
    
    public List<SizeResource> Items { get; } = SizeResources.Sizes.Where(pair => pair.Value > 0)
        .Select(pair => new SizeResource(pair.Key, pair.Value)).ToList();
}

public class SizeResource
{
    public SizeResource(string key, double value)
    {
        Key = key;
        Value = value;
    }
        
    public string Key { get; }
    public double Value { get; }
}
