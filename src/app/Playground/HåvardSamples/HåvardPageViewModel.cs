using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.MemoryManagement;
using DIPS.Mobile.UI.MVVM;

namespace Playground.HåvardSamples;

public class HåvardPageViewModel : ViewModel
{
    private IEnumerable<object> m_selectedItems;
    private object m_selectedItem;
    private object m_selectedItem2;
    private List<Something> m_items;
    private List<Something> m_items2;
    private HåvardPage3ViewModel m_listener;
    private HåvardPage3ViewModel m_target;

    public ICommand Command { get; }

    public ICommand Test { get; }
    
    public HåvardPageViewModel()
    {
        Items = new List<Something>()
        {
            new("Based on your input, get a random alpha numeric string. The random string generator creates a series of numbers and letters that have no pattern. These can be helpful for creating security codes.", new SomeOtherThing(true)),
            new("With this utility you generate a 16 character output based on your input of numbers and upper and lower case letters.  Random strings can be unique. Used in computing, a random string generator can also be called a random character string generator. This is an important tool if you want to generate a unique set of strings. The utility generates a sequence that lacks a pattern and is random.", new SomeOtherThing(false)),
            new("Possible applications for a random string generator could be for statistical sampling, simulations, and cryptography.  For security reasons, a random string generator can be useful. ", new SomeOtherThing(true)),
            new("The generation of this type of random string can be a common or typical task in computer programming.  Some forms of randomness concern hash or seach algorithms.  Another task that is random concerns selecting music tracks.", new SomeOtherThing(true)),
            new("Very small", new SomeOtherThing(false)),
            new("Very very very small", new SomeOtherThing(false)),
            new("Very very very veeeeeeery small", new SomeOtherThing(true)),
            new("8", new SomeOtherThing(false)),
            new("9", new SomeOtherThing(true)),
            new("10", new SomeOtherThing(false)),
            new("11", new SomeOtherThing(false)),
            new("12", new SomeOtherThing(true)),
            new("13", new SomeOtherThing(true)),
            new("14", new SomeOtherThing(true)),
            new("15", new SomeOtherThing(true))
        };

        Test = new Command(() => DialogService.ShowMessage("Test", "test", "ok"));
        
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

    public void SetListener(HåvardPage3ViewModel håvardPage3ViewModel)
    {
        m_listener = håvardPage3ViewModel;
    }

    public void DoSomething()
    {
        m_listener?.DoSomethingBack();
    }
    
    public void RemoveListener()
    {
        m_listener = null;
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