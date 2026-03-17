using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Toolbar;

internal class ToolbarSamplesViewModel : ViewModel
{
    private string m_lastAction = "None";

    public Command EditCommand => new(OnEdit);
    public Command CopyCommand => new(() => LastAction = "Copy tapped");
    public Command DeleteCommand => new(() => LastAction = "Delete tapped");
    public Command ShareCommand => new(() => LastAction = "Share tapped");
    public Command PrintCommand => new(() => LastAction = "Print tapped");

    public string LastAction
    {
        get => m_lastAction;
        set => RaiseWhenSet(ref m_lastAction, value);
    }

    private void OnEdit() => LastAction = "Edit tapped";
}
