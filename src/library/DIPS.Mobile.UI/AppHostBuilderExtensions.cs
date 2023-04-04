using DIPS.Mobile.UI.Components;
using DIPS.Mobile.UI.Components.MyCustomView;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;

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
                .OnCreate((activity, bundle) =>  API.Library.Android.DUI.Init(activity)));
        });
#endif
        return builder;
    }
}