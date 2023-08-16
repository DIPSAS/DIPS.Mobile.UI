using System.Collections;
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Playground.HåvardSamples;

public class HåvardPageViewModel : ViewModel
{
    private IEnumerable<object> m_selectedItems;

    public ICommand OpenContactsPickerCommand { get; set; }

    public HåvardPageViewModel()
    {
        Items = new List<string>()
        {
            "948 19 788",
            "936 15 719",
            "418 01 260",
            "959 94 238",
            "464 51 717",
            "916 18 816",
            "998 15 227",
            "468 14 026",
            "413 10 076",
            "986 77 073",
            "979 82 486",
            "453 26 392",
            "439 14 037"
        };

        var items = Items.Take(new Range(0, 4));
        SelectedItems = items.ToList();
        
        SelectedItemCommand =  new Command<IEnumerable<object>>(selectedItems =>
        {
        });
    }

    public List<string> Items { get; set; }

    public IEnumerable<object> SelectedItems
    {
        get => m_selectedItems;
        set => RaiseWhenSet(ref m_selectedItems, value);
    }

    public ICommand SelectedItemCommand { get; }

    public async void Initialize()
    {
        await Task.Delay(2000);
        OpenContactsPickerCommand.Execute(null);
    }
}