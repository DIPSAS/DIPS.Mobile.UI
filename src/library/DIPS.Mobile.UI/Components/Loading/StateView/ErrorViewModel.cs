using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

internal class ErrorViewModel : ViewModel
{
    private string m_title = DUILocalizedStrings.ErrorViewTitle;
    private string m_description = DUILocalizedStrings.ErrorViewDescription;
    
    private bool m_isRefreshing;
    
    private ICommand? m_refreshCommand;
    
    public string Title
    {
        get => m_title;
        set => RaiseWhenSet(ref m_title, value);
    }

    public string Description
    {
        get => m_description;
        set => RaiseWhenSet(ref m_description, value);
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