using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheets;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace DIPS.Mobile.UI.iOS.Components.BottomSheets
{
    internal class iOSBottomSheetService : IBottomSheetService
    {
        public Task PushBottomSheet(BottomSheet bottomSheet) => new SheetContentPage(bottomSheet).Open();
    }
}