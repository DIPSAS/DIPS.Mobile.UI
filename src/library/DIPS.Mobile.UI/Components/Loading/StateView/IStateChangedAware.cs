namespace DIPS.Mobile.UI.Components.Loading.StateView;

public interface IStateChangedAware
{
    Task OnStateChanged(State state);
}