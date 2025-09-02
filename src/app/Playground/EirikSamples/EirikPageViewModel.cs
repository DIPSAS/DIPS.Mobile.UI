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
    private string m_text;

    public EirikPageViewModel()
    {
        SaveCommand = new AsyncCommand(async () =>
        {
            IsSaving = true;
            await Task.Delay(2000);
            IsSavingCompleted = true;
        });
    }

    public string Text
    {
        get => m_text;
        set => RaiseWhenSet(ref m_text, value);
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