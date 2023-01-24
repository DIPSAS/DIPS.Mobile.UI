using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheet;
using UIKit;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.iOS.Components.BottomSheet
{
    internal class iOSBottomSheetService : IBottomSheetService
    {
        public async Task<IBottomSheet> PushBottomSheet(BottomSheetView bottomSheetView)
        {
            var bottomSheet = new BottomSheet(bottomSheetView);
            await bottomSheet.Open();
            return bottomSheet;
        }
    }
}