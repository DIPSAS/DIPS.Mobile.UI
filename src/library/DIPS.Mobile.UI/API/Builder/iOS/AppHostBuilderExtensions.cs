using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Layout;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;
using Microsoft.Maui.LifecycleEvents;

namespace DIPS.Mobile.UI.API.Builder;

public static partial class AppHostBuilderExtensions
{
    static partial void AddPlatformHandlers(IMauiHandlersCollection handlers)
    {
        handlers.AddHandler(typeof(Components.Searching.iOS.InternalSearchBar), typeof(Components.Searching.iOS.InternalSearchBarHandler));
        handlers.AddHandler<InlineDatePicker, InlineDatePickerHandler>();
        handlers.AddHandler<InlineTimePicker, InlineTimePickerHandler>();
        handlers.AddHandler<InlineDateAndTimePicker, InlineDateAndTimePickerHandler>();
        handlers.AddHandler<Layout, LayoutHandler>();
    }

    static partial void ConfigurePlatformLifecycleEvents(ILifecycleBuilder events)
    {
        events.AddiOS(ios => ios.FinishedLaunching((_, _) =>
        {
            DUI.EnsureSkLottieResourcesAdded();
            return true;
        }));
    }
}

