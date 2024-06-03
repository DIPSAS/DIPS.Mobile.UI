using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheetHandler : ContentViewHandler
{

    public static partial void MapIsInteractiveCloseable(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        bottomSheet.ViewController.ModalInPresentation = !bottomSheet.IsInteractiveCloseable;
    }

    public static partial void MapTitle(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        bottomSheet.ViewController.SetTitle();
    }

    private static partial void MapBackButtonBehavior(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        bottomSheet.ViewController.SetBackButton();
    }

    public static partial void MapToolbarItems(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        bottomSheet.ViewController.AddToolbarItems();
    }

    public static partial void MapHasSearchBar(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        bottomSheet.ViewController.ModifySearchbar(bottomSheet.HasSearchBar);
    }

    private static partial void MapBottomBar(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        bottomSheet.ViewController.ModifyBottomBar(bottomSheet.HasBottomBarButtons);
    }

    private static partial void MapPositioning(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        if (bottomSheet.NavigationController is not null)
        {
            bottomSheet.NavigationController.SetPositioning();
        }
        else
        {
            bottomSheet.ViewController.SetPositioning();
        }
    }

   

    
}

