using DIPS.Mobile.UI.Components.Loading.StateView;
using DIPS.Mobile.UI.MVVM;

namespace Playground.VetleSamples;

public class VitalSignViewModel : ViewModel
{
    public VitalSignViewModel()
    {
    }

    public async Task Initialize()
    {
        StateViewModel.CurrentState = State.Loading;
        await Task.Delay(2000);
        StateViewModel.CurrentState = State.Default;
    }
    
    public StateViewModel StateViewModel { get; } = new(State.Loading);

}