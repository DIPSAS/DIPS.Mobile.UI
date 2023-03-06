using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Droid.Components.BottomSheets;
using DatePickerRenderer = DIPS.Mobile.UI.Droid.Components.Pickers.DatePickerRenderer;

namespace DIPS.Mobile.UI.Droid
{
    internal class AndroidLibraryService : ILibraryService
    {
        public AndroidLibraryService()
        {
            BottomSheetService.Instance = new AndroidBottomSheetService();    
        }
        
        public async Task RemoveViewsLocatedOnTopOfPage()
        {
            if (BottomSheetService.Instance.IsBottomSheetOpen())
            {
                await BottomSheetService.Instance.CloseCurrentBottomSheet();    
            }

            if (DatePickerRenderer.IsOpen())
            {
                DatePickerRenderer.Close();
            }
        }
    }
}