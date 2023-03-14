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
            BottomSheetService.Current = new AndroidBottomSheetService();    
        }
        
        public async Task RemoveViewsLocatedOnTopOfPage()
        {
            if (BottomSheetService.Current.IsBottomSheetOpen())
            {
                await BottomSheetService.Current.CloseCurrentBottomSheet();    
            }

            if (DatePickerRenderer.IsOpen())
            {
                DatePickerRenderer.Close();
            }
        }
    }
}