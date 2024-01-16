using DIPS.Mobile.UI.MVVM;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

public class DefaultViewModel : ViewModel
{
    private string m_title = "Default view has not been set";

    /// <summary>
    /// Sets the title
    /// </summary>
    public string Title
    {
        get => m_title;
        set => RaiseWhenSet(ref m_title, value);
    }
}