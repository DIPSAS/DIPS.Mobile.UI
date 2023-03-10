using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.iOS.Components.BottomSheets;
using DIPS.Mobile.UI.iOS.Components.Pickers;
using DIPS.Mobile.UI.iOS.Components.Pickers.Date;

namespace DIPS.Mobile.UI.iOS
{
    internal class iOSLibraryService : ILibraryService
    {
        public iOSLibraryService()
        {
            BottomSheetService.Instance = new iOSBottomSheetService();    
        }
        
        public async Task RemoveViewsLocatedOnTopOfPage()
        {
            if (BottomSheetService.Instance.IsBottomSheetOpen())
            {
                await BottomSheetService.Instance.CloseCurrentBottomSheet();    
            }

            if (DatePickerRenderer.IsOpen())
            {
                await DatePickerRenderer.Close();
            }
            
        }
    }
}