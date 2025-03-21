using DIPS.Mobile.UI.Components.BottomSheets.iOS;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.BottomSheets;

public static partial class BottomSheetService
{

    internal async static partial Task PlatformOpen(BottomSheet bottomSheet)
    {
        try
        {
            var bottomSheetViewController = new BottomSheetViewController(bottomSheet);

            var currentViewController = Platform.GetCurrentUIViewController();
            if (currentViewController is null)
                return;
            
            bottomSheetViewController.ModalPresentationStyle = bottomSheet.IsDraggable ? UIModalPresentationStyle.PageSheet : UIModalPresentationStyle.FullScreen;

            TryAddGrabberAndSetSheetPresentationProperties(bottomSheetViewController, bottomSheetViewController);

            await currentViewController.PresentViewControllerAsync(bottomSheetViewController, true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static void TryAddGrabberAndSetSheetPresentationProperties(UIViewController viewControllerToPresent,
        BottomSheetViewController bottomSheetViewController)
    {
        var presentationController = viewControllerToPresent.SheetPresentationController;
        if (presentationController is null) 
            return;

        presentationController.PrefersGrabberVisible = true;
        presentationController.PrefersScrollingExpandsWhenScrolledToEdge = true;
        presentationController.Delegate = new BottomSheetControllerDelegate { BottomSheetViewController = bottomSheetViewController };
        presentationController.PrefersEdgeAttachedInCompactHeight = true; // Makes sure its usable when rotated.
    }

    public async static partial Task Close(BottomSheet bottomSheet, bool animated)
    {
        if (bottomSheet?.ViewController == null) return;
        
        await bottomSheet.ViewController.DismissViewControllerAsync(animated);
        bottomSheet.ViewController.Dispose();
    }
}