using System.Windows.Input;
using DIPS.Mobile.UI.Components.Loading.StateView;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Loading.StateView;

public class StateViewSamplesViewModel : ViewModel
{
    private StateViewModel m_myStateViewModel;
    private StateViewModel m_myOtherStateViewModel;

    public StateViewSamplesViewModel()
    {
        SelectedItemCommand = new Command<string>(obj =>
        {
            if (obj == "Default")
            {
                m_myStateViewModel!.CurrentState = State.Default;
                m_myOtherStateViewModel!.CurrentState = State.Default;
            }

            if (obj == "Loading")
            {
                m_myStateViewModel!.CurrentState = State.Loading;
                m_myOtherStateViewModel!.CurrentState = State.Loading;
            }

            if (obj == "Error")
            {
                m_myStateViewModel!.CurrentState = State.Error;
                m_myOtherStateViewModel!.CurrentState = State.Error;
            }

            if (obj == "Empty")
            {
                m_myStateViewModel!.CurrentState = State.Empty;
                m_myOtherStateViewModel!.CurrentState = State.Empty;
            }
        });
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
                m_myStateViewModel.CurrentState = State.Default;
            });
        }
    }

    public StateViewModel MyOtherStateViewModel
    {
        get => m_myOtherStateViewModel;
        set => m_myOtherStateViewModel = value;
    }

    public ICommand SelectedItemCommand { get; }
}