using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;
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

        if (!bottomSheet.HasSearchBar)
        {
            Content = bottomSheet;
        }
        else
        {
            AddWithSearchBar();
        }
        
        SetupViewController();
        SubscribeEvents();
    }

    private void AddWithSearchBar()
    {
        var grid = new Grid
        {
            RowDefinitions = { new RowDefinition(GridLength.Auto), new RowDefinition(GridLength.Star) }
        };
        
        grid.Add(m_bottomSheet.SearchBar);
        grid.Add(m_bottomSheet, 0, 1);

        Content = grid;
    }

    private void SubscribeEvents()
    {
        m_bottomSheet.WillClose += Close;
    }

    private void UnSubscribeEvents()
    {
        m_bottomSheet.WillClose -= Close;
    }

    private void Close(object? sender, EventArgs e)
    {
        m_viewController?.DismissViewController(true, null);
    }

    private void SetupViewController()
    {
        var mauiContext = DUI.GetCurrentMauiContext;
        
        if (mauiContext == null)
            return;

        m_viewController = this.ToUIViewController(mauiContext);
        
        if (m_viewController == null) 
            return;
        
        m_viewController.RestorationIdentifier = BottomSheetService.BottomSheetRestorationIdentifier;
        m_viewController.ModalPresentationStyle = UIModalPresentationStyle.PageSheet;

        if (!OperatingSystem.IsIOSVersionAtLeast(15) || m_viewController.SheetPresentationController == null) //Can use bottom sheet
            return;

        m_sheetPresentationController = m_viewController.SheetPresentationController;

        var preferredDetent = UISheetPresentationControllerDetent.CreateMediumDetent();
        var preferredDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Medium;

        if (m_bottomSheet.ShouldFitToContent)
        {
            if (OperatingSystem.IsIOSVersionAtLeast(16)) //Can fit to content by setting a custom detent
            {
                preferredDetent = UISheetPresentationControllerDetent.Create("prefferedDetent",
                    resolver  =>
                    {
                        var r = m_bottomSheet.Content.Measure(UIScreen.MainScreen.Bounds.Width, resolver.MaximumDetentValue);
                        return (float)(r.Request.Height+Padding.Bottom+Padding.Top);
                    });
                preferredDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Unknown;
            }
            else if (OperatingSystem.IsIOSVersionAtLeast(15)) //Select large detent if the content is bigger than medium detent
            {
                if (Content.Height > Height / 2) //if the content is larger than half the screen when the detent is medium, it means that something is outside of bounds
                {
                    preferredDetent = UISheetPresentationControllerDetent.CreateLargeDetent();
                    preferredDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Large;
                }
            }
        }

        if (OperatingSystem.IsIOSVersionAtLeast(15))
        {
            m_sheetPresentationController.Detents = (m_bottomSheet.ShouldFitToContent)
                ? new[] {preferredDetent}
                : new[] {preferredDetent, UISheetPresentationControllerDetent.CreateLargeDetent(),};

            //Add grabber
            m_sheetPresentationController.PrefersGrabberVisible = true;

            m_sheetPresentationController.SelectedDetentIdentifier = preferredDetentIdentifier;

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