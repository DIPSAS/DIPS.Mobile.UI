using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.Images;
using DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;
using DIPS.Mobile.UI.Components.Searching;
using DIPS.Mobile.UI.Effects.AwesomeTouchEffect;
#if __ANDROID__
using DIPS.Mobile.UI.Components.Buttons.Android;
using DIPS.Mobile.UI.Components.Searching.Android;
#elif __IOS__
using DIPS.Mobile.UI.Components.Searching.iOS;
#endif

using Microsoft.Maui.LifecycleEvents;
using ContextMenuPlatformEffect = DIPS.Mobile.UI.Components.ContextMenus.ContextMenuPlatformEffect;
using DateAndTimePickerHandler = DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker.DateAndTimePickerHandler;
using DatePicker = DIPS.Mobile.UI.Components.Pickers.DatePicker.DatePicker;
using DatePickerHandler = DIPS.Mobile.UI.Components.Pickers.DatePicker.DatePickerHandler;
using Image = DIPS.Mobile.UI.Components.Images.Image;
using TimePicker = Microsoft.Maui.Controls.TimePicker;
using TimePickerHandler = DIPS.Mobile.UI.Components.Pickers.TimePicker.TimePickerHandler;

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
            handlers.AddHandler(typeof(Image), typeof(ImageHandler));
            handlers.AddHandler(typeof(Chip), typeof(ChipHandler));
            handlers.AddHandler(typeof(DatePicker), typeof(DatePickerHandler));
            handlers.AddHandler(typeof(DateAndTimePicker), typeof(DateAndTimePickerHandler));
            handlers.AddHandler(typeof(Components.Pickers.TimePicker.TimePicker), typeof(TimePickerHandler));
#if __ANDROID__
            handlers.AddHandler(typeof(InternalSearchBar), typeof(InternalSearchBarHandler));
            handlers.AddHandler(typeof(IndeterminateProgressBar), typeof(IndeterminateProgressBarHandler));
            handlers.AddHandler(typeof(Button), typeof(ButtonHandler));
#elif __IOS__
            handlers.AddHandler(typeof(InternalSearchBar), typeof(InternalSearchBarHandler));
#endif
        });

        builder.ConfigureEffects(effects =>
        {
            effects.Add(typeof(ContextMenuEffect), typeof(ContextMenuPlatformEffect));
            effects.Add(typeof(AwesomeTouchEffect), typeof(AwesomeTouchPlatformEffect));
        });

        return builder;
    }
}