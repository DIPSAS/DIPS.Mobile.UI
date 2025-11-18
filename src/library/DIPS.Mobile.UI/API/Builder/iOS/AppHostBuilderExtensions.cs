using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.API.Library.iOS;
using DIPS.Mobile.UI.Components.CarouselView;
using DIPS.Mobile.UI.Components.Lists;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;
using DIPS.Mobile.UI.Components.TextFields.Entry.iOS;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.LifecycleEvents;
using SwitchHandler = DIPS.Mobile.UI.Components.Selection.iOS.SwitchHandler;

namespace DIPS.Mobile.UI.API.Builder;

public static partial class AppHostBuilderExtensions
{
    static partial void AddPlatformHandlers(IMauiHandlersCollection handlers)
    {
        handlers.AddHandler<Components.Searching.iOS.InternalSearchBar, Components.Searching.iOS.InternalSearchBarHandler>();
        handlers.AddHandler<InlineDatePicker, InlineDatePickerHandler>();
        handlers.AddHandler<InlineTimePicker, InlineTimePickerHandler>();
        handlers.AddHandler<InlineDateAndTimePicker, InlineDateAndTimePickerHandler>();
        handlers.AddHandler<Switch, SwitchHandler>();
        handlers.AddHandler<CollectionView2, CollectionView2Handler>();
        handlers.AddHandler<CarouselView2, Microsoft.Maui.Controls.Handlers.Items2.CarouselViewHandler2>();
        handlers.AddHandler<Entry, TryFixCrashEntryHandler>();

        if (DUI.ShouldUseCustomHideSoftInputOnTappedImplementation)
        {
            PageHandler.Mapper.ReplaceMapping<ContentPage, IPageHandler>(nameof(ContentPage.HideSoftInputOnTapped), HideSoftInputOnTapHandlerMappings.MapHideSoftInputOnTapped);
            EntryHandler.Mapper.ReplaceMapping<Entry, IEntryHandler>(nameof(VisualElement.IsFocused), HideSoftInputOnTapHandlerMappings.MapInputIsFocused);
            EditorHandler.Mapper.ReplaceMapping<Editor, IEditorHandler>(nameof(VisualElement.IsFocused), HideSoftInputOnTapHandlerMappings.MapInputIsFocused);
            SearchBarHandler.Mapper.ReplaceMapping<SearchBar, ISearchBarHandler>(nameof(VisualElement.IsFocused), HideSoftInputOnTapHandlerMappings.MapInputIsFocused);
        }
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

