using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Loading.DelayedView;

public class ComplexLayoutTestPageViewModel : ViewModel
{
    private string m_navigationTimeMessage = string.Empty;

    public string NavigationTimeMessage
    {
        get => m_navigationTimeMessage;
        set => RaiseWhenSet(ref m_navigationTimeMessage, value);
    }
}
