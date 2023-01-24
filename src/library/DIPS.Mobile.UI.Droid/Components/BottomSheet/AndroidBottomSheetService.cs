using System.Threading.Tasks;
using Android.Content;
using DIPS.Mobile.UI.Components.BottomSheet;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace DIPS.Mobile.UI.Droid.Components.BottomSheet
{
    internal class AndroidBottomSheetService : IBottomSheetService
    {
        public async Task<IBottomSheet> PushBottomSheet(BottomSheetView bottomSheetView)
        {
            var bottomSheet = new BottomSheet(bottomSheetView);
            await bottomSheet.Open();
            return bottomSheet;
        }
    }
}