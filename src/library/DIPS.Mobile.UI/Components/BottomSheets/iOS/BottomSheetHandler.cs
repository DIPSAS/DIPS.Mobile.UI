using CoreGraphics;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Layouts;
using Microsoft.Maui.Platform;
using UIKit;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = Microsoft.Maui.Graphics.Colors;
using ContentView = Microsoft.Maui.Platform.ContentView;

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
        if (handler.MauiContext == null || bottomSheet.UIViewController == null ||
            !bottomSheet.BottombarButtons.Any()) return;
        if (bottomSheet.BottomBarUIViewController != null) return; //Already presenting
        // await Task.Delay(750);
        // var border = new Border()
        // {
        //     VerticalOptions = LayoutOptions.End,
        //     HeightRequest = 80,
        //     BackgroundColor = Colors.Green
        // };
        // //https://learn.microsoft.com/en-us/dotnet/maui/user-interface/brushes/lineargradient?view=net-maui-8.0#create-a-vertical-linear-gradient
        // border.Background = new LinearGradientBrush()
        // {
        //     EndPoint = new Point(0, 1),
        //     GradientStops = new GradientStopCollection()
        //     {
        //         new() {Color = Colors.Transparent, Offset = 0.1f}, new() {Color = Colors.White, Offset = 1.0f}
        //     }
        // };
        // var horizontalStackLayout = new HorizontalStackLayout() {HorizontalOptions = LayoutOptions.Center,};
        // foreach (var button in bottomSheet.BottombarButtons)
        // {
        //     horizontalStackLayout.Add(button);
        // }
        //
        // border.Content = horizontalStackLayout;
        // var view = border.ToPlatform(handler.MauiContext);
        // bottomSheet.UIViewController.View?.AddSubview(view);
        // bottomSheet.UIViewController.ParentViewController.View.AddSubview((view));
        //floatingButton.centerXAnchor.constraint(equalTo: self.view.centerXAnchor).isActive = true
        

        
        
        
        // var contentPage =
        //     new ContentPage() {Content = border, BackgroundColor = Colors.Transparent};
        // bottomSheet.BottomBarUIViewController = contentPage.ToUIViewController(handler.MauiContext);
        // var uiViewController = new UIViewController() {View = view};
        // //Set auto layout IOS?!
        // bottomSheet.BottomBarUIViewController = uiViewController;
        // bottomSheet.BottomBarUIViewController.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
        //
        // await bottomSheet.UIViewController.PresentViewControllerAsync(bottomSheet.BottomBarUIViewController, true);

        // var  y = UIScreen.MainScreen.Bounds.Size.Height - size.Height;
        // view.Frame = new CGRect(0, y, view.Frame.Size.Width, view.Frame.Size.Height);
//         present view controller which is invisible?
// remember to remove when closing
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