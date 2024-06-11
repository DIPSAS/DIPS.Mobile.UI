using System.ComponentModel;
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

public class ErrorViewModel : ViewModel, IRefreshableViewModel
{
    private string m_title = DUILocalizedStrings.ErrorViewTitle;
    private string m_description = DUILocalizedStrings.ErrorViewDescription;
    
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
    /// Sets the icon
    /// </summary>
    /// <remarks>Optional</remarks>
    [TypeConverter(nameof(ImageSourceConverter))]
    public ImageSource? Icon
    {
        get => m_icon;
        set => RaiseWhenSet(ref m_icon, value);
    }
    
    public bool HasRefreshView { get; set; }
}