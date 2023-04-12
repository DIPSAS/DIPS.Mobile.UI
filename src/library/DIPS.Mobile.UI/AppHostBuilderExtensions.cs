using DIPS.Mobile.UI.Components.ContextMenu;
using DIPS.Mobile.UI.Components.MyCustomView;
using DIPS.Mobile.UI.Effects.ContextMenuEffect;
using DIPS.Mobile.UI.Effects.PopoverEffect;
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

        builder.ConfigureEffects(effects =>
        {
            effects.Add(typeof(PopoverEffect), typeof(PopoverPlatformEffect));
            effects.Add(typeof(ContextMenuEffect), typeof(ContextMenuPlatformEffect));
        });

        return builder;
    }
}