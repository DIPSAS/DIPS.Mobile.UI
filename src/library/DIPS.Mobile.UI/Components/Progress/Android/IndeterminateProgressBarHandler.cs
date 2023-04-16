using Google.Android.Material.ProgressIndicator;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Progress.Android;

public class IndeterminateProgressBarHandler : ViewHandler<IndeterminateProgressBar, LinearProgressIndicator>
{
    public IndeterminateProgressBarHandler() : base(ViewMapper)
    {
    }
    
    public static IPropertyMapper<IndeterminateProgressBar, IndeterminateProgressBarHandler> PropertyMapper = new PropertyMapper<IndeterminateProgressBar, IndeterminateProgressBarHandler>(ViewMapper)
    {
        [nameof(IndeterminateProgressBar.IsRunning)] = ToggleAnimation
    };

    protected override LinearProgressIndicator CreatePlatformView() =>
        new LinearProgressIndicator(Context);
    
    private static void ToggleAnimation(IndeterminateProgressBarHandler handler, IndeterminateProgressBar indeterminateProgressBar)
    {
        handler.PlatformView.Indeterminate = indeterminateProgressBar.IsRunning;
        if (!handler.PlatformView.Indeterminate)
        {
            handler.PlatformView.Progress = 100;
        }
        else
        {
            handler.PlatformView.Progress = 0;
        }
    }
}