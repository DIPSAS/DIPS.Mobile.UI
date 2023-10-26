using DIPS.Mobile.UI.API.Library;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Platform = Microsoft.Maui.ApplicationModel.Platform;
using UIModalPresentationStyle = UIKit.UIModalPresentationStyle;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

internal class BottomSheetContentPage : ContentPage
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
        
        this.SetAppThemeColor(BackgroundColorProperty, BottomSheet.BackgroundColorName);
        
        if (bottomSheet.HasSearchBar)
        {
            IncludeSearchBar();
        }
        else
        {
            Content = bottomSheet;
        }

        SetupViewController();
    }


    private void IncludeSearchBar()
    {
        var grid = new Grid
        {
            RowDefinitions = { new RowDefinition(GridLength.Auto), new RowDefinition(GridLength.Star) }
        };
        
        grid.Add(m_bottomSheet.SearchBar);
        grid.Add(m_bottomSheet, 0, 1);

        Content = grid;
    }

#pragma warning disable CA1416
    private void SetupViewController()
    {
        if (!OperatingSystem.IsIOSVersionAtLeast(15))
        {
            return;
        }
        
        var mauiContext = DUI.GetCurrentMauiContext;
        
        if (mauiContext == null)
            return;

        m_viewController = this.ToUIViewController(mauiContext);

        Page navigationPage;

        if (m_bottomSheet.ShouldHaveNavigationBar)
        {
            navigationPage = new NavigationPage(this);
            navigationPage.SetAppThemeColor(NavigationPage.BarBackgroundColorProperty, BottomSheet.BackgroundColorName);
            navigationPage.SetAppThemeColor(NavigationPage.BarTextColorProperty, BottomSheet.ToolbarTextColorName);
        }
        else
        {
            navigationPage = this;
        }

        m_navigationViewController = navigationPage.ToUIViewController(mauiContext);
        
        if (m_viewController == null) 
            return;
        
        m_navigationViewController.RestorationIdentifier = BottomSheetService.BottomSheetRestorationIdentifier;
        m_navigationViewController.ModalPresentationStyle = UIModalPresentationStyle.PageSheet;
        
        if (m_viewController.SheetPresentationController == null) //Can use bottom sheet
            return;
        
        if(m_bottomSheet.ShouldHaveNavigationBar)        
            ConfigureToolbar();
        
        m_sheetPresentationController = m_navigationViewController.SheetPresentationController;
        m_sheetPresentationController!.Delegate = new BottomSheetControllerDelegate(this);

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
                        double navBarHeight = 0;
                        if (m_bottomSheet.ShouldHaveNavigationBar)
                        {
                            var navigationController = m_viewController!.NavigationController;
                            navBarHeight = navigationController!.NavigationBar.Frame.Height;
                        }
                        
                        return (float)(r.Request.Height+m_bottomSheet.Padding.Bottom+m_bottomSheet.Padding.Top+navBarHeight);
                    });
                preferredDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Unknown;
            }
            else //Select large detent if the content is bigger than medium detent
            {
                if (m_bottomSheet.Content.Height > m_bottomSheet.Height / 2) //if the content is larger than half the screen when the detent is medium, it means that something is outside of bounds
                {
                    preferredDetent = UISheetPresentationControllerDetent.CreateLargeDetent();
                    preferredDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Large;
                }
            }
        }
        
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
        this.Padding = new Thickness(0, m_bottomSheet.ShouldHaveNavigationBar ? 0 : Sizes.GetSize(SizeName.size_4), 0,
            bottom); //Respect grabber and make sure we add some padding to the bottom, depending on if Safe Area (non physical home button) is visible.
    }
#pragma warning restore CA1416

    private void ConfigureToolbar()
    {
        Title = m_bottomSheet.Title;
        foreach (var item in m_bottomSheet.ToolbarItems)
        {
            item.BindingContext = m_bottomSheet.BindingContext;
            ToolbarItems.Add(item);
        }
        
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
        }
    }

    public async Task Close(bool animated)
    {
        if (m_navigationViewController == null) return;
        await m_navigationViewController.DismissViewControllerAsync(animated);
        await Task.Delay(100);
        Dispose();
    }

    internal void Opened()
    {
        m_bottomSheet.SendOpen();
    }
    
    internal void Dispose()
    {
        m_bottomSheet.SendClose();
        BottomSheetService.Current = null;
    }
}

internal class BottomSheetControllerDelegate : UISheetPresentationControllerDelegate
{
    private readonly BottomSheetContentPage m_bottomSheetContentPage;

    public BottomSheetControllerDelegate(BottomSheetContentPage bottomSheetContentPageContentPage)
    {
        m_bottomSheetContentPage = bottomSheetContentPageContentPage;
    }

    public override void WillPresent(UIPresentationController presentationController, UIModalPresentationStyle style,
        IUIViewControllerTransitionCoordinator? transitionCoordinator)
    {
        m_bottomSheetContentPage.Opened();
    }
    
    public override void DidDismiss(UIPresentationController presentationController)
    {
        m_bottomSheetContentPage.Dispose();
    }
}
