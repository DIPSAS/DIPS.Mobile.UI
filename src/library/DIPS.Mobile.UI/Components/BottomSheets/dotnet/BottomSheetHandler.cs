using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheetHandler : ContentViewHandler
{
    public static partial void MapShouldFitToContent(BottomSheetHandler handler, BottomSheet bottomSheet){}

    public static partial void MapHasSearchBar(BottomSheetHandler handler, BottomSheet bottomSheet){}
    public static partial void MapToolbarItems(BottomSheetHandler handler, BottomSheet bottomSheet){}

    public static partial void MapTitle(BottomSheetHandler handler, BottomSheet bottomSheet){}
    public static partial void MapIsInteractiveCloseable(BottomSheetHandler handler, BottomSheet bottomSheet){}
}