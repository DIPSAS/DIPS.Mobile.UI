using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Components.Alerting.SystemMessage;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;
using DIPS.Mobile.UI.Components.Slidable;
using DIPS.Mobile.UI.MVVM;

namespace Playground.EirikSamples;

public class EirikPageViewModel : ViewModel
{
    public ObservableCollection<GroupedStrings> List { get; } =
    [
        new("Group 1", new List<string> {"Item 1", "Item 2", "Item 3"}),
        new("Group 2", new List<string> {"Item A", "Item B", "Item C"}),
        new("Group 3", new List<string> {"Item X", "Item Y", "Item Z"})
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