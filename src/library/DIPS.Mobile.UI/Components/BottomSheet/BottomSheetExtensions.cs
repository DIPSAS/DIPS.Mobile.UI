using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.BottomSheet
{
    public static class BottomSheetExtensions
    {
        public static Task<IBottomSheet> PushBottomSheet(this Application app, BottomSheetView bottomSheetView)
        {
            if (BottomSheetService.Instance != null)
            {
                return BottomSheetService.Instance.PushBottomSheet(bottomSheetView);
            }

            throw new Exception("Looks like BottomSheetService.Instance is not set, this means that DUI.Init has not been run in the platform. Please see Getting Started wiki for further instructions.");
        }
    }
}