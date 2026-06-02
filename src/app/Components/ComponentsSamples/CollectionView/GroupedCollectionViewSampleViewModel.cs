using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.CollectionView;

public class ItemGroup : ObservableCollection<string>
{
    public ItemGroup(string name, List<string> items) : base(items)
    {
        Name = name;
    }

    public string Name { get; }
}

public class GroupedCollectionViewSampleViewModel : ViewModel
{
    private int m_sectionCounter;
    private int m_itemCounter;

    private bool m_autoCornerRadius = true;
    private bool m_autoHideLastDivider = true;

    public GroupedCollectionViewSampleViewModel()
    {
        AddSectionCommand = new Command(() =>
        {
            m_sectionCounter++;
            Groups.Add(new ItemGroup($"Section {m_sectionCounter}", [$"Item {++m_itemCounter}", $"Item {++m_itemCounter}", $"Item {++m_itemCounter}"]));
        });

        RemoveFirstSectionCommand = new Command(() =>
        {
            if (Groups.Count > 0)
                Groups.RemoveAt(0);
        });

        RemoveLastSectionCommand = new Command(() =>
        {
            if (Groups.Count > 0)
                Groups.RemoveAt(Groups.Count - 1);
        });

        AddItemToFirstSectionStartCommand = new Command(() =>
        {
            if (Groups.Count > 0)
                Groups[0].Insert(0, $"Item {++m_itemCounter} (start)");
        });

        AddItemToFirstSectionMiddleCommand = new Command(() =>
        {
            if (Groups.Count > 0)
            {
                var middle = Groups[0].Count / 2;
                Groups[0].Insert(middle, $"Item {++m_itemCounter} (middle)");
            }
        });

        AddItemToFirstSectionEndCommand = new Command(() =>
        {
            if (Groups.Count > 0)
                Groups[0].Add($"Item {++m_itemCounter} (end)");
        });

        RemoveItemFromFirstSectionCommand = new Command(() =>
        {
            if (Groups.Count > 0 && Groups[0].Count > 0)
                Groups[0].RemoveAt(Groups[0].Count - 1);
        });

        MinimizeFirstSectionCommand = new Command(() =>
        {
            if (Groups.Count > 0)
            {
                while (Groups[0].Count > 1)
                    Groups[0].RemoveAt(Groups[0].Count - 1);
            }
        });

        ExpandFirstSectionCommand = new Command(() =>
        {
            if (Groups.Count == 0)
                AddSectionCommand.Execute(null);

            while (Groups[0].Count < 4)
                Groups[0].Add($"Item {++m_itemCounter} (expanded)");
        });

        ResetCommand = new Command(Reset);

        Reset();
    }

    public ObservableCollection<ItemGroup> Groups { get; } = [];

    public bool AutoCornerRadius
    {
        get => m_autoCornerRadius;
        set => RaiseWhenSet(ref m_autoCornerRadius, value);
    }

    public bool AutoHideLastDivider
    {
        get => m_autoHideLastDivider;
        set => RaiseWhenSet(ref m_autoHideLastDivider, value);
    }

    public ICommand AddSectionCommand { get; }
    public ICommand RemoveFirstSectionCommand { get; }
    public ICommand RemoveLastSectionCommand { get; }
    public ICommand AddItemToFirstSectionStartCommand { get; }
    public ICommand AddItemToFirstSectionMiddleCommand { get; }
    public ICommand AddItemToFirstSectionEndCommand { get; }
    public ICommand RemoveItemFromFirstSectionCommand { get; }
    public ICommand MinimizeFirstSectionCommand { get; }
    public ICommand ExpandFirstSectionCommand { get; }
    public ICommand ResetCommand { get; }

    private void Reset()
    {
        Groups.Clear();
        m_sectionCounter = 0;
        m_itemCounter = 0;

        Groups.Add(new ItemGroup("Section 1", [$"Item {++m_itemCounter}", $"Item {++m_itemCounter}", $"Item {++m_itemCounter}", $"Item {++m_itemCounter}"]));
        Groups.Add(new ItemGroup("Section 2", [$"Item {++m_itemCounter}", $"Item {++m_itemCounter}", $"Item {++m_itemCounter}"]));
        Groups.Add(new ItemGroup("Section 3", [$"Item {++m_itemCounter}", $"Item {++m_itemCounter}"]));
        m_sectionCounter = 3;
    }
}
