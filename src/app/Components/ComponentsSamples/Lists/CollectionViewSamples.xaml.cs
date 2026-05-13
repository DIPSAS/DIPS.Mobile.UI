using System.Collections.ObjectModel;

namespace Components.ComponentsSamples.Lists;

public partial class CollectionViewSamples
{
    public CollectionViewSamples()
    {
        Items = new ObservableCollection<string>(
            Enumerable.Range(1, 50).Select(i => $"Item {i}"));
        
        BindingContext = this;
        InitializeComponent();
    }

    public ObservableCollection<string> Items { get; }
}
