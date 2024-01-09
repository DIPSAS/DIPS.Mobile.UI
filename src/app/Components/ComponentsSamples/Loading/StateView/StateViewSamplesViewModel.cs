using System.Windows.Input;
using DIPS.Mobile.UI.Components.Loading.StateView;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Loading.StateView;

public class StateViewSamplesViewModel : ViewModel
{
    private State m_currentState;
    private StateViewModel m_myStateViewModel;

    public StateViewSamplesViewModel()
    {
        SelectedItemCommand = new Command<string>(obj =>
        {
            if (obj == "Default")
            {
                CurrentState = State.Default;
            }

            if (obj == "Loading")
            {
                CurrentState = State.Loading;
            }

            if (obj == "Error")
            {
                CurrentState = State.Error;
            }

            if (obj == "Empty")
            {
                CurrentState = State.Empty;
            }
        });
    }
    
    public State CurrentState
    {
        get => m_currentState;
        set => RaiseWhenSet(ref m_currentState, value);
    }

    public StateViewModel MyStateViewModel
    {
        get => m_myStateViewModel;
        set
        {
            m_myStateViewModel = value;

            m_myStateViewModel.Error.Title = "This is an error title";
            m_myStateViewModel.Error.Description = "Description of the error";

            m_myStateViewModel.Error.RefreshCommand = new Command(async () =>
            {
                m_myStateViewModel.Error.IsRefreshing = false;
                await Task.Delay(1000);
                // The error is resolved
                CurrentState = State.Default;
            });
        }
    }

    public ICommand SelectedItemCommand { get; }
}