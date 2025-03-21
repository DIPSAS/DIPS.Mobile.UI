using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheetHandler : ContentViewHandler
{
    public static partial void MapHasSearchBar(BottomSheetHandler handler, BottomSheet bottomSheet) =>
        Only_Here_For_UnitTests.Throw();
    
    public static partial void MapIsInteractiveCloseable(BottomSheetHandler handler, BottomSheet bottomSheet) =>
        Only_Here_For_UnitTests.Throw();

    private static partial void MapPositioning(BottomSheetHandler handler, BottomSheet bottomSheet) =>
        Only_Here_For_UnitTests.Throw();

    private static partial void MapIsDraggable(BottomSheetHandler handler, BottomSheet bottomSheet) =>
        Only_Here_For_UnitTests.Throw();
}