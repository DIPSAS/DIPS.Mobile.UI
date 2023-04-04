using DIPS.Mobile.UI.Components;
using DIPS.Mobile.UI.Components.MyCustomView;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;
#if __ANDROID__
using DIPS.Mobile.UI.API.Library.Android;
#endif

namespace DIPS.Mobile.UI;

public static class AppHostBuilderExtensions
{
    // ReSharper disable once IdentifierTypo
    // ReSharper disable once InconsistentNaming
    public static MauiAppBuilder UseDIPSUI(
        this MauiAppBuilder builder)
    {
        builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler(typeof(MyCustomView), typeof(MyCustomViewHandler));

        });

#if __ANDROID__
        builder.ConfigureLifecycleEvents(events =>
        {

            events.AddAndroid(android => android
                .OnCreate((activity, bundle) => DUI.Init(activity)));
        });
#endif
        return builder;
    }
}