using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

internal class ErrorViewModel : ViewModel
{
    private string m_errorViewTitle = DUILocalizedStrings.ErrorViewTitle;
    private string m_errorViewDescription = DUILocalizedStrings.ErrorViewDescription;
    
    private bool m_isRefreshing;
    
    private ICommand? m_refreshCommand;
    
    public string ErrorViewTitle
    {
        get => m_errorViewTitle;
        set => RaiseWhenSet(ref m_errorViewTitle, value);
    }

    public string ErrorViewDescription
    {
        get => m_errorViewDescription;
        set => RaiseWhenSet(ref m_errorViewDescription, value);
    }

    public bool IsRefreshing
    {
        get => m_isRefreshing;
        set => RaiseWhenSet(ref m_isRefreshing, value);
    }

    public ICommand? RefreshCommand
    {
        get => m_refreshCommand;
        set => RaiseWhenSet(ref m_refreshCommand, value);
    }
}