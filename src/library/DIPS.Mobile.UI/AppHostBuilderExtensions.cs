using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.Images;
using DIPS.Mobile.UI.Components.MyCustomView;
using DIPS.Mobile.UI.Components.Searching;

#if __ANDROID__
using DIPS.Mobile.UI.Components.Buttons.Android;
using DIPS.Mobile.UI.Components.Searching.Android;
using DIPS.Mobile.UI.Components.Progress.Android;
#else
using DIPS.Mobile.UI.Components.Searching.iOS;
#endif

using Microsoft.Maui.LifecycleEvents;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;
using ContextMenuPlatformEffect = DIPS.Mobile.UI.Components.ContextMenus.ContextMenuPlatformEffect;
using Image = DIPS.Mobile.UI.Components.Images.Image;
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
            handlers.AddHandler(typeof(MyCustomView), typeof(MyCustomViewHandler));
            handlers.AddHandler(typeof(Image), typeof(ImageHandler));
#if __IOS__
            handlers.AddHandler(typeof(InternalSearchBar), typeof(InternalSearchBarHandler));
            handlers.AddHandler(typeof(Shell), typeof(DIPS.Mobile.UI.Components.Shell.iOS.CustomShellRenderer));
#elif __ANDROID__
            handlers.AddHandler(typeof(InternalSearchBar), typeof(InternalSearchBarHandler));
            handlers.AddHandler(typeof(IndeterminateProgressBar), typeof(IndeterminateProgressBarHandler));
            handlers.AddHandler(typeof(Button), typeof(ButtonHandler));
#endif
        });

        builder.ConfigureEffects(effects =>
        {
            effects.Add(typeof(ContextMenuEffect), typeof(ContextMenuPlatformEffect));
        });

        return builder;
    }
}