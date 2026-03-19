using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Toolbar;

internal class ToolbarSamplesViewModel : ViewModel
{
    public Command SignCommand => new(async () =>
    {
        IsSignBusy = true;
        await Task.Delay(1000);
        IsSignVisible = false;
        IsSignBusy = false;
    });
    public Command EditCommand => new(() => { });
    public Command CopyCommand => new(() => { });
    public Command DeleteCommand => new(() => { });
    public Command ShareCommand => new(() => { });
    public Command PrintCommand => new(() => { });

    public bool IsSignVisible
    {
        get;
        set => RaiseWhenSet(ref field, value);
    } = true;

    public bool IsSignBusy
    {
        get;
        set => RaiseWhenSet(ref field, value);
    }
}
