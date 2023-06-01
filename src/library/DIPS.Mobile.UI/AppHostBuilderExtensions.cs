using System.Collections.ObjectModel;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.FloatingActionButtons.ExtendedFloatingActionButton;
using DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingActionButton;
using DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingNavigationButton;
using DIPS.Mobile.UI.Components.Images;
using DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;
using DIPS.Mobile.UI.Effects.DUIImageEffect;
using DIPS.Mobile.UI.Effects.DUITouchEffect;
using Microsoft.Maui.LifecycleEvents;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using ContextMenuPlatformEffect = DIPS.Mobile.UI.Components.ContextMenus.ContextMenuPlatformEffect;
using DatePickerHandler = DIPS.Mobile.UI.Components.Pickers.DatePicker.DatePickerHandler;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;
using SearchBarHandler = DIPS.Mobile.UI.Components.Searching.SearchBarHandler;
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
                .OnCreate((activity, _) => DUI.Init(activity)));
            events.AddAndroid(android => android.OnPause(_ => DUI.RemoveViewsLocatedOnTopOfPage()));
        });
#elif __IOS__
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddiOS(ios => ios.OnResignActivation(_ => DUI.RemoveViewsLocatedOnTopOfPage()));
        });
#endif
        
        //Handlers
        builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler(typeof(Chip), typeof(ChipHandler));
            handlers.AddHandler(typeof(Components.Pickers.DatePicker.DatePicker), typeof(DatePickerHandler));
            handlers.AddHandler(typeof(DateAndTimePicker), typeof(DateAndTimePickerHandler));
            handlers.AddHandler(typeof(Components.Pickers.TimePicker.TimePicker), typeof(TimePickerHandler));
            handlers.AddHandler(typeof(NativeIcon), typeof(NativeIconHandler));
            handlers.AddHandler(typeof(SearchBar), typeof(SearchBarHandler));
           // handlers.AddHandler(typeof(Components.Buttons.ImageButton), typeof(ImageButtonHandler));
       //     handlers.AddHandler(typeof(FloatingActionButtonMenu), typeof(FloatingActionButtonMenuHandler));
#if __ANDROID__
            handlers.AddHandler(typeof(Button), typeof(DIPS.Mobile.UI.Components.Buttons.Android.ButtonHandler));
            handlers.AddHandler(typeof(DIPS.Mobile.UI.Components.Searching.Android.IndeterminateProgressBar), typeof(DIPS.Mobile.UI.Components.Searching.Android.IndeterminateProgressBarHandler));
            //handlers.AddHandler(typeof(FloatingActionButton), typeof(DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingActionButton.Android.FloatingActionButtonHandler));
#elif __IOS__
            handlers.AddHandler(typeof(Components.Searching.iOS.InternalSearchBar), typeof(Components.Searching.iOS.InternalSearchBarHandler));
#endif
        });

        builder.ConfigureEffects(effects =>
        {
            effects.Add(typeof(ContextMenuEffect), typeof(ContextMenuPlatformEffect));
            effects.Add(typeof(DUITouchEffect), typeof(DUITouchPlatformEffect));
            effects.Add(typeof(DUIImageEffect), typeof(DUIImagePlatformEffect));
        });

        return builder;
    }
    
}

