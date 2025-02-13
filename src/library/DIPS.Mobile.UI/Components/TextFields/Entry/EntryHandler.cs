namespace DIPS.Mobile.UI.Components.TextFields.Entry;

#if __IOS__
using MauiHandler = iOS.TryFixCrashEntryHandler;
#else
using MauiHandler = Microsoft.Maui.Handlers.EntryHandler;
#endif

public partial class EntryHandler : MauiHandler
{
    public EntryHandler() : base(PropertyMapper)
    {
    }
    
    public static IPropertyMapper<Entry, EntryHandler> PropertyMapper = new PropertyMapper<Entry, EntryHandler>(Mapper)
    {
        [nameof(Entry.HasBorder)] = MapHasBorder,
        [nameof(Entry.ShouldSelectAllTextOnFocused)] = MapShouldSelectTextOnTapped,
        [nameof(Entry.ShouldUseDefaultPadding)] = MapShouldUseDefaultPadding
    };

    private static partial void MapShouldUseDefaultPadding(EntryHandler handler, Entry entry);
    private static partial void MapShouldSelectTextOnTapped(EntryHandler handler, Entry entry);
    private static partial void MapHasBorder(EntryHandler handler, Entry entry);
}