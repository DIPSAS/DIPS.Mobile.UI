using UIKit;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

internal static class PresentationControllerPositioningHelper
{
    public static void SetPositioning(this UIViewController viewController, BottomSheet bottomSheet, View? container = null)
    {
        var detents = new List<UISheetPresentationControllerDetent>
        {
            UISheetPresentationControllerDetent.CreateMediumDetent(),
            UISheetPresentationControllerDetent.CreateLargeDetent()
        };

        var preferredDetent = UISheetPresentationControllerDetentIdentifier.Unknown;
        switch (bottomSheet.Positioning)
        {
            case Positioning.Medium:
                preferredDetent = UISheetPresentationControllerDetentIdentifier.Medium;
                break;
            case Positioning.Large:
                preferredDetent = UISheetPresentationControllerDetentIdentifier.Large;
                break;
            case Positioning.Fit:

                var fitToContentDetent = TryCreateFitToContentDetent(viewController, bottomSheet, container);
                if (fitToContentDetent != null)
                {
                    detents.Add(fitToContentDetent);
                    preferredDetent = UISheetPresentationControllerDetentIdentifier.Unknown;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        var sheetPresentation = viewController.SheetPresentationController;
        
        if(sheetPresentation is null)
            return;
        
        // Means this is the first time this function is run
        if (sheetPresentation.Detents.Length == 1)
        {
            sheetPresentation.Detents = detents.ToArray();
            sheetPresentation.SelectedDetentIdentifier = preferredDetent;
        }
        else
        {
            sheetPresentation.AnimateChanges(() => sheetPresentation.SelectedDetentIdentifier = preferredDetent);
        }
    }
    
    private static UISheetPresentationControllerDetent? TryCreateFitToContentDetent(UIViewController uiViewController, BottomSheet bottomSheet, View? container)
    {
        if (container is null)
            return null;
        
        if
            (OperatingSystem.IsIOSVersionAtLeast(16)) //Can fit to content by setting a custom detent
        {
            return UISheetPresentationControllerDetent.Create("prefferedDetent",
                resolver =>
                {
                    var r = container.Measure(UIScreen.MainScreen.Bounds.Width, resolver.MaximumDetentValue);
                    double navBarHeight = 0;
                    if (uiViewController.NavigationController is not null)
                    {
                        var navigationController = uiViewController.NavigationController;
                        navBarHeight = navigationController!.NavigationBar.Frame.Height;
                    }

                    return (float)(r.Request.Height + bottomSheet.Padding.Bottom + bottomSheet.Padding.Top +
                                   navBarHeight);
                });
        }

        return null;
    }
}