using System.Windows.Input;
using DIPS.Mobile.UI.Components.Loading.StateView;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.Resources.Icons;

namespace Components.ComponentsSamples.Loading.StateView;

public class StateViewSamplesViewModel : ViewModel
{
    private string m_selectedState = "Loading";

    public StateViewSamplesViewModel()
    {
        SelectedItemCommand = new Command<string>(obj =>
        {
            SelectedState = obj;
            
            if (SelectedState == "Default")
            {
                MyStateViewModel.CurrentState = State.Default;
            }

            if (SelectedState == "Loading")
            {
                MyStateViewModel.CurrentState = State.Loading;
            }

            if (SelectedState == "Error")
            {
                MyStateViewModel.CurrentState = State.Error;
            }

            if (SelectedState == "Empty")
            {
                MyStateViewModel.CurrentState = State.Empty;
            }
        });
    }

    public StateViewModel MyStateViewModel { get; } = new (State.Loading);

    public string SelectedState
    {
        get => m_selectedState;
        set => RaiseWhenSet(ref m_selectedState, value);
    }

    public ICommand SelectedItemCommand { get; }
}