using Microsoft.Maui.Handlers;
using UIKit;

namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheetHandler : ContentViewHandler
{
    public static partial void MapIsInteractiveCloseable(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        var controller = (UIViewController?)bottomSheet.ViewController.NavigationController ?? bottomSheet.ViewController;
        controller.ModalInPresentation = !bottomSheet.IsInteractiveCloseable;
    }

    private static partial void MapIsDraggable(BottomSheetHandler arg1, BottomSheet arg2)
    {
        
    }

    public static partial void MapHasSearchBar(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        bottomSheet.ViewController.ModifySearchbar(bottomSheet.HasSearchBar);
    }

    private static void MapBottomBar(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        bottomSheet.ViewController.ModifyBottomBar(bottomSheet.HasBottomBarButtons);
    }

    private static partial void MapPositioning(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        bottomSheet.ViewController.SetPositioning();
    }
}

