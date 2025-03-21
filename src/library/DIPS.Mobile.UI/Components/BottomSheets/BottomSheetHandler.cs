using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheetHandler
{
    public BottomSheetHandler() : base(BottomSheetMapper)
    {
    }

    public static readonly IPropertyMapper<BottomSheet, BottomSheetHandler> BottomSheetMapper =
        new PropertyMapper<BottomSheet, BottomSheetHandler>(Mapper)
        {
            [nameof(BottomSheet.IsInteractiveCloseable)] = MapIsInteractiveCloseable,
            [nameof(BottomSheet.HasSearchBar)] = MapHasSearchBar,
            [nameof(BottomSheet.Positioning)] = MapPositioning,
#if __IOS__
            [nameof(BottomSheet.BottombarButtons)] = MapBottomBar,
#endif
            [nameof(BottomSheet.IsDraggable)] = MapIsDraggable
        };

    private static partial void MapIsDraggable(BottomSheetHandler handler, BottomSheet bottomSheet);
    private static partial void MapPositioning(BottomSheetHandler handler, BottomSheet bottomSheet);
    public static partial void MapHasSearchBar(BottomSheetHandler handler, BottomSheet bottomSheet);
    public static partial void MapIsInteractiveCloseable(BottomSheetHandler handler, BottomSheet bottomSheet);
}