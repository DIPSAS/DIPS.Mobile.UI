using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Toolbar;

internal class ToolbarSamplesViewModel : ViewModel
{
    public Command SignCommand => new(async () =>
    {
        IsSignBusy = true;
        await Task.Delay(1000);
        IsSignBusy = false;
        IsSignFinished = true;
        await Task.Delay(2000);
        IsSignFinished = false;
        IsSignVisible = false;
    });

    public Command SimulateSignErrorCommand => new(async () =>
    {
        IsSignBusy = true;
        await Task.Delay(1000);
        IsSignBusy = false;
        HasSignError = true;
    });
    public Command EditCommand => new(() => { });
    public Command CopyCommand => new(() => { });
    public Command DeleteCommand => new(() => { });
    public Command ShareCommand => new(() => { });
    public Command PrintCommand => new(() => { });

    public Command SignErrorTappedCommand => new(async () =>
    {
        await DialogService.ShowMessage(configurator =>
        {
            configurator
                .SetTitle("Sign failed")
                .SetDescription("The signing service is currently unavailable. Please try again.");
        });
        HasSignError = false;
    });

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

    public bool IsSignFinished
    {
        get;
        set => RaiseWhenSet(ref field, value);
    }

    public bool HasSignError
    {
        get;
        set => RaiseWhenSet(ref field, value);
    }

    public string SignTitle
    {
        get;
        set => RaiseWhenSet(ref field, value);
    } = "Sign";
}
