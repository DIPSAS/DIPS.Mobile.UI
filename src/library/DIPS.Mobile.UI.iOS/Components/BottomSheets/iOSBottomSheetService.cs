using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheets;

namespace DIPS.Mobile.UI.iOS.Components.BottomSheets
{
    internal class iOSBottomSheetService : IBottomSheetService
    {
        internal const string BottomSheetRestorationIdentifier = nameof(SheetContentPage); 
        public Task PushBottomSheet(BottomSheet bottomSheet) => new SheetContentPage(bottomSheet).Open();
        public async Task CloseCurrentBottomSheet()
        {
            var currentPresentedUiViewController = DUI.CurrentViewController;
            if (currentPresentedUiViewController.RestorationIdentifier == BottomSheetRestorationIdentifier)
            {
                await currentPresentedUiViewController.DismissViewControllerAsync(false);
                await Task.Delay(100); //Small delay before its actually removed.
            }
        }
    }
}