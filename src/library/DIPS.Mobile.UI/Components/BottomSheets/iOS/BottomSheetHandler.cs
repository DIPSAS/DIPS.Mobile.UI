using DIPS.Mobile.UI.Components.BottomSheets.iOS;
using DIPS.Mobile.UI.Converters.ValueConverters;
using Microsoft.Maui.Handlers;
using UIKit;
using ContentView = Microsoft.Maui.Platform.ContentView;

namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheetHandler : ContentViewHandler
{
    private BottomSheet m_bottomSheet;

    protected override void ConnectHandler(ContentView platformView)
    {
        base.ConnectHandler(platformView);
    }

    public void OnBeforeOpening()
    {
        if (VirtualView is not BottomSheet bottomSheet) return;
        
        m_bottomSheet = bottomSheet;
        
        bottomSheet.UISheetPresentationController!.Delegate = new BottomSheetControllerDelegate(this);
        
       

        //Add grabber
        bottomSheet.UISheetPresentationController.PrefersGrabberVisible = true;
        
        bottomSheet.UISheetPresentationController.PrefersScrollingExpandsWhenScrolledToEdge = true;
            

        var bottom = (UIApplication.SharedApplication.KeyWindow?.SafeAreaInsets.Bottom) == 0
                ? Sizes.GetSize(SizeName.size_4) //There is a physical home button
                : Sizes.GetSize(SizeName.size_1) //There is no phyiscal home button, but we need some air between the safe area and the content
            ;
        m_bottomSheet.WrappingContentPage.Padding = new Thickness(0, m_bottomSheet.ShouldHaveNavigationBar ? 0 : Sizes.GetSize(SizeName.size_4), 0,
            bottom); 
    }

    public static partial void MapShouldFitToContent(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
         var preferredDetent = UISheetPresentationControllerDetent.CreateMediumDetent();
        var preferredDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Medium;

        
        if (bottomSheet.ShouldFitToContent)
        {
            if (OperatingSystem.IsIOSVersionAtLeast(16)) //Can fit to content by setting a custom detent
            {
                preferredDetent = UISheetPresentationControllerDetent.Create("prefferedDetent",
                    resolver  =>
                    {
                        var r = bottomSheet.Content.Measure(UIScreen.MainScreen.Bounds.Width, resolver.MaximumDetentValue);
                        double navBarHeight = 0;
                        if (bottomSheet.ShouldHaveNavigationBar)
                        {
                            var navigationController = bottomSheet.UIViewController!.NavigationController;
                            navBarHeight = navigationController!.NavigationBar.Frame.Height;
                        }
                        
                        return (float)(r.Request.Height+bottomSheet.Padding.Bottom+bottomSheet.Padding.Top+navBarHeight);
                    });
                preferredDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Unknown;
            }
            else //Select large detent if the content is bigger than medium detent
            {
                if (bottomSheet.Content.Height > bottomSheet.Height / 2) //if the content is larger than half the screen when the detent is medium, it means that something is outside of bounds
                {
                    preferredDetent = UISheetPresentationControllerDetent.CreateLargeDetent();
                    preferredDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Large;
                }
            }
        }
        
        bottomSheet.UISheetPresentationController.Detents = (bottomSheet.ShouldFitToContent)

            ? new[] {preferredDetent}
            : new[] {preferredDetent, UISheetPresentationControllerDetent.CreateLargeDetent()};
        bottomSheet.UISheetPresentationController.SelectedDetentIdentifier = preferredDetentIdentifier;

    }


    public static partial void MapIsInteractiveCloseable(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
            bottomSheet.NavigationController.ModalInPresentation = !bottomSheet.IsInteractiveCloseable;
    }

    public static partial void MapTitle(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        bottomSheet.WrappingContentPage.Title = bottomSheet.Title;
    }

    public static partial void MapToolbarItems(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        foreach (var item in bottomSheet.ToolbarItems)
        {
            item.BindingContext = bottomSheet.BindingContext;
            bottomSheet.WrappingContentPage.ToolbarItems.Add(item);
        }
    }

    public static partial void MapHasSearchBar(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        if (bottomSheet.HasSearchBar)
        {
            var grid = new Grid
            {
                RowDefinitions = { new RowDefinition(GridLength.Auto), new RowDefinition(GridLength.Star) }
            };
        
            grid.Add(bottomSheet.SearchBar);
            grid.Add(bottomSheet, 0, 1);

            bottomSheet.WrappingContentPage.Content = grid;
        }
        else
        {
            bottomSheet.WrappingContentPage.Content = bottomSheet;
        }
    }

    internal void Dispose()
    {
        m_bottomSheet.SendClose();
        BottomSheetService.Current = null;
    }

    internal void Opened()
    {
        m_bottomSheet.SendOpen();
    }
}

internal class BottomSheetControllerDelegate : UISheetPresentationControllerDelegate
{
    private readonly BottomSheetHandler m_bottomSheetHandler;

    public BottomSheetControllerDelegate(BottomSheetHandler bottomSheetHandler)
    {
        m_bottomSheetHandler = bottomSheetHandler;
    }

    public override void WillPresent(UIPresentationController presentationController, UIModalPresentationStyle style,
        IUIViewControllerTransitionCoordinator? transitionCoordinator)
    {
        m_bottomSheetHandler.Opened();
    }

    public override void DidDismiss(UIPresentationController presentationController)
    {
        m_bottomSheetHandler.Dispose();
    }
}