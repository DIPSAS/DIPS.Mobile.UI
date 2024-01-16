using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Components.Loading.StateView;
using DIPS.Mobile.UI.MVVM;

namespace Playground.VetleSamples;

public class VetleTestPage1ViewModel : ViewModel
{
    private bool m_isSavingCompleted;
    private bool m_isSaving;

    public VetleTestPage1ViewModel()
    {
        Navigate = new Command(() => Shell.Current.Navigation.PushAsync(new VetleTestPage2()));
        Test = new Command(() => _  = Refresh());

        _ = Initialize();
    }

    private async Task Refresh()
    {
        StateViewModel.CurrentState = State.Loading;
        _ = Initialize();
    }

    //public VitalSignViewModel VitalSignViewModel { get; } = new();

    private async Task Initialize()
    {
        //_ = VitalSignViewModel.Initialize();
        await Task.Delay(1000);
        StateViewModel.CurrentState = State.Default;
    }

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
}