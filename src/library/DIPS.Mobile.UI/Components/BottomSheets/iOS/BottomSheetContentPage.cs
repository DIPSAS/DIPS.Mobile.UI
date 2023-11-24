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

    private readonly BottomSheet m_bottomSheet;

    // public UIViewController UINavigationController { get; set; }
    public UIViewController? UIViewController { get; set; }
    public UISheetPresentationController? UISheetPresentationController { get; set; }

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

    public static async Task CreateAndShowBottomSheet(bool shouldHaveNavigationBar)
    {
        var mauiContext = DUI.GetCurrentMauiContext;
        if (mauiContext == null) return;
    
        
        Page page = new ContentPage();
        NavigationPage? navigationPage = null;
        if (shouldHaveNavigationBar)
        {
            navigationPage = new NavigationPage(page);
            navigationPage.SetAppThemeColor(NavigationPage.BarBackgroundColorProperty, BottomSheet.BackgroundColorName);
            navigationPage.SetAppThemeColor(NavigationPage.BarTextColorProperty, BottomSheet.ToolbarTextColorName);
            
        }

        var uiViewController  = page.ToUIViewController(mauiContext);
        UIViewController? navigationController; 
        if (navigationPage != null)
        {
            navigationController = navigationPage.ToUIViewController(mauiContext);
            navigationController.RestorationIdentifier = BottomSheetService.BottomSheetRestorationIdentifier;
            navigationController.ModalPresentationStyle = UIModalPresentationStyle.PageSheet;
            ConfigureToolbar(uiViewController);
        }
        else
        {
            navigationController = page.ToUIViewController(mauiContext);
        }
        
        var uiSheetPresentationController = navigationController.SheetPresentationController;
        
        //TODO: MOVE TO HANDLER:
        //uiSheetPresentationController!.Delegate = new BottomSheetControllerDelegate(this);
        var currentViewController = Platform.GetCurrentUIViewController();
        if (currentViewController == null) return;
        await currentViewController.PresentViewControllerAsync(navigationController, true);
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
        // if (!OperatingSystem.IsIOSVersionAtLeast(15))
        // {
        //     return;
        // }
        //
        // var mauiContext = DUI.GetCurrentMauiContext;
        //
        // if (mauiContext == null)
        //     return;
        //
        // UIViewController = this.ToUIViewController(mauiContext);
        //
        // Page navigationPage;
        //
        // if (m_bottomSheet.ShouldHaveNavigationBar)
        // {
        //     navigationPage = new NavigationPage(this);
        //     navigationPage.SetAppThemeColor(NavigationPage.BarBackgroundColorProperty, BottomSheet.BackgroundColorName);
        //     navigationPage.SetAppThemeColor(NavigationPage.BarTextColorProperty, BottomSheet.ToolbarTextColorName);
        // }
        // else
        // {
        //     navigationPage = this;
        // }
        //
        // UINavigationController = navigationPage.ToUIViewController(mauiContext);
        //
        // if (UIViewController == null) 
        //     return;
        //
        // UINavigationController.RestorationIdentifier = BottomSheetService.BottomSheetRestorationIdentifier;
        // UINavigationController.ModalPresentationStyle = UIModalPresentationStyle.PageSheet;
        //
        // if (UIViewController.SheetPresentationController == null) //Can use bottom sheet
        //     return;
        //
        //
        // if(m_bottomSheet.ShouldHaveNavigationBar)        
        //     ConfigureToolbar();
        //
        // UISheetPresentationController = UINavigationController.SheetPresentationController;
        // UISheetPresentationController!.Delegate = new BottomSheetControllerDelegate(this);

        //Todo: Kan muligens flyttes til handler
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
                            var navigationController = UIViewController!.NavigationController;
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
        
        UISheetPresentationController.Detents = (m_bottomSheet.ShouldFitToContent)

            ? new[] {preferredDetent}
            : new[] {preferredDetent, UISheetPresentationControllerDetent.CreateLargeDetent(),};

        //Add grabber
        UISheetPresentationController.PrefersGrabberVisible = true;

        UISheetPresentationController.SelectedDetentIdentifier = preferredDetentIdentifier;

        UISheetPresentationController.PrefersScrollingExpandsWhenScrolledToEdge = true;
            

        var bottom = (UIApplication.SharedApplication.KeyWindow?.SafeAreaInsets.Bottom) == 0
                ? Sizes.GetSize(SizeName.size_4) //There is a physical home button
                : Sizes.GetSize(SizeName.size_1) //There is no phyiscal home button, but we need some air between the safe area and the content
            ;
        this.Padding = new Thickness(0, m_bottomSheet.ShouldHaveNavigationBar ? 0 : Sizes.GetSize(SizeName.size_4), 0,
            bottom); //Respect grabber and make sure we add some padding to the bottom, depending on if Safe Area (non physical home button) is visible.
    }
#pragma warning restore CA1416

    private static void ConfigureToolbar(UIViewController uiViewController)
    {
        //TODO: Move to handler
        // Title = m_bottomSheet.Title;
        // foreach (var item in m_bottomSheet.ToolbarItems)
        // {
        //     item.BindingContext = m_bottomSheet.BindingContext;
        //     ToolbarItems.Add(item);
        // }
        
        var navigationController = uiViewController!.NavigationController;
        if (navigationController == null) return;
        RemoveNavigationBarSeparator(navigationController.NavigationBar);
        // Sets the color for all navigation buttons
        navigationController.NavigationBar.TintColor = Colors.GetColor(BottomSheet.ToolbarActionButtonsName).ToPlatform(); 
    }

    private static void RemoveNavigationBarSeparator(UINavigationBar navigationBar)
    {
        navigationBar.ShadowImage = new UIImage();
        navigationBar.SetBackgroundImage(new UIImage(), default);
    }
}
