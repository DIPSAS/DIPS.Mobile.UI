using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

/// <summary>
/// The view model to configure <see cref="StateView"/>
/// </summary>
public class StateViewModel : ViewModel
{
    
    private State m_currentState;
    private bool m_isRefreshing;

    public StateViewModel(State startingState)
    {
        CurrentState = startingState;
    }

    public event Action<State>? OnStateChanged;
    
    /// <summary>
    /// View model to the default implementation <see cref="EmptyView"/>
    /// </summary>
    public EmptyViewModel Empty { get; } = new();
    /// <summary>
    /// View model to the default implementation <see cref="ErrorView"/>
    /// </summary>
    public ErrorViewModel Error { get; } = new();
    /// <summary>
    /// View model to the default implementation <see cref="LoadingView"/>
    /// </summary>
    public LoadingViewModel Loading { get; } = new();
    /// <summary>
    /// View model to the default implementation <see cref="DefaultView"/>
    /// </summary>
    public DefaultViewModel Default { get; } = new();

    /// <summary>
    /// The current <see cref="State"/>, modify to change <see cref="StateView"/>'s view
    /// </summary>
    public State CurrentState
    {
        get => m_currentState;
        set
        {
            m_currentState = value;
            OnStateChanged?.Invoke(value);
        }
    }

    /// <summary>
    /// Sets the <see cref="CurrentState"/>
    /// </summary>
    /// <param name="state">The state to go to</param>
    public void GoToState(State state)
    {
        CurrentState = state;
    }
    
    /// <summary>
    /// Sets the <see cref="RefreshView"/>'s <see cref="Command"/>, if you have set HasRefreshView on either <see cref="Default"/>, <see cref="Error"/> or <see cref="Empty"/>
    /// </summary>
    public ICommand RefreshCommand { get; set; }

    /// <summary>
    /// Determines whether the <see cref="RefreshView"/> is refreshing or not
    /// </summary>
    public bool IsRefreshing
    {
        get => m_isRefreshing;
        set => RaiseWhenSet(ref m_isRefreshing, value);
    }
}