using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Components.Loading.StateView;
using DIPS.Mobile.UI.MVVM;

namespace Playground.VetleSamples;

public class VetleTestPage1ViewModel : ViewModel
{
    private bool m_isSavingCompleted;
    private bool m_isSaving;
    private bool m_isBusy = true;

    public VetleTestPage1ViewModel()
    {
        Navigate = new Command(() => Shell.Current.Navigation.PushAsync(new VetleTestPage2()));
        Test = new Command(async () =>
        {
            await Task.Delay(1000);
            IsBusy = false;
        });

        _ = Initialize();
    }

    public bool IsBusy
    {
        get => m_isBusy;
        set => RaiseWhenSet(ref m_isBusy, value);
    }

    private async Task Refresh()
    {
        StateViewModel.CurrentState = State.Loading;
        _ = Initialize();
    }

    public string TestString { get; } = "Testern";
    
    public ObservableCollection<string> TestStrings { get; set; } = new()
    {
        "1234567",
        "7654321",
        "526321",
        "271351",
        "912512",        "912512awiojdioawjdio9awjdjwq90idjkqwidjqwoidjqwdjwqoijdwqoijdwqoijqwoidjqoiwdjoqiw jpiqouw jdoiuqw hndiouqwhnoiqw hoiquw hoiwqhioqwuhqwoiu hqwoiudhqwiouhoqiwduhidwoqu qw",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512912512awiojdioawjdio9awjdjwq90idjkqwidjqwoidjqwdjwqoijdwqoijdwqoijqwoidjqoiwdjoqiw jpiqouw jdoiuqw hndiouqwhnoiqw hoiquw hoiwqhioqwuhqwoiu hqwoiudhqwiouhoqiwduhidwoqu qw",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",   "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512912512awiojdioawjdio9awjdjwq90idjkqwidjqwoidjqwdjwqoijdwqoijdwqoijqwoidjqoiwdjoqiw jpiqouw jdoiuqw hndiouqwhnoiqw hoiquw hoiwqhioqwuhqwoiu hqwoiudhqwiouhoqiwduhidwoqu qw",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",   "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",   "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512912512awiojdioawjdio9awjdjwq90idjkqwidjqwoidjqwdjwqoijdwqoijdwqoijqwoidjqoiwdjoqiw jpiqouw jdoiuqw hndiouqwhnoiqw hoiquw hoiwqhioqwuhqwoiu hqwoiudhqwiouhoqiwduhidwoqu qw",
        "912512",
        "912512",   "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",   "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",   "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",   "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",
        "912512",

        "ABC",
        "ÅBE",
        "HAALAND",
        "ØDEGÅÅRD",
        "Testern",
        "Testern",
        "Testern",
        "Testern",
        "Testern",
        "Testern",
        "Testern",
        "Testern",
        "Testern",
        "Testern",
        "Testern",
        "Testern",
        "Testern",
        "Testern",
        "Testern",
        "Testern",
        "Testern",
        "Testern",
        "Testern",
        "Testern",

    };

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