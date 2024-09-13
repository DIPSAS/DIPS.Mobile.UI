using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Preview.Android.Slider;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Chips;
using Microsoft.Maui.LifecycleEvents;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;
using ContentPageHandler = DIPS.Mobile.UI.Components.Pages.Android.ContentPageHandler;

namespace DIPS.Mobile.UI.API.Builder;

public static partial class AppHostBuilderExtensions
{
    static partial void AddPlatformHandlers(IMauiHandlersCollection handlers)
    {
        handlers
            .AddHandler(typeof(DIPS.Mobile.UI.Components.Searching.Android.IndeterminateProgressBar),
                typeof(DIPS.Mobile.UI.Components.Searching.Android.IndeterminateProgressBarHandler))
            .AddHandler(typeof(Chip), typeof(ChipHandler))
            .AddHandler<ContentPage, ContentPageHandler>()
            .AddHandler<CameraZoomSlider, CameraZoomSliderHandler>()
            .AddHandler<CameraPreview, CameraPreviewHandler>();
    }

    static partial void ConfigurePlatformLifecycleEvents(ILifecycleBuilder events)
    {
        events.AddAndroid(android => android
            .OnCreate((activity, _) =>
            {
                DUI.Init(activity);
                DUI.EnsureSkLottieResourcesAdded();
            }));
    }
}

