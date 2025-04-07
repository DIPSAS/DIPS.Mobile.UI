using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Components.Loading.StateView;
using DIPS.Mobile.UI.Components.Sorting;
using DIPS.Mobile.UI.Components.VoiceVisualizer;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.Resources.Icons;

namespace Playground.VetleSamples;

public class VetlePageViewModel : ViewModel
{
    private bool m_isChecked = true;
    private bool m_isProgressing = true;
    private bool m_isError;
    private bool m_isToggled;
    private LayoutOptions m_horizontalOptions;
    private bool m_isEllipsized;
    private int m_maxLines = 3;
    private List<SortOption> m_sortOptions;
    private SortOption m_defaultSelectedItem;
    private bool m_disabled;
    private bool m_isSaving;
    private bool m_isSavingCompleted;
    private SortOption m_selectedOrganizationalUnit;
    private DateTime m_date = new DateTime(2023, 2, 1, 23, 30, 0, DateTimeKind.Utc);
    private DateTime m_localDate = new DateTime(2023, 2, 1, 23, 30, 0, DateTimeKind.Local);
    private TestObject2 m_testObject2;
    private TimePlanningViewModel m_timePlanningViewModel = null;
    private bool m_isEnabled;
    private List<string> m_testStrings = [];
    private ObservableCollection<GroupedTest> m_groupedTest = [];
    private bool m_isRefreshing;
    private string m_subtitle = "Tjeneste med tilleggsspørsmål og egenskaper";
    private bool m_isVisible;
    private string m_transcriptionText;

    public VetlePageViewModel()
    {
        
        Navigate = new Command(Navigatee);
        SaveSuccess = new Command(async () =>
        {
        });

        SaveError = new Command(async () =>
        {
            IsSavingCompleted = false;
            IsError = false;
            IsSaving = true;
            await Task.Delay(2000);
            IsSaving = false;
            IsError = true;
            IsSavingCompleted = true;
        });

        CompletedCommand = new Command(() =>
        {
            if(m_testStrings.Count == 0)
                m_testStrings.Add("Test");
            else
            {
                m_testStrings.Clear();
            }
            RaisePropertyChanged(nameof(IsEnabled));
        });

        SetMaxLinesCommand = new Command<string>(s => MaxLines = int.Parse(s));

        SortingDoneCommand = new Command<(object, SortOrder)>(SortingDone);

        CancelCommand = new Command(() => Controller.IsRunning = !Controller.IsRunning);

        //_ = DelayFunction();

        DisableCommand = new Command(Disable);

        CanExecuteCommand = new Command(() => { }, () => Disabled);

        CheckCommand = new Command(async () =>
        {
            IsSaving = true;
            await Task.Delay(2000);
            IsSavingCompleted = true;
        });
        
        TestObjects.Add(new TestObject(CheckCommand));
        TestObjects.Add(new TestObject(CheckCommand));
        TestObjects.Add(new TestObject(CheckCommand));
        TestObjects.Add(new TestObject(CheckCommand));

        
        GroupedTest = [new(["Test"]), new(TestStrings.ToList())];

        _ = GenerateRandomWords();
    }

    private async Task GenerateRandomWords()
    {
        var random = new Random();
        var words = new List<string> { "apple", "banana", "cherry", "date", "elderberry", "fig", "grape", "honeydew" };
        while(true)
        {
            var randomWord = words[random.Next(words.Count)];
            TranscriptionText += randomWord + " ";

            await Task.Delay(250);
        }
    }

    public TimeSpan? Time { get; set; } = TimeSpan.Zero;
    
    private void Disable()
    {
        IsVisible = !IsVisible;
    }

    public bool Disabled
    {
        get => m_disabled;
        set
        {
            RaiseWhenSet(ref m_disabled, value);
            (CanExecuteCommand as Command).ChangeCanExecute();
        }
    }

    public ICommand CancelCommand { get; }
    
    private void SortingDone((object, SortOrder) sortResult)
    {
        var oldTestStrings = new List<string>(TestStrings);
        
        oldTestStrings.Sort(new SortOptionComparer(sortResult.Item1 as SortOption, sortResult.Item2));

        RaisePropertyChanged(nameof(TestStrings));
    }

    public string TestString { get; } = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";

    public bool IsEllipsized
    {
        get => m_isEllipsized;
        set => RaiseWhenSet(ref m_isEllipsized, value);
    }

    public int MaxLines
    {
        get => m_maxLines;
        set => RaiseWhenSet(ref m_maxLines, value);
    }

    public ICommand SetMaxLinesCommand { get; }
    
    public ICommand CheckCommand { get; }

    private async Task Test2()
    {
        IsProgressing = true;
        await Task.Delay(2000);
        IsError = true;
        IsProgressing = false;
        await Task.Delay(2000);
        IsError = false;
    }

    private async Task DelayFunction()
    {
        await Task.Delay(1000);

        GroupedTest = [new(TestStrings.ToList()), new(TestStrings.ToList())];
        
        await Task.Delay(5000);
        
        GroupedTest = [new(TestStrings.ToList()), new(TestStrings.ToList()), new(TestStrings.ToList())];
    }


    private void Navigatee()
    {
        Shell.Current.Navigation.PushAsync(new VetleTestPage1());
    }

    public List<SortOption> SortOptions
    {
        get => m_sortOptions;
        set => RaiseWhenSet(ref m_sortOptions, value);
    }

    public SortOption DefaultSelectedItem
    {
        get => m_defaultSelectedItem;
        set => RaiseWhenSet(ref m_defaultSelectedItem, value);
    }

    public ObservableCollection<string> TestStrings { get; set; } =
    [
        "Lokalisasjon påkrevd - Kodeverk og egendefinert",
        "Lokalisasjon - Fritekst",
        "tjeneste med tilleggsspørsmål og egenskaper",
        "271351",
        "912512",
        "ABC",
        "ÅBE",
        "HAALAND",
        "ØDEGÅÅRD",
        "Testern",
        "Test",
        "ABC",
        "ÅBE",
        "HAALAND",
        "ØDEGÅÅRD",
        "Testern",
        "ABC",
        "ÅBE",
        "HAALAND",
        "ØDEGÅÅRD",
        "Testern",
    ];

    public List<TestObject2> TestObject2s { get; } =
        [new TestObject2("Lol"), new TestObject2("Lol2"), new TestObject2("Lol3"), new TestObject2("Lol4")];
    
    public string? Summary { get; }

    public TestObject2 TestObject2
    {
        get => m_testObject2;
        private set => RaiseWhenSet(ref m_testObject2, value);
    }

    public ICommand RemoveStringCommand => new Command(s =>
    {
        var firstOrDefault = TestStrings.FirstOrDefault(testString => (string)s == testString);
        TestStrings.Remove(firstOrDefault);
    });

    public List<TestObject> TestObjects { get; } = new List<TestObject>();
    
    public ICommand Navigate { get; }
    public ICommand SaveSuccess { get; }
    public ICommand SaveError { get; }
    public ICommand CompletedCommand { get; }

    public bool IsChecked
    {
        get => m_isChecked;
        set => RaiseWhenSet(ref m_isChecked, value);
    }

    public bool IsError
    {
        get => m_isError;
        set => RaiseWhenSet(ref m_isError, value);
    }

    public bool IsProgressing
    {
        get => m_isProgressing;
        set => RaiseWhenSet(ref m_isProgressing, value);
    }

    public LayoutOptions HorizontalOptions
    {
        get => m_horizontalOptions;
        set => RaiseWhenSet(ref m_horizontalOptions, value);
    }

    public bool IsToggled
    {
        get => m_isToggled;
        set
        {
            m_isToggled = value;
            if (value)
            {
                HorizontalOptions = LayoutOptions.Center;
            }
            else
            {
                HorizontalOptions = LayoutOptions.End;
            }
        }
    }

    public ICommand SortingDoneCommand { get; }
    public ICommand DisableCommand { get; }
    public ICommand CanExecuteCommand { get; }

    public bool IsSaving
    {
        get => m_isSaving;
        set => RaiseWhenSet(ref m_isSaving, value);
    }

    public bool IsSavingCompleted
    {
        get => m_isSavingCompleted;
        set => RaiseWhenSet(ref m_isSavingCompleted, value);
    }

    public StateViewModel StateViewModel { get; set; } = new(State.Loading);

    public DateTime Date
    {
        get => m_date;
        set => m_date = value;
    }

    public DateTime LocalDate
    {
        get => m_localDate;
        set => m_localDate = value;
    }

    public DateTime? NullableDate { get; set; } = new DateTime(2023, 2, 1, 0, 0, 0);
    public DateTime? StartingNullDate { get; set; } = null;
    public List<SortOption> SelectableOrganizations { get; } = [new SortOption("Lol", "Lol")];

    public SortOption SelectedOrganizationalUnit
    {
        get => m_selectedOrganizationalUnit;
        set => RaiseWhenSet(ref m_selectedOrganizationalUnit, value);
    }

    public ICommand SelectedDateCommand => new Command(date =>
    {
        
    });

    public ICommand SelectedTimeCommand => new Command(time =>
    {

    });

    public ObservableCollection<GroupedTest> GroupedTest
    {
        get => m_groupedTest;
        set => RaiseWhenSet(ref m_groupedTest, value);
    }

    public TimePlanningViewModel TimePlanningViewModel
    {
        get => m_timePlanningViewModel;
        set => RaiseWhenSet(ref m_timePlanningViewModel, value);
    }

    public bool IsEnabled => m_testStrings.Any(x => x.Equals("Test"));

    public ICommand RefreshCommand => new Command(async () =>
    {
        await Task.Delay(2000);
        IsRefreshing = false;
        GroupedTest = [new(TestStrings.ToList()), new(TestStrings.ToList()), new(TestStrings.ToList())];
    });

    public bool IsRefreshing
    {
        get => m_isRefreshing;
        set => RaiseWhenSet(ref m_isRefreshing, value);
    }

    public ICommand AddItemCommand => new Command(() =>
    {
        TestStrings.Add("Test");
    });

    public string Subtitle
    {
        get => m_subtitle;
        set => RaiseWhenSet(ref m_subtitle, value);
    }

    public Controller Controller { get; } = new();

    public bool IsVisible
    {
        get => m_isVisible;
        set => RaiseWhenSet(ref m_isVisible, value);
    }

    public string TranscriptionText
    {
        get => m_transcriptionText;
        set => RaiseWhenSet(ref m_transcriptionText, value);
    }

    public void OnDateChanged()
    {
        TimePlanningViewModel = new TimePlanningViewModel(this);
    }
}

public class Controller : AmplitudeViewController
{
    public override float GetNextAmplitude()
    {
        return (float)new Random().NextDouble();
    }
}

public class GroupedTest : List<string>
{
    public GroupedTest(List<string> strings) : base(strings)
    {
        
    }
    
    public string Header { get; } = "Header";
}

public class SortOption
{
    public SortOption(string text, string identifier)
    {
        Text = text;
        Identifier = identifier;
    }
    
    public string Text { get; }
    public string Identifier { get; }
}

class SortOptionComparer : IComparer<string>
{
    private readonly SortOption m_sortOption;
    private readonly SortOrder m_sortOrder;

    public SortOptionComparer(SortOption sortOption, SortOrder sortOrder)
    {
        m_sortOption = sortOption;
        m_sortOrder = sortOrder;
    }
    
    public int Compare(string x, string y)
    {
        if (m_sortOption.Identifier == "Number")
        {
            var xIsNumber = int.TryParse(x, out var number1);
            var yIsNumber = int.TryParse(y, out var number2);

            var returnValue = 0;
            
            if (xIsNumber && yIsNumber)
            {
                returnValue = number1.CompareTo(number2);
            }

            if (xIsNumber && !yIsNumber)
                returnValue = 1;

            if (!xIsNumber && !yIsNumber)
                returnValue = -1;

            return m_sortOrder == SortOrder.Ascending ? returnValue : -returnValue;
        }
        else
        {
            var xIsNumber = int.TryParse(x, out var number1);
            var yIsNumber = int.TryParse(y, out var number2);

            var returnValue = 0;
            
            if (xIsNumber && yIsNumber)
                returnValue = 0;

            if (!xIsNumber && yIsNumber)
                returnValue = 1;

            if (!xIsNumber && !yIsNumber)
                returnValue = String.Compare(x, y, StringComparison.OrdinalIgnoreCase);
            
            return m_sortOrder == SortOrder.Ascending ? returnValue : -returnValue;
        }
    }

}
public class TestObject
{
    public TestObject(ICommand command)
    {
        Command = command;
    }
    
    public ICommand Command { get; }

    public bool IsChecked { get; } = true;
}

public class TestObject2
{
    public TestObject2(string name)
    {
        Name = name;
        TestCommand = new Command(() => { });
        // Generate random icon
        var random = new Random();
        var icon = random.Next(0, 2);
        Icon = icon == 0 ? Icons.GetIcon(IconName.alert_fill) : Icons.GetIcon(IconName.failure_fill);
    }
    
    public ImageSource Icon { get; set; }
   public string Name { get; set; }

   public string SearchTerm { get; } = "Test";
   
   public ICommand TestCommand { get; }
}


