using DIPS.Mobile.UI.Components.MyCustomView;
using Microsoft.Maui.LifecycleEvents;

namespace DIPS.Mobile.UI;

public static class AppHostBuilderExtensions
{
    // ReSharper disable once IdentifierTypo
    // ReSharper disable once InconsistentNaming
    public static MauiAppBuilder UseDIPSUI(
        this MauiAppBuilder builder)
    {
        //Initializers
        DUI.Init();
#if __ANDROID__
        builder.ConfigureLifecycleEvents(events =>
        {

            events.AddAndroid(android => android
                .OnCreate((activity, _) =>  API.Library.Android.DUI.Init(activity)));
        });
#endif
        
        //Handlers
        builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler(typeof(MyCustomView), typeof(MyCustomViewHandler));

        });

        return builder;
    }
}