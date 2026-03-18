using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Toolbar;

internal class ToolbarSamplesViewModel : ViewModel
{
    public Command SignCommand => new(() => { });
    public Command EditCommand => new(() => { });
    public Command CopyCommand => new(() => { });
    public Command DeleteCommand => new(() => { });
    public Command ShareCommand => new(() => { });
    public Command PrintCommand => new(() => { });
}
