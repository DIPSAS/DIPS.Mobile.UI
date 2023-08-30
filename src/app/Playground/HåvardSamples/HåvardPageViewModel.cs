
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Playground.HåvardSamples;

public class HåvardPageViewModel : ViewModel
{
    private IEnumerable<object> m_selectedItems;
    private object m_selectedItem;

    public ICommand Command { get; } = new Command(() =>
    {
    });

    public HåvardPageViewModel()
    {
        Items = new List<Something>()
        {
            new Something(new SomeOtherThing(true)),
            new Something(new SomeOtherThing(true)),
            new Something(new SomeOtherThing(false)),
            new Something(new SomeOtherThing(false)),
            new Something(new SomeOtherThing(true)),
            new Something(new SomeOtherThing(false)),
            new Something(new SomeOtherThing(false)),
            new Something(new SomeOtherThing(true)),
            new Something(new SomeOtherThing(false)),
            new Something(new SomeOtherThing(false)),
            new Something(new SomeOtherThing(false)),
            new Something(new SomeOtherThing(true)),
            new Something(new SomeOtherThing(false)),
            new Something(new SomeOtherThing(false)),
            new Something(new SomeOtherThing(false)),
            new Something(new SomeOtherThing(false)),
            new Something(new SomeOtherThing(true)),
            new Something(new SomeOtherThing(false)),
        };

        var items = Items.Take(new Range(0, 4));
        SelectedItems = items.ToList();
        SelectedItem = items.First();
        
        SelectedItemCommand =  new Command<IEnumerable<object>>(selectedItems =>
        {
        });
    }
    
    

    public List<Something> Items { get; set; }

    public IEnumerable<object> SelectedItems
    {
        get => m_selectedItems;
        set => RaiseWhenSet(ref m_selectedItems, value);
    }

    public ICommand SelectedItemCommand { get; }

    public object SelectedItem
    {
        get => m_selectedItem;
        set => RaiseWhenSet(ref m_selectedItem, value);
    }

    
    }

public class Something : ViewModel
{
    private SomeOtherThing m_theTing;

    public SomeOtherThing TheTing
    {
        get => m_theTing;
        set => RaiseWhenSet(ref m_theTing, value);
    }

    public Something(SomeOtherThing theTing)
    {
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