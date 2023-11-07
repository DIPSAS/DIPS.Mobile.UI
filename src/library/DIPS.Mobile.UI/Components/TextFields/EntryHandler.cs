namespace DIPS.Mobile.UI.Components.TextFields;

public partial class EntryHandler : Microsoft.Maui.Handlers.EntryHandler
{
    public EntryHandler() : base(PropertyMapper)
    {
    }
    
    public static IPropertyMapper<Entry, EntryHandler> PropertyMapper = new PropertyMapper<Entry, EntryHandler>(Mapper)
    {
        [nameof(Entry.HasBorder)] = MapHasBorder,
        [nameof(Entry.ShouldSelectAllTextOnFocused)] = MapShouldSelectTextOnTapped
    };

    private static partial void MapShouldSelectTextOnTapped(EntryHandler handler, Entry entry);
    private static partial void MapHasBorder(EntryHandler handler, Entry entry);
}