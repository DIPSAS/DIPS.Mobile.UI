using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.SystemMessage;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;
using DIPS.Mobile.UI.Components.Slidable;
using DIPS.Mobile.UI.MVVM;

namespace Playground.EirikSamples;

public class EirikPageViewModel : ViewModel
{
    private readonly List<SelectableDateViewModel> m_dateViewModels =
    [
        new SelectableDateViewModel(DateTime.Now - new TimeSpan(4, 0, 0, 0)),
        new SelectableDateViewModel(DateTime.Now - new TimeSpan(3, 0, 0, 0)),
        new SelectableDateViewModel(DateTime.Now - new TimeSpan(2, 0, 0, 0)),
        new SelectableDateViewModel(DateTime.Now - new TimeSpan(1, 0, 0, 0))
    ];

    private SliderConfig m_config = new(-3, 0);
    private SlidableProperties m_properties = new(0);

    public EirikPageViewModel()
    {
        AddToListCommand = new Command(AddToList);
        ShowSystemMessageCommand = new Command<string>(ShowSystemMessage);
        RemoveFromListCommand = new Command<string>(RemoveFromList);
        NavigateCommand = new Command(Navigate);
    }

    private void Navigate()
    {
        App.Current.MainPage.Navigation.PushAsync(new EirikPage2 {BindingContext = this});
    }

    public ICommand NavigateCommand { get; }

    private void RemoveFromList(string obj)
    {
        var strings = new List<string>(List.FirstOrDefault()?.ToList() ?? []);
        strings.Remove(obj);
        if (List.FirstOrDefault() is not null)
            List.Remove(List.FirstOrDefault());
        List.Add(new GroupedStrings("First", strings));
    }

    private void ShowSystemMessage(string obj)
    {
        SystemMessageService.Display(configurator => configurator.Text = obj);
    }

    public Func<int, SelectableDateViewModel> DateViewModelFactory => i => m_dateViewModels[Math.Abs(i)];

    public ObservableCollection<GroupedStrings> List { get; } = new();
    public ICommand AddToListCommand { get; }
    public ICommand ShowSystemMessageCommand { get; }
    public ICommand RemoveFromListCommand { get; }

    private void AddToList()
    {
        var strings = new List<string>(List.FirstOrDefault()?.ToList() ?? []);
        strings.Insert(0, strings.Count.ToString());
        if (List.FirstOrDefault() is not null)
            List.Remove(List.FirstOrDefault());
        List.Add(new GroupedStrings("First", strings));
    }

    public SliderConfig Config
    {
        get => m_config;
        set => RaiseWhenSet(ref m_config, value);
    }

    public SlidableProperties Properties
    {
        get => m_properties;
        set => RaiseWhenSet(ref m_properties, value);
    }
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