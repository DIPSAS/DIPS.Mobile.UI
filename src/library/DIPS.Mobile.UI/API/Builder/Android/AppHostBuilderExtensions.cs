using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.LifecycleEvents;

namespace DIPS.Mobile.UI.API.Builder;

public static partial class AppHostBuilderExtensions
{
    static partial void AddPlatformHandlers(IMauiHandlersCollection handlers)
    {
        handlers.AddHandler(typeof(Button), typeof(DIPS.Mobile.UI.Components.Buttons.Android.ButtonHandler));
        handlers.AddHandler(typeof(DIPS.Mobile.UI.Components.Searching.Android.IndeterminateProgressBar), typeof(DIPS.Mobile.UI.Components.Searching.Android.IndeterminateProgressBarHandler));
    }

    static partial void ConfigurePlatformLifecycleEvents(ILifecycleBuilder events)
    {
        events.AddAndroid(android => android
            .OnCreate((activity, _) => DUI.Init(activity)));
        events.AddAndroid(android => android.OnPause(_ => DUI.RemoveViewsLocatedOnTopOfPage()));
    }
}

