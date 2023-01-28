using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    public static class BottomSheetExtensions
    {
        public static Task  PushBottomSheet(this Application app, BottomSheet bottomSheet)
        {
            if (BottomSheetService.Instance != null)
            {
                return BottomSheetService.Instance.PushBottomSheet(bottomSheet);
            }

            throw new Exception("Looks like BottomSheetService.Instance is not set, this means that DUI.Init has not been run in the platform. Please see Getting Started wiki for further instructions.");
        }
    }
}