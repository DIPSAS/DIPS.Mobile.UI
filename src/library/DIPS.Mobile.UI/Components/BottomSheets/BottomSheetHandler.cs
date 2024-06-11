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
            [nameof(BottomSheet.Title)] = MapTitle,
            [nameof(BottomSheet.ToolbarItems)] = MapToolbarItems,
            [nameof(BottomSheet.HasSearchBar)] = MapHasSearchBar,
            [nameof(BottomSheet.Positioning)] = MapPositioning,
            [nameof(BottomSheet.BottombarButtons)] = MapBottomBar,
            [nameof(BottomSheet.BackButtonBehavior)] = MapBackButtonBehavior,
            [nameof(BottomSheet.IsBackButtonVisible)] = MapIsBackButtonVisible
        };

    private static partial void MapIsBackButtonVisible(BottomSheetHandler handler, BottomSheet bottomSheet);

    private static partial void MapBackButtonBehavior(BottomSheetHandler handler, BottomSheet bottomSheet);

    private static partial void MapBottomBar(BottomSheetHandler handler, BottomSheet bottomSheet);

    private static partial void MapPositioning(BottomSheetHandler handler, BottomSheet bottomSheet);

    public static partial void MapHasSearchBar(BottomSheetHandler handler, BottomSheet bottomSheet);
    public static partial void MapToolbarItems(BottomSheetHandler handler, BottomSheet bottomSheet);

    public static partial void MapTitle(BottomSheetHandler handler, BottomSheet bottomSheet);
    public static partial void MapIsInteractiveCloseable(BottomSheetHandler handler, BottomSheet bottomSheet);
}