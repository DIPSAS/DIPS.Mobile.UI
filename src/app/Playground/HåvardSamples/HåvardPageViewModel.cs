using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Playground.HåvardSamples;

public class HåvardPageViewModel : ViewModel
{
    private IEnumerable<object> m_selectedItems;
    private object m_selectedItem;
    private object m_selectedItem2;
    private List<Something> m_items;
    private List<Something> m_items2;

    public ICommand Command { get; }

    public HåvardPageViewModel()
    {
        Items = new List<Something>()
        {
            new("1", new SomeOtherThing(true)),
            new("2", new SomeOtherThing(false)),
            new("3", new SomeOtherThing(true)),
            new("4", new SomeOtherThing(true)),
            new("5", new SomeOtherThing(false)),
            new("6", new SomeOtherThing(false)),
            new("7", new SomeOtherThing(true)),
            new("8", new SomeOtherThing(false)),
            new("9", new SomeOtherThing(true)),
            new("10", new SomeOtherThing(false)),
            new("11", new SomeOtherThing(false)),
            new("12", new SomeOtherThing(true)),
            new("13", new SomeOtherThing(true)),
            new("14", new SomeOtherThing(true)),
            new("15", new SomeOtherThing(true))
        };

        var items = Items.Take(new Range(0, 4));
        SelectedItems = items.ToList();
        SelectedItem = items.First();

        Items2 = items.ToList();

        SelectedItemCommand = new Command<IEnumerable<object>>(selectedItems =>
        {
        });

        Command = new Command(() =>
        {
            var oldItems = Items.Take(new Range(0,1));
            Items = oldItems.ToList();
        });
    }


    public List<Something> Items
    {
        get => m_items;
        set => RaiseWhenSet(ref m_items, value);
    }

    public IEnumerable<object> SelectedItems
    {
        get => m_selectedItems;
        set => RaiseWhenSet(ref m_selectedItems, value);
    }

    public ICommand SelectedItemCommand { get; }

    public object? SelectedItem
    {
        get => m_selectedItem;
        set
        {
            Items2 = Items.ToList().Take(new Range(0, 5)).ToList();
            RaiseWhenSet(ref m_selectedItem, value);
        }
    }

    public object SelectedItem2
    {
        get => m_selectedItem2;
        set
        {
            RaiseWhenSet(ref m_selectedItem2, value);
        }
    }

    public List<Something> Items2
    {
        get => m_items2;
        set => RaiseWhenSet(ref m_items2, value);
    }
}

public class Something : ViewModel
{
    private readonly string m_title;
    private SomeOtherThing m_theTing;
    public string DisplayName => m_title;

    public SomeOtherThing TheTing
    {
        get => m_theTing;
        set => RaiseWhenSet(ref m_theTing, value);
    }

    public Something(string title, SomeOtherThing theTing)
    {
        m_title = title;
        TheTing = theTing;
    }

    public async void Initialize()
    {
        await Task.Delay(1500);
        var randomNumber = new Random(10).Next(0, 10);
        TheTing.IsIt = randomNumber > 5;
    }

    public override string ToString()
    {
        return m_title;
    }
}

public class SomeOtherThing : ViewModel
{
    private bool m_isIt;

    public bool IsIt
    {
        get => m_isIt;
        set => RaiseWhenSet(ref m_isIt, value);
    }

    public SomeOtherThing(bool isIt)
    {
        IsIt = isIt;
    }
}