using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.CarouselView;
using DIPS.Mobile.UI.Components.Layout;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;
using DIPS.Mobile.UI.Components.Selection.iOS;
using Microsoft.Maui.Controls.Handlers.Items;
using Microsoft.Maui.LifecycleEvents;
using CarouselView = DIPS.Mobile.UI.Components.CarouselView.CarouselView;

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
        handlers.AddHandler<Switch, SwitchHandler>();
        handlers.AddHandler<CarouselView2, Microsoft.Maui.Controls.Handlers.Items2.CarouselViewHandler2>();
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

