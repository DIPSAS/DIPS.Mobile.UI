using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Components.Alerting.SystemMessage;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;
using DIPS.Mobile.UI.Components.Slidable;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.MVVM.Commands;

namespace Playground.EirikSamples;

public class EirikPageViewModel : ViewModel
{
    private bool m_isSaving;
    private bool m_isSavingCompleted;

    public EirikPageViewModel()
    {
        SaveCommand = new AsyncCommand(async () =>
        {
            IsSaving = true;
            await Task.Delay(2000);
            IsSavingCompleted = true;
        });
    }

    public bool IsSaving
    {
        get => m_isSaving;
        set => RaiseWhenSet(ref m_isSaving, value);
    }

    public bool IsSavingCompleted
    {
        get => m_isSavingCompleted;
        private set => RaiseWhenSet(ref m_isSavingCompleted, value);
    }

    public ICommand SaveCommand { get; }
    
    public string SelectedItem
    {
        get;
        set => RaiseWhenSet(ref field, value);
    }

    public List<string> Items { get; } =
    [
        "Item 1",
        "Item 2",
        "Item 3",
        "Item 4",
        "Item 5",
        "Item 6",
        "Item 7",
        "Item 8",
        "Item 9",
        "Item 10",
        "Item 11",
        "Item 12",
        "Item 13",
        "Item 14",
        "Item 15",
        "Item 16",
        "Item 17",
        "Item 18",
        "Item 19",
        "Item 20",
    ];
}

public class GroupedStrings : ObservableCollection<string>
{
    public GroupedStrings(string title, List<string> elements)
        : base(elements)
    {
        GroupTitle = title;
    }
    
    public string GroupTitle { get; }
}