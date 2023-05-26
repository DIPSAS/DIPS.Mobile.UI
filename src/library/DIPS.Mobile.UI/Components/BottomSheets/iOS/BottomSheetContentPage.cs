using CoreGraphics;
using Microsoft.Maui.Platform;
using UIKit;
using Application = Microsoft.Maui.Controls.Application;
using Colors = Microsoft.Maui.Graphics.Colors;
using UIModalPresentationStyle = UIKit.UIModalPresentationStyle;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

internal class BottomSheetContentPage : ContentPage
{
    private UIViewController? m_viewController;
    private readonly BottomSheet m_bottomSheet;
    private UISheetPresentationController? m_sheetPresentationController;

    public BottomSheetContentPage(BottomSheet bottomSheet)
    {
        m_bottomSheet = bottomSheet;
        bottomSheet.VerticalOptions = LayoutOptions.Start;
        Content = bottomSheet;
        
        SetupViewController();
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        m_bottomSheet.WillClose += Close;
    }

    private void UnSubscribeEvents()
    {
        m_bottomSheet.WillClose -= Close;
    }

    private void Close(object sender, EventArgs e)
    {
        m_viewController?.DismissViewController(true, null);
    }

    private void SetupViewController()
    {
        var mauiContext = Application.Current?.MainPage?.Window.Handler.MauiContext;
        if (mauiContext == null)
        {
            return;
        }
        
        m_viewController = this.ToUIViewController(mauiContext);

        if (m_viewController == null) return;

        m_viewController.RestorationIdentifier = BottomSheetService.BottomSheetRestorationIdentifier;
        m_viewController.ModalPresentationStyle = UIModalPresentationStyle.PageSheet;

        if (OperatingSystem.IsIOSVersionAtLeast(15)) //Can use bottom sheet
        {
            if (m_viewController.SheetPresentationController == null) return;
            if (m_viewController.SheetPresentationController != null)
            {
                m_sheetPresentationController = m_viewController.SheetPresentationController;


                var prefferedDetent = UISheetPresentationControllerDetent.CreateMediumDetent();
                var prefferedDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Medium;

                if (m_bottomSheet.ShouldFitToContent)
                {
                    if (OperatingSystem.IsIOSVersionAtLeast(16)) //Can fit to content by setting a custom detent
                    {
                        prefferedDetent = UISheetPresentationControllerDetent.Create("prefferedDetent",
                            resolver  =>
                            {
                                var r = m_bottomSheet.Content.Measure(UIScreen.MainScreen.Bounds.Width, resolver.MaximumDetentValue);
                                return (float)(r.Request.Height+Padding.Bottom+Padding.Top);
                            });
                        prefferedDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Unknown;
                    }
                    else if
                        (OperatingSystem
                         .IsIOSVersionAtLeast(15)) //Select large detent if the content is bigger than medium detent
                    {
                        if (Content.Height >
                            Height /
                            2) //if the content is larger than half the screen when the detent is medium, it means that something is outside of bounds
                        {
                            prefferedDetent = UISheetPresentationControllerDetent.CreateLargeDetent();
                            prefferedDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Large;
                        }
                    }
                }

                if (OperatingSystem.IsIOSVersionAtLeast(15))
                {
                    m_sheetPresentationController.Detents = (m_bottomSheet.ShouldFitToContent)
                        ? new[] {prefferedDetent}
                        : new[] {prefferedDetent, UISheetPresentationControllerDetent.CreateLargeDetent(),};

                    //Add grabber
                    m_sheetPresentationController.PrefersGrabberVisible = true;

                    m_sheetPresentationController.SelectedDetentIdentifier = prefferedDetentIdentifier;

                    m_sheetPresentationController.PrefersScrollingExpandsWhenScrolledToEdge = true;

                    var bottom = (UIApplication.SharedApplication.KeyWindow?.SafeAreaInsets.Bottom) == 0
                            ? Sizes.GetSize(SizeName.size_4) //There is a physical home button
                            : Sizes.GetSize(SizeName.size_1) //There is no phyiscal home button, but we need some air between the safe area and the content
                        ;
                    // view.Bounds = view.Frame.Inset(Sizes.GetSize(SizeName.size_4), bottom);
                    Padding = new Thickness(0, Sizes.GetSize(SizeName.size_4), 0,
                         bottom); //Respect grabber and make sure we add some padding to the bottom, depending on if Safe Area (non physical home button) is visible.
                }
            }
        }
    }

    internal async Task Open()
    {
        var currentViewController = Platform.GetCurrentUIViewController();
        if (m_viewController != null && currentViewController != null)
        {
            await currentViewController.PresentViewControllerAsync(m_viewController, true);
        }
    }

    protected override void OnDisappearing()
    {
        UnSubscribeEvents();
        base.OnDisappearing();
        m_bottomSheet.SendDidClose();
    }
}