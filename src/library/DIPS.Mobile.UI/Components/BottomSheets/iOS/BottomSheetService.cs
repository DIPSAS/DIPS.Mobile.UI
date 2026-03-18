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
            var navigationController = new UINavigationController(bottomSheetViewController);

            var currentViewController = Platform.GetCurrentUIViewController();
            if (currentViewController is null)
                return;
            
            navigationController.ModalPresentationStyle = bottomSheet.IsDraggable ? UIModalPresentationStyle.PageSheet : UIModalPresentationStyle.FullScreen;

            TryAddGrabberAndSetSheetPresentationProperties(navigationController, bottomSheetViewController);

            await currentViewController.PresentViewControllerAsync(navigationController, true);
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
        
        // Set initial detents before presentation so iOS animates directly to the correct position
        presentationController.Detents =
        [
            UISheetPresentationControllerDetent.CreateMediumDetent(),
            UISheetPresentationControllerDetent.CreateLargeDetent()
        ];
        
        switch (bottomSheetViewController.BottomSheet.Positioning)
        {
            case Positioning.Large:
                presentationController.SelectedDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Large;
                break;
            case Positioning.Medium:
                presentationController.SelectedDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Medium;
                break;
            // Fit: custom detent is set later via SetPositioning when the container is available
        }
    }

    public async static partial Task Close(BottomSheet bottomSheet, bool animated)
    {
        if (bottomSheet?.ViewController == null) return;
        
        await bottomSheet.ViewController.DismissViewControllerAsync(animated);
        bottomSheet.ViewController.Dispose();
    }
}