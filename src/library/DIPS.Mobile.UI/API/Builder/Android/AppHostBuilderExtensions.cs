using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.CheckBoxes;
using DIPS.Mobile.UI.Components.Shell.Android;
using DIPS.Mobile.UI.Components.Toolbars.Android;
using Microsoft.Maui.LifecycleEvents;
using CollectionViewHandler = DIPS.Mobile.UI.Components.Lists.CollectionViewHandler;
using Shell = DIPS.Mobile.UI.Components.Shell.Shell;

namespace DIPS.Mobile.UI.API.Builder;

public static partial class AppHostBuilderExtensions
{
    static partial void AddPlatformHandlers(IMauiHandlersCollection handlers)
    {
        handlers.AddHandler(typeof(DIPS.Mobile.UI.Components.Searching.Android.IndeterminateProgressBar), typeof(DIPS.Mobile.UI.Components.Searching.Android.IndeterminateProgressBarHandler));
        handlers.AddHandler<DUIToolbar, DUIToolbarHandler>();
        handlers.AddHandler<Shell, ShellRenderer>();
    }

    static partial void ConfigurePlatformLifecycleEvents(ILifecycleBuilder events)
    {
        events.AddAndroid(android => android
            .OnCreate((activity, _) =>
            {
                DUI.Init(activity);
                FilledCheckBox.EnsureSkLottieResourcesAdded();
            }));
    }
}

