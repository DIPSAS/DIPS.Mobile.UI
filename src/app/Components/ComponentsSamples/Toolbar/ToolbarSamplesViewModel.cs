using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Toolbar;

internal class ToolbarSamplesViewModel : ViewModel
{
    private bool m_isSignVisible = true;
    private bool m_hidesOnScroll;

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

    public bool HidesOnScroll
    {
        get => m_hidesOnScroll;
        set => RaiseWhenSet(ref m_hidesOnScroll, value);
    }
}
