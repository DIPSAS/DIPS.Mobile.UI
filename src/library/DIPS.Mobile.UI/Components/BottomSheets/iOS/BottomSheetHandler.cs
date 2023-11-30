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
    private BottomSheet m_bottomSheet;

    public void OnBeforeOpening()
    {
        if (VirtualView is not BottomSheet bottomSheet) return;

        m_bottomSheet = bottomSheet;

        bottomSheet.UISheetPresentationController!.Delegate =
            new BottomSheetControllerDelegate() {BottomSheetHandler = this};
        bottomSheet.UISheetPresentationController.PrefersEdgeAttachedInCompactHeight = true; // Makes sure its usable when rotated.


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

    private async static partial void MapBottomBar(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        if (bottomSheet.HasBottomBarButtons)
        {
            var grid = new Grid(){IgnoreSafeArea = true};
            var oldContent = bottomSheet.WrappingContentPage.Content;
            grid.Add(oldContent);
            var bottomBar = bottomSheet.CreateBottomBar();
            //add extra space to not get too close to bottom safe area
            if (UIApplication.SharedApplication.Delegate.GetWindow().SafeAreaInsets.Bottom > 0)
            {
                if (bottomBar.Content == null) return;
                bottomBar.Content.Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.size_2));    
            }
            grid.Add(bottomBar);
            bottomSheet.WrappingContentPage.Content = grid;

            // bottomBar.HandlerChanged += (sender, args) =>
            // {
            //     if (bottomBar.Handler is BorderHandler borderHandler)
            //     {
            //         if (borderHandler.PlatformView is UIView uiView)
            //         {
            //             var maskview = new UIView(new CGRect(0,0, DeviceDisplay.Current.MainDisplayInfo.Width, 200)){RestorationIdentifier = "fadeview"} ;
            //             var gradientMaskLayer = new CAGradientLayer();
            //             gradientMaskLayer.Frame = maskview.Frame;
            //
            //             gradientMaskLayer.Colors = [bottomSheet.BackgroundColor.WithAlpha(0).ToCGColor(), bottomSheet.BackgroundColor.ToCGColor()];
            //             gradientMaskLayer.Locations = [0.0,0.2];
            //             maskview.Layer.AddSublayer(gradientMaskLayer);
            //             uiView.AddSubviews(maskview); 
            //             // IKKE ADD DET PÅ TOP, DA BLOKKERER DET DET SOM LIGGER BAK!
            //             // FINN EN BEDRE EVENT Å GJØRE DETTE PÅ, DET KOMMER LITT SENT
            //         }
            //     }
            // };
        }
    }

    internal void Dispose()
    {
        m_bottomSheet.SendClose();
        BottomSheetService.RemoveFromStack(m_bottomSheet);
        m_bottomSheet.Handler?.DisconnectHandler();
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

    private static Positioning GetCurrentPosition(UISheetPresentationController sheetPresentationController)
    {
        return sheetPresentationController.SelectedDetentIdentifier switch
        {
            UISheetPresentationControllerDetentIdentifier.Unknown => Positioning.Fit,
            UISheetPresentationControllerDetentIdentifier.Medium => Positioning.Medium,
            UISheetPresentationControllerDetentIdentifier.Large => Positioning.Large,
            _ => throw new ArgumentOutOfRangeException()
        };
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
    public BottomSheetHandler? BottomSheetHandler { get; set; }

    public override void WillPresent(UIPresentationController presentationController, UIModalPresentationStyle style,
        IUIViewControllerTransitionCoordinator? transitionCoordinator)
    {
        BottomSheetHandler?.Opened();
    }

    public override void DidDismiss(UIPresentationController presentationController)
    {
        BottomSheetHandler?.Dispose();
    }
}