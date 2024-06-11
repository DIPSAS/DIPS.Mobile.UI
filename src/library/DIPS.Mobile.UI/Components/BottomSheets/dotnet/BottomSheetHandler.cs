using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheetHandler : ContentViewHandler
{
    private static partial void MapBackButtonBehavior(BottomSheetHandler handler, BottomSheet bottomSheet) =>
        Only_Here_For_UnitTests.Throw();
    
    public static partial void MapHasSearchBar(BottomSheetHandler handler, BottomSheet bottomSheet) =>
        Only_Here_For_UnitTests.Throw();
    
    private static partial void MapIsBackButtonVisible(BottomSheetHandler handler, BottomSheet bottomSheet) =>
        Only_Here_For_UnitTests.Throw();

    public static partial void MapToolbarItems(BottomSheetHandler handler, BottomSheet bottomSheet) =>
        Only_Here_For_UnitTests.Throw();

    public static partial void MapTitle(BottomSheetHandler handler, BottomSheet bottomSheet) =>
        Only_Here_For_UnitTests.Throw();

    public static partial void MapIsInteractiveCloseable(BottomSheetHandler handler, BottomSheet bottomSheet) =>
        Only_Here_For_UnitTests.Throw();

    private static partial void MapPositioning(BottomSheetHandler handler, BottomSheet bottomSheet) =>
        Only_Here_For_UnitTests.Throw();

    private static partial void MapBottomBar(BottomSheetHandler handler, BottomSheet bottomSheet) =>
        Only_Here_For_UnitTests.Throw();
}