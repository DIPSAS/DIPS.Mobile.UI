using DIPS.Mobile.UI.MVVM;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

public class StateViewModel : ViewModel
{
    public EmptyViewModel Empty { get; } = new();
    public ErrorViewModel Error { get; } = new();
}