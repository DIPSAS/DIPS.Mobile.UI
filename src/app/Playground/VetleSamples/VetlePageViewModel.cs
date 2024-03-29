using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Components.Loading.StateView;
using DIPS.Mobile.UI.Components.Sorting;
using DIPS.Mobile.UI.MVVM;

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

        CompletedCommand = new Command(() => DialogService.ShowMessage("Test asd asdasd a sadasdas dsa", "test lang tesktsdtsdfsefseasdaawdkjawoidjiaowjdo iawjd9oia jwodijawo dijaw uoid jawuidjh awiudhawiud hiawuh " +
            "asdasdasdasdsadasd diuawhdiuawhdiuawhdiuawhiduhawiudhwaiu dhiauw dhiawudh iawuhd iauwhd iawhdiuawhdiawuhdiaw diuwa hdiuhwaid uhawiduhawdfsefasefase fse fase fase fase fasefasefasefseaf saef sae fsaef seaf sea efsa fsae", "ok"));

        SetMaxLinesCommand = new Command<string>(s => MaxLines = int.Parse(s));

        SortingDoneCommand = new Command<(object, SortOrder)>(SortingDone);

        CancelCommand = new Command(() => Shell.Current.DisplayAlert("Hei", "hei", "hei"));

        _ = DelayFunction();

        DisableCommand = new Command(Disable);

        CanExecuteCommand = new Command(() => { }, () => Disabled);

        CheckCommand = new Command(() => IsChecked = !IsChecked);
        
        TestObjects.Add(new TestObject(CheckCommand));
        TestObjects.Add(new TestObject(CheckCommand));
        TestObjects.Add(new TestObject(CheckCommand));
        TestObjects.Add(new TestObject(CheckCommand));

    }

    private void Disable()
    {
        Disabled = !Disabled;
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
        var sortOptions = new List<SortOption>()
        {
            new SortOption("Pasient med fødselsnummer, veldig lang sorteringsmulighet", "Number"),
            new SortOption("Ord", "Words"),
        };

        
        SortOptions = sortOptions;
        DefaultSelectedItem = sortOptions[1];
    }


    private void Navigatee()
    {
        var page = new VetleTestPage1();
        var vm = new VetleTestPage1ViewModel();
        page.BindingContext = vm;
        Shell.Current.Navigation.PushModalAsync(new NavigationPage(page));
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

    public ObservableCollection<string> TestStrings { get; set; } = new()
    {
        "1234567",
        "7654321",
        "526321",
        "271351",
        "912512",
        "ABC",
        "ÅBE",
        "HAALAND",
        "ØDEGÅÅRD",
        "Testern",
    };

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
