using DIPS.Mobile.UI.Components.BottomSheets.iOS;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.BottomSheets;

public static partial class BottomSheetService
{
    internal const string BottomSheetRestorationIdentifier = nameof(BottomSheetContentPage); 
    public static partial Task OpenBottomSheet(BottomSheet bottomSheet) => new BottomSheetContentPage(bottomSheet).Open();
        
    public static async partial Task CloseCurrentBottomSheet(bool animated)
    {
        var potentialBottomSheetUiViewController = GetCurrentBottomSheetUiViewController();
        if (potentialBottomSheetUiViewController != null)
        {
            await potentialBottomSheetUiViewController.DismissViewControllerAsync(animated);
            await Task.Delay(100); //Small delay before its actually removed.    
        }
    }

    public static partial bool IsBottomSheetOpen()
    {
        return GetCurrentBottomSheetUiViewController() != null;
    }

    private static UIViewController? GetCurrentBottomSheetUiViewController()
    {
        var currentPresentedUiViewController = Platform.GetCurrentUIViewController();
        return currentPresentedUiViewController?.RestorationIdentifier == BottomSheetRestorationIdentifier ? currentPresentedUiViewController : null;
    }
}