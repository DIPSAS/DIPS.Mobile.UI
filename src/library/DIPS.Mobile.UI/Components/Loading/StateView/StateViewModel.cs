using DIPS.Mobile.UI.MVVM;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

/// <summary>
/// The view model to configure <see cref="StateView"/>
/// </summary>
public class StateViewModel : ViewModel
{
    private readonly IStateChangedAware m_stateChangedAware;
    
    private State m_currentState;

    public StateViewModel(IStateChangedAware stateChangedAware)
    {
        m_stateChangedAware = stateChangedAware;
    }
    
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
            m_stateChangedAware.OnStateChanged(value);
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
}