using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Toolbar;

internal class ToolbarSamplesViewModel : ViewModel
{
    private bool m_isSignVisible = true;

    public Command SignCommand => new(() => { });
    public Command EditCommand => new(() => { });
    public Command CopyCommand => new(() => { });
    public Command DeleteCommand => new(() => { });
    public Command ShareCommand => new(() => { });
    public Command PrintCommand => new(() => { });

    public bool IsSignVisible
    {
        get => m_isSignVisible;
        set => RaiseWhenSet(ref m_isSignVisible, value);
    }
}
