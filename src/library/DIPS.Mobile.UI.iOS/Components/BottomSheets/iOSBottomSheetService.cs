using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheets;
using UIKit;

namespace DIPS.Mobile.UI.iOS.Components.BottomSheets
{
    internal class iOSBottomSheetService : IBottomSheetService
    {
        internal const string BottomSheetRestorationIdentifier = nameof(BottomSheetContentPage); 
        public Task OpenBottomSheet(BottomSheet bottomSheet) => new BottomSheetContentPage(bottomSheet).Open();
        public async Task CloseCurrentBottomSheet()
        {
            var potentialBottomSheetUiViewController = GetCurrentBottomSheetUiViewController();
            if (potentialBottomSheetUiViewController != null)
            {
                await potentialBottomSheetUiViewController.DismissViewControllerAsync(false);
                await Task.Delay(100); //Small delay before its actually removed.    
            }
        }

        public bool IsBottomSheetOpen()
        {
            return GetCurrentBottomSheetUiViewController() != null;
        }

        private static UIViewController? GetCurrentBottomSheetUiViewController()
        {
            var currentPresentedUiViewController = DUI.CurrentViewController;
            return currentPresentedUiViewController?.RestorationIdentifier == BottomSheetRestorationIdentifier ? currentPresentedUiViewController : null;
        }
    }
}