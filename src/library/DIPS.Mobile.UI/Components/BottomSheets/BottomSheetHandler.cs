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
            [nameof(BottomSheets.BottomSheet.IsInteractiveCloseable)] = MapIsInteractiveCloseable,
            [nameof(BottomSheets.BottomSheet.Title)] = MapTitle,
            [nameof(BottomSheets.BottomSheet.ToolbarItems)] = MapToolbarItems,
            [nameof(BottomSheets.BottomSheet.HasSearchBar)] = MapHasSearchBar,
            [nameof(BottomSheets.BottomSheet.Positioning)] = MapPositioning,
            [nameof(BottomSheets.BottomSheet.BottombarButtons)] = MapBottomBar,
        };

    private static partial void MapBottomBar(BottomSheetHandler handler, BottomSheet bottomSheet);

    private static partial void MapPositioning(BottomSheetHandler handler, BottomSheet bottomSheet);

    public static partial void MapHasSearchBar(BottomSheetHandler handler, BottomSheet bottomSheet);
    public static partial void MapToolbarItems(BottomSheetHandler handler, BottomSheet bottomSheet);

    public static partial void MapTitle(BottomSheetHandler handler, BottomSheet bottomSheet);
    public static partial void MapIsInteractiveCloseable(BottomSheetHandler handler, BottomSheet bottomSheet);
}