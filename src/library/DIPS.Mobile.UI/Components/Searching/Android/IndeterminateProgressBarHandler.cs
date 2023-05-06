using Google.Android.Material.ProgressIndicator;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Searching.Android;

internal class IndeterminateProgressBarHandler : ViewHandler<IndeterminateProgressBar, LinearProgressIndicator>
{
    public IndeterminateProgressBarHandler() : base(PropertyMapper)
    {
    }
    protected override LinearProgressIndicator CreatePlatformView() => new(Context);
    
    public static IPropertyMapper<IndeterminateProgressBar, IndeterminateProgressBarHandler> PropertyMapper = new PropertyMapper<IndeterminateProgressBar, IndeterminateProgressBarHandler>(ViewMapper)
    {
        [nameof(IndeterminateProgressBar.IsRunning)] = MapIsRunning,
        [nameof(IndeterminateProgressBar.TrackColor)] = MapTrackColor,
        [nameof(IndeterminateProgressBar.IndicatorColor)] = MapIndicatorColor
    };

    private static void MapIndicatorColor(IndeterminateProgressBarHandler handler, IndeterminateProgressBar bar)
    {
        handler.PlatformView.SetIndicatorColor(bar.IndicatorColor.ToPlatform());
    }

    private static void MapTrackColor(IndeterminateProgressBarHandler handler, IndeterminateProgressBar bar)
    {
        handler.PlatformView.TrackColor = bar.TrackColor.ToPlatform();
    }

    private static void MapIsRunning(IndeterminateProgressBarHandler handler, IndeterminateProgressBar indeterminateProgressBar)
    {
        handler.PlatformView.Indeterminate = indeterminateProgressBar.IsRunning;
        handler.PlatformView.Progress = !indeterminateProgressBar.IsRunning ? 100 : 0;
    }
}