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
                : Sizes.GetSize(SizeName
                    .size_1) //There is no phyiscal home button, but we need some air between the safe area and the content
            ;
        m_bottomSheet.WrappingContentPage.Padding = new Thickness(0,
            m_bottomSheet.ShouldHaveNavigationBar ? 0 : Sizes.GetSize(SizeName.size_4), 0,
            bottom);
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
                RowDefinitions = {new RowDefinition(GridLength.Auto), new RowDefinition(GridLength.Star)}
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
        BottomSheetService.RemoveFromStack(m_bottomSheet);
    }

    internal void Opened()
    {
        m_bottomSheet.SendOpen();
    }

    private static partial void MapPositioning(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        var detents = new List<UISheetPresentationControllerDetent>
        {
            UISheetPresentationControllerDetent.CreateMediumDetent(),
            UISheetPresentationControllerDetent.CreateLargeDetent(),
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
                
                    var fitToContentDetent = TryCreateFitToContentDetent(bottomSheet);
                    if (fitToContentDetent != null)
                    {
                        detents.Add(fitToContentDetent);
                        preferredDetent = UISheetPresentationControllerDetentIdentifier.Unknown;
                    }
                    break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        bottomSheet.UISheetPresentationController.Detents = detents.ToArray();
        bottomSheet.UISheetPresentationController.SelectedDetentIdentifier = preferredDetent;
    }

    private static UISheetPresentationControllerDetent? TryCreateFitToContentDetent(BottomSheet bottomSheet)
    {
        if
            (OperatingSystem.IsIOSVersionAtLeast(16)) //Can fit to content by setting a custom detent
        {
            return UISheetPresentationControllerDetent.Create("prefferedDetent",
                resolver =>
                {
                    var r = bottomSheet.Content.Measure(UIScreen.MainScreen.Bounds.Width, resolver.MaximumDetentValue);
                    double navBarHeight = 0;
                    if (bottomSheet.ShouldHaveNavigationBar)
                    {
                        var navigationController = bottomSheet.UIViewController!.NavigationController;
                        navBarHeight = navigationController!.NavigationBar.Frame.Height;
                    }

                    return (float)(r.Request.Height + bottomSheet.Padding.Bottom + bottomSheet.Padding.Top +
                                   navBarHeight);
                });
        }

        return null;
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