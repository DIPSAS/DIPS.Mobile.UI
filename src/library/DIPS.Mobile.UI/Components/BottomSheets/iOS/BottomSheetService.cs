using DIPS.Mobile.UI.iOS.Components.BottomSheets;
using UIKit;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    public static partial class BottomSheetService
    {
        internal const string BottomSheetRestorationIdentifier = nameof(BottomSheetContentPage); 
        public static partial Task OpenBottomSheet(BottomSheet bottomSheet) => new BottomSheetContentPage(bottomSheet).Open();
        
        public static async partial Task CloseCurrentBottomSheet()
        {
            var potentialBottomSheetUiViewController = GetCurrentBottomSheetUiViewController();
            if (potentialBottomSheetUiViewController != null)
            {
                await potentialBottomSheetUiViewController.DismissViewControllerAsync(false);
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
}