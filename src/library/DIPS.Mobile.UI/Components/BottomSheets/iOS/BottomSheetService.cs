using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.BottomSheets.iOS;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;
using Page = Microsoft.Maui.Controls.Page;
using UIModalPresentationStyle = UIKit.UIModalPresentationStyle;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.BottomSheets;

public static partial class BottomSheetService
{
    internal const string BottomSheetRestorationIdentifier = nameof(BottomSheetContentPage);
    internal static BottomSheet? Current { get; set; }

    public static partial Task OpenBottomSheet(BottomSheet bottomSheet)
    {
        if (IsBottomSheetOpen())
        {
            CloseCurrentBottomSheet();
        }

        Current = bottomSheet;

        return Open(bottomSheet);
    }

    internal async static Task Open(BottomSheet bottomSheet)
    {
        try
        {
            var mauiContext = DUI.GetCurrentMauiContext;
            if (mauiContext == null) return;


            var page = new ContentPage();
            NavigationPage? navigationPage = null;
            if (bottomSheet.ShouldHaveNavigationBar)
            {
                navigationPage = new NavigationPage(page);
                navigationPage.SetAppThemeColor(NavigationPage.BarBackgroundColorProperty, BottomSheet.BackgroundColorName);
                navigationPage.SetAppThemeColor(NavigationPage.BarTextColorProperty, BottomSheet.ToolbarTextColorName);
            }

            var uiViewController = page.ToUIViewController(mauiContext);
            UIViewController? navigationController;
            if (navigationPage != null)
            {
                navigationController = navigationPage.ToUIViewController(mauiContext);
                navigationController.RestorationIdentifier = BottomSheetRestorationIdentifier;
                navigationController.ModalPresentationStyle = UIModalPresentationStyle.PageSheet;
            
                if (uiViewController!.NavigationController == null) return;
                var navController = uiViewController.NavigationController;
            
                if (navController?.NavigationBar == null) return;
                navController.NavigationBar.ShadowImage = new UIImage();
                navController.NavigationBar.SetBackgroundImage(new UIImage(), default);
                // Sets the color for all navigation buttons
                navController.NavigationBar.TintColor = Colors.GetColor(BottomSheet.ToolbarActionButtonsName).ToPlatform();
            }
            else
            {
                navigationController = page.ToUIViewController(mauiContext);
            }

            var uiSheetPresentationController = navigationController.SheetPresentationController;
            if (uiSheetPresentationController == null) return;
        
            var currentViewController = Platform.GetCurrentUIViewController();
            if (currentViewController == null) return;

            bottomSheet.UIViewController = uiViewController;
            bottomSheet.NavigationController = navigationController;
            bottomSheet.UISheetPresentationController = uiSheetPresentationController;
            bottomSheet.ContentPage = page;
            page.Content = bottomSheet;
            if (bottomSheet.Handler is BottomSheetHandler bottomSheetHandler)
            {
                bottomSheetHandler.OnBeforeOpening();
            }
        
            await currentViewController.PresentViewControllerAsync(navigationController, true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async partial Task CloseCurrentBottomSheet(bool animated)
    {
        if (Current is { } bottomSheet)
        {
            await bottomSheet.NavigationController.DismissViewControllerAsync(animated);
            await Task.Delay(100);
            if (bottomSheet.Handler is not BottomSheetHandler bottomSheetHandler) return;
            bottomSheetHandler.Dispose();
        }
    }

    public static partial bool IsBottomSheetOpen()
    {
        return Current != null;
    }
}