using System.Collections.ObjectModel;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Loading.DelayedView;

public class CollectionViewTestPageViewModel : ViewModel
{
    private string m_navigationTimeMessage = string.Empty;

    public ObservableCollection<SampleItem> Items { get; } = new();

    public string NavigationTimeMessage
    {
        get => m_navigationTimeMessage;
        set => RaiseWhenSet(ref m_navigationTimeMessage, value);
    }

    public CollectionViewTestPageViewModel()
    {
        // Generate 50 sample items
        for (int i = 1; i <= 50; i++)
        {
            Items.Add(new SampleItem
            {
                Title = $"Item {i}",
                Subtitle = $"This is the subtitle for item {i}"
            });
        }
    }
}

public class SampleItem
{
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
}
