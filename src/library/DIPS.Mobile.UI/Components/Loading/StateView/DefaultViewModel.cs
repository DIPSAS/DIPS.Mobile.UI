using DIPS.Mobile.UI.MVVM;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

public class DefaultViewModel : ViewModel, IRefreshAbleViewModel
{
    private string m_title = "Default view has not been set";

    /// <summary>
    /// Sets the title
    /// </summary>
    /// <remarks>Only valid when you have not overriden the default view</remarks>
    public string Title
    {
        get => m_title;
        set => RaiseWhenSet(ref m_title, value);
    }
    
    public bool HasRefreshView { get; set; }
}