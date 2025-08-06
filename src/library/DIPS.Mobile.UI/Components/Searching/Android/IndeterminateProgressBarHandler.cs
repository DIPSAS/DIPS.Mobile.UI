using Google.Android.Material.ProgressIndicator;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Colors = Microsoft.Maui.Graphics.Colors;

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
    };

    private static void MapTrackColor(IndeterminateProgressBarHandler handler, IndeterminateProgressBar bar)
    {
        if (bar.TrackColor == null) return;
        handler.PlatformView.TrackColor = bar.TrackColor.ToPlatform();
    }

    private static void MapIsRunning(IndeterminateProgressBarHandler handler, IndeterminateProgressBar indeterminateProgressBar)
    {
        if (indeterminateProgressBar.IsRunning)
        {
            handler.PlatformView.SetIndicatorColor(indeterminateProgressBar.IndicatorColor.ToPlatform());
        }
        else
        {
            handler.PlatformView.SetIndicatorColor(Colors.Transparent.ToPlatform());
        }
        
        handler.PlatformView.Indeterminate = indeterminateProgressBar.IsRunning;
    }
}