
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Playground.HåvardSamples;

public class HåvardPageViewModel : ViewModel
{
    private IEnumerable<object> m_selectedItems;
    private object m_selectedItem;

    public ICommand Command { get; }

    public HåvardPageViewModel()
    {
        Items = new List<Something>()
        {
            new("First",new SomeOtherThing( true)),
            new("First",new SomeOtherThing( false)),
            new("First",new SomeOtherThing( true)),
            new("First",new SomeOtherThing( true)),
            new("First",new SomeOtherThing( false)),
            new("First",new SomeOtherThing( false)),
            new("First",new SomeOtherThing( true)),
            new("First",new SomeOtherThing( false)),
            new("First",new SomeOtherThing( true)),
            new("First",new SomeOtherThing( false)),
            new("First",new SomeOtherThing( false)),
            new("First",new SomeOtherThing( true)),
            new("First",new SomeOtherThing( true)),
            new("First",new SomeOtherThing( true)),
            new("First",new SomeOtherThing( true))
        };

        var items = Items.Take(new Range(0, 4));
        SelectedItems = items.ToList();
        SelectedItem = items.First();
        
        SelectedItemCommand =  new Command<IEnumerable<object>>(selectedItems =>
        {
        });

        Command = new Command(() =>
        {
            SelectedItem = null;
        });
    }
    
    

    public List<Something> Items { get; set; }

    public IEnumerable<object> SelectedItems
    {
        get => m_selectedItems;
        set => RaiseWhenSet(ref m_selectedItems, value);
    }

    public ICommand SelectedItemCommand { get; }

    public object? SelectedItem
    {
        get => m_selectedItem;
        set => RaiseWhenSet(ref m_selectedItem, value);
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