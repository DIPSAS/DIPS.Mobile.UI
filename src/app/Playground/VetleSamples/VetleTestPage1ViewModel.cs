using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.SystemMessage;
using DIPS.Mobile.UI.Components.Loading.StateView;
using DIPS.Mobile.UI.MVVM;

namespace Playground.VetleSamples;

public class VetleTestPage1ViewModel : ViewModel
{
    private bool m_isSavingCompleted;
    private bool m_isSaving;
    private bool m_isRefreshing;

    public VetleTestPage1ViewModel()
    {
        Navigate = new Command(() => Shell.Current.Navigation.PushAsync(new VetleTestPage2()));
        Test = new Command(() => _  = Shell.Current.Navigation.PopModalAsync());

        _ = Initialize();

        StateViewModel.Default.HasRefreshView = true;
        StateViewModel.Error.HasRefreshView = true;
        StateViewModel.RefreshCommand = new Command(() => _ = Refresh());

        SaveCommand = new Command(async () =>
        {
            IsSaving = true;

            await Task.Delay(2000);

            IsSavingCompleted = true;
        });

        ErrorCommand = new Command(async () =>
        {
            IsSaving = true;

            await Task.Delay(2000);

            IsSaving = false;
        });

        CompletedCommand = new Command(() =>
        {
            SystemMessageService.Display(config =>
            {
                config.Text = "Yeppers";
            });
        });
    }

  

    private async Task Refresh()
    {
        await Task.Delay(1000);
        await Task.Delay(1000);
        StateViewModel.IsRefreshing = false;

        if (StateViewModel.CurrentState == State.Default)
        {
            StateViewModel.CurrentState = State.Error;
        }
        else
        {
            StateViewModel.CurrentState = State.Default;
        }
        
    }

    //public VitalSignViewModel VitalSignViewModel { get; } = new();

    private async Task Initialize()
    {
        //_ = VitalSignViewModel.Initialize();
        await Task.Delay(1000);
        StateViewModel.CurrentState = State.Default;
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


    public ICommand Navigate { get; }
    
    public ICommand Test { get; }

    public bool IsSavingCompleted
    {
        get => m_isSavingCompleted;
        set => RaiseWhenSet(ref m_isSavingCompleted, value);
    }

    public bool IsSaving
    {
        get => m_isSaving;
        set => RaiseWhenSet(ref m_isSaving, value);
    }

    public StateViewModel StateViewModel { get; } = new StateViewModel(State.Loading);

    public ICommand SaveCommand { get; }
    public ICommand ErrorCommand { get; }
    public ICommand CompletedCommand { get; }
}