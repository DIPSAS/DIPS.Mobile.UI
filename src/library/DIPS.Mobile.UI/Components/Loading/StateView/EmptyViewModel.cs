using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

internal class EmptyViewModel : ViewModel
{
    private string m_emptyTitle = DUILocalizedStrings.EmptyViewTitle;
    private string m_emptyDescription = DUILocalizedStrings.EmptyViewDescription;

    public string EmptyTitle
    {
        get => m_emptyTitle;
        set => RaiseWhenSet(ref m_emptyTitle, value);
    }

    public string EmptyDescription
    {
        get => m_emptyDescription;
        set => RaiseWhenSet(ref m_emptyDescription, value);
    }
}