using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.CheckBoxes;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;
using Foundation;
using Microsoft.Maui.LifecycleEvents;
using UIKit;

namespace DIPS.Mobile.UI.API.Builder;

public static partial class AppHostBuilderExtensions
{
    static partial void AddPlatformHandlers(IMauiHandlersCollection handlers)
    {
        handlers.AddHandler(typeof(DIPS.Mobile.UI.Components.Searching.iOS.InternalSearchBar), typeof(DIPS.Mobile.UI.Components.Searching.iOS.InternalSearchBarHandler));
        handlers.AddHandler<InlineDatePicker, InlineDatePickerHandler>();
    }

    static partial void ConfigurePlatformLifecycleEvents(ILifecycleBuilder events)
    {
        events.AddiOS(ios => ios.FinishedLaunching((_, _) =>
        {
            FilledCheckBox.EnsureSkLottieResourcesAdded();
            return true;
        }));
    }
}

