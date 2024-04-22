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

            BottomSheetNavigationViewController? navigationController = null;
            if (bottomSheet.ShouldHaveNavigationBar)
            {
                navigationController = new BottomSheetNavigationViewController(bottomSheet, bottomSheetViewController);
                bottomSheet.NavigationController = navigationController;
            }
            
            UIViewController viewControllerToPresent = navigationController is not null ? navigationController : bottomSheetViewController;

            var currentViewController = Platform.GetCurrentUIViewController();
            if (currentViewController is null)
                return;
            
            viewControllerToPresent.ModalPresentationStyle = UIModalPresentationStyle.PageSheet;

            if (TryAddGrabberAndSetSheetPresentationProperties(viewControllerToPresent, bottomSheetViewController))
            {
                return;
            }

            await currentViewController.PresentViewControllerAsync(viewControllerToPresent, true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static bool TryAddGrabberAndSetSheetPresentationProperties(UIViewController viewControllerToPresent,
        BottomSheetViewController bottomSheetViewController)
    {
        //Add grabber
        var presentationController = viewControllerToPresent.SheetPresentationController;
        if (presentationController is null)
            return true;

        presentationController.PrefersGrabberVisible = true;
        presentationController.PrefersScrollingExpandsWhenScrolledToEdge = true;
        presentationController.Delegate = new BottomSheetControllerDelegate { BottomSheetViewController = bottomSheetViewController };
        presentationController.PrefersEdgeAttachedInCompactHeight = true; // Makes sure its usable when rotated.
        return false;
    }

    public async static partial Task Close(BottomSheet bottomSheet, bool animated)
    {
        if (bottomSheet?.ViewController == null) return;
        
        await bottomSheet.ViewController.DismissViewControllerAsync(animated);
        bottomSheet.ViewController.Dispose();
    }
}