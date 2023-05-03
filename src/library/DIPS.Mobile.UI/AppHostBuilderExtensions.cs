using DIPS.Mobile.UI.Components.ContextMenus;
using Microsoft.Maui.LifecycleEvents;
using ContextMenuPlatformEffect = DIPS.Mobile.UI.Components.ContextMenus.ContextMenuPlatformEffect;
using Shell = DIPS.Mobile.UI.Components.Shell.Shell;

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
            events.AddAndroid(android => android.OnPause(activity => _ = DUI.RemoveViewsLocatedOnTopOfPage()));
        });
#elif __IOS__
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddiOS(ios => ios.OnResignActivation(application => _ = DUI.RemoveViewsLocatedOnTopOfPage()));
        });
#endif
        
        //Handlers
        builder.ConfigureMauiHandlers(handlers =>
        {
#if __IOS__
            handlers.AddHandler(typeof(Shell), typeof(DIPS.Mobile.UI.Components.Shell.iOS.CustomShellRenderer));
#endif
        });

        builder.ConfigureEffects(effects =>
        {
            effects.Add(typeof(ContextMenuEffect), typeof(ContextMenuPlatformEffect));
        });

        return builder;
    }
}