using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

public class ErrorViewModel : ViewModel
{
    private string m_title = DUILocalizedStrings.ErrorViewTitle;
    private string m_description = DUILocalizedStrings.ErrorViewDescription;
    
    private bool m_isRefreshing;
    
    private ICommand? m_refreshCommand;
    
    private ImageSource? m_icon;
    
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

    /// <summary>
    /// Is used with <see cref="RefreshView"/> to determine if it is refreshing or not
    /// </summary>
    public bool IsRefreshing
    {
        get => m_isRefreshing;
        set => RaiseWhenSet(ref m_isRefreshing, value);
    }

    /// <summary>
    /// Is used with <see cref="RefreshView"/> and is executed when the user has pulled to refresh
    /// </summary>
    public ICommand? RefreshCommand
    {
        get => m_refreshCommand;
        set => RaiseWhenSet(ref m_refreshCommand, value);
    }
    
    /// <summary>
    /// Sets the icon
    /// </summary>
    /// <remarks>Optional</remarks>
    public ImageSource? Icon
    {
        get => m_icon;
        set => RaiseWhenSet(ref m_icon, value);
    }
}