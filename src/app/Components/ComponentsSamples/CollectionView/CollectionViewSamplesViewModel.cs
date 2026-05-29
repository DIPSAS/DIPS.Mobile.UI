using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.CollectionView;

public class CollectionViewSamplesViewModel : ViewModel
{
    private int m_nextId;

    public ObservableCollection<CollectionViewSampleItem> Items { get; } = new();

    public ICommand AddFirstCommand { get; }
    public ICommand AddLastCommand { get; }
    public ICommand AddMiddleCommand { get; }
    public ICommand RemoveFirstCommand { get; }
    public ICommand RemoveLastCommand { get; }
    public ICommand RemoveMiddleCommand { get; }
    public ICommand ClearAllCommand { get; }
    public ICommand ResetCommand { get; }

    public CollectionViewSamplesViewModel()
    {
        AddFirstCommand = new Command(AddFirst);
        AddLastCommand = new Command(AddLast);
        AddMiddleCommand = new Command(AddMiddle);
        RemoveFirstCommand = new Command(RemoveFirst);
        RemoveLastCommand = new Command(RemoveLast);
        RemoveMiddleCommand = new Command(RemoveMiddle);
        ClearAllCommand = new Command(ClearAll);
        ResetCommand = new Command(Reset);

        Reset();
    }

    private void AddFirst()
    {
        m_nextId++;
        Items.Insert(0, new CollectionViewSampleItem($"New first #{m_nextId}"));
    }

    private void AddLast()
    {
        m_nextId++;
        Items.Add(new CollectionViewSampleItem($"New last #{m_nextId}"));
    }

    private void AddMiddle()
    {
        if (Items.Count == 0)
        {
            AddLast();
            return;
        }

        m_nextId++;
        var middleIndex = Items.Count / 2;
        Items.Insert(middleIndex, new CollectionViewSampleItem($"New middle #{m_nextId}"));
    }

    private void RemoveFirst()
    {
        if (Items.Count > 0)
            Items.RemoveAt(0);
    }

    private void RemoveLast()
    {
        if (Items.Count > 0)
            Items.RemoveAt(Items.Count - 1);
    }

    private void RemoveMiddle()
    {
        if (Items.Count > 0)
            Items.RemoveAt(Items.Count / 2);
    }

    private void ClearAll()
    {
        Items.Clear();
    }

    private void Reset()
    {
        Items.Clear();
        m_nextId = 5;
        Items.Add(new CollectionViewSampleItem("Item 1"));
        Items.Add(new CollectionViewSampleItem("Item 2"));
        Items.Add(new CollectionViewSampleItem("Item 3"));
        Items.Add(new CollectionViewSampleItem("Item 4"));
        Items.Add(new CollectionViewSampleItem("Item 5"));
    }
}

public class CollectionViewSampleItem(string title)
{
    public string Title { get; } = title;
}
