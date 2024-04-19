using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.BottomSheets.iOS;
using UIKit;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.BottomSheets;

public static partial class BottomSheetService
{

    internal async static partial Task PlatformOpen(BottomSheet bottomSheet)
    {
        try
        {
            var mauiContext = DUI.GetCurrentMauiContext;
            if (mauiContext == null) return;


            
           
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

            //Add grabber
            var presentationController = viewControllerToPresent.SheetPresentationController;
            if (presentationController is null)
                return;

            presentationController.PrefersGrabberVisible = true;
            presentationController.PrefersScrollingExpandsWhenScrolledToEdge = true;
            presentationController.Delegate = new BottomSheetControllerDelegate { BottomSheetViewController = bottomSheetViewController };
            presentationController.PrefersEdgeAttachedInCompactHeight = true; // Makes sure its usable when rotated.

           
            await currentViewController.PresentViewControllerAsync(viewControllerToPresent, true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static void CreateDetentsAndSetPositioning(UIViewController uiViewController)
    {
        var detents = new List<UISheetPresentationControllerDetent>
        {
            UISheetPresentationControllerDetent.CreateMediumDetent(),
            UISheetPresentationControllerDetent.CreateLargeDetent(),
        };
    }
    
    public async static partial Task Close(BottomSheet bottomSheet, bool animated)
    {
        if (bottomSheet?.ViewController == null) return;
        
        await bottomSheet.ViewController.DismissViewControllerAsync(animated);
    }
}