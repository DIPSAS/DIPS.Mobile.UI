using Android.App;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Graphics.Drawable;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Preview.Android.Slider;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.API.Library.Android;
using DIPS.Mobile.UI.Components.Chips;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;
using DIPS.Mobile.UI.Components.TabView;
using DIPS.Mobile.UI.Components.TabView.Android;
using Microsoft.Maui.Controls.PlatformConfiguration;
using ActivityIndicatorHandler = DIPS.Mobile.UI.Components.Loading.Android.ActivityIndicatorHandler;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Shell = DIPS.Mobile.UI.Components.Shell.Shell;
using SwitchHandler = DIPS.Mobile.UI.Components.Selection.Android.SwitchHandler;

namespace DIPS.Mobile.UI.API.Builder;

public static partial class AppHostBuilderExtensions
{
    static partial void AddPlatformHandlers(IMauiHandlersCollection handlers)
    {
        handlers
            .AddHandler<Components.Searching.Android.IndeterminateProgressBar, Components.Searching.Android.IndeterminateProgressBarHandler>()
            .AddHandler<Chip, ChipHandler>()
            .AddHandler<CameraZoomSlider, CameraZoomSliderHandler>()
            .AddHandler<CameraPreview, CameraPreviewHandler>()
            .AddHandler<TabView, TabViewHandler>()
            .AddHandler<Switch, SwitchHandler>()
            .AddHandler<ActivityIndicator, ActivityIndicatorHandler>();
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

