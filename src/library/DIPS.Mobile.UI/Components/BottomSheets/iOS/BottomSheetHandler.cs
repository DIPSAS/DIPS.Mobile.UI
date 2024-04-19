using CoreAnimation;
using CoreGraphics;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Layouts;
using Microsoft.Maui.Platform;
using UIKit;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = Microsoft.Maui.Graphics.Colors;
using ContentView = Microsoft.Maui.Platform.ContentView;
using UIModalPresentationStyle = UIKit.UIModalPresentationStyle;

namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheetHandler : ContentViewHandler
{

    public static partial void MapIsInteractiveCloseable(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        bottomSheet.ViewController.ModalInPresentation = !bottomSheet.IsInteractiveCloseable;
    }

    public static partial void MapTitle(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        //bottomSheet.WrappingContentPage.Title = bottomSheet.Title;
    }

    public static partial void MapToolbarItems(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        foreach (var item in bottomSheet.ToolbarItems)
        {
            item.BindingContext = bottomSheet.BindingContext;
            //bottomSheet.WrappingContentPage.ToolbarItems.Add(item);
        }
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

