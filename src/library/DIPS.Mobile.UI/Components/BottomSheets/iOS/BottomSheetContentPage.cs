using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.BottomSheets.ToolbarConfiguration;
using Foundation;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Platform;
using ObjCRuntime;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Platform = Microsoft.Maui.ApplicationModel.Platform;
using UIModalPresentationStyle = UIKit.UIModalPresentationStyle;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

internal class BottomSheetContentPage 
{
    private UIViewController? m_viewController;
#nullable disable
    private UIViewController m_navigationViewController;
#nullable enable
    private readonly BottomSheet m_bottomSheet;
    private UISheetPresentationController? m_sheetPresentationController;

    public BottomSheetContentPage(BottomSheet bottomSheet)
    {
        m_bottomSheet = bottomSheet;

        if (bottomSheet.HasSearchBar)
        {
            IncludeSearchBar();
        }
        
        SetupViewController();
        SubscribeEvents();
    }

    private void IncludeSearchBar()
    {
        var grid = new Grid
        {
            RowDefinitions = { new RowDefinition(GridLength.Auto), new RowDefinition(GridLength.Star) }
        };
        
        grid.Add(m_bottomSheet.SearchBar);
        grid.Add(m_bottomSheet.Content, 0, 1);

       m_bottomSheet.Content = grid;
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
        
        m_bottomSheet.SendDisappearing();
        UnSubscribeEvents();
    }

    private void SetupViewController()
    {
        var mauiContext = DUI.GetCurrentMauiContext;
        
        if (mauiContext == null)
            return;

        m_viewController = m_bottomSheet.ToUIViewController(mauiContext);

        Page navigationPage;

        if (m_bottomSheet.ShouldHaveNavigationBar())
        {
            navigationPage = new NavigationPage(m_bottomSheet);
            navigationPage.SetAppThemeColor(NavigationPage.BarBackgroundColorProperty, BottomSheet.ToolbarBackgroundColorName);
            navigationPage.SetAppThemeColor(NavigationPage.BarTextColorProperty, BottomSheet.ToolbarTextColorName);
        }
        else
        {
            navigationPage = m_bottomSheet;
        }

        m_navigationViewController = navigationPage.ToUIViewController(mauiContext);

        if (m_viewController == null) 
            return;
        
        m_navigationViewController.RestorationIdentifier = BottomSheetService.BottomSheetRestorationIdentifier;
        m_navigationViewController.ModalPresentationStyle = UIModalPresentationStyle.PageSheet;

        if (!OperatingSystem.IsIOSVersionAtLeast(15) || m_viewController.SheetPresentationController == null) //Can use bottom sheet
            return;
        
        if(m_bottomSheet.ShouldHaveNavigationBar())        
            ConfigureToolbar();

        m_sheetPresentationController = m_navigationViewController.SheetPresentationController;

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
                        return (float)(r.Request.Height+m_bottomSheet.Padding.Bottom+m_bottomSheet.Padding.Top);
                    });
                preferredDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Unknown;
            }
            else if (OperatingSystem.IsIOSVersionAtLeast(15)) //Select large detent if the content is bigger than medium detent
            {
                if (m_bottomSheet.Content.Height > m_bottomSheet.Height / 2) //if the content is larger than half the screen when the detent is medium, it means that something is outside of bounds
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
            m_bottomSheet.Padding = new Thickness(0, m_bottomSheet.ShouldHaveNavigationBar() ? 0 : Sizes.GetSize(SizeName.size_4), 0,
                bottom); //Respect grabber and make sure we add some padding to the bottom, depending on if Safe Area (non physical home button) is visible.
        }
    }

    private void ConfigureToolbar()
    {
        var navigationController = m_viewController!.NavigationController;
        RemoveNavigationBarSeparator(navigationController.NavigationBar);
        // Sets the color for all navigation buttons
        navigationController.NavigationBar.TintColor = Colors.GetColor(BottomSheet.ToolbarActionButtonsName).ToPlatform(); 
    }

    private void RemoveNavigationBarSeparator(UINavigationBar navigationBar)
    {
        navigationBar.ShadowImage = new UIImage();
        navigationBar.SetBackgroundImage(new UIImage(), default);
    }

    internal async Task Open()
    {
        var currentViewController = Platform.GetCurrentUIViewController();
        if (m_navigationViewController != null && currentViewController != null)
        {
            await currentViewController.PresentViewControllerAsync(m_navigationViewController, true);
            m_bottomSheet.SendAppearing();
        }
    }
    
}