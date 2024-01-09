using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

public class EmptyViewModel : ViewModel
{
    private string m_title = DUILocalizedStrings.EmptyViewTitle;
    private string m_description = DUILocalizedStrings.EmptyViewDescription;

    /// <summary>
    /// Sets the title
    /// </summary>
    public string Title
    {
        get => m_title;
        set => RaiseWhenSet(ref m_title, value);
    }

    /// <summary>
    /// Sets the description
    /// </summary>
    public string Description
    {
        get => m_description;
        set => RaiseWhenSet(ref m_description, value);
    }
}