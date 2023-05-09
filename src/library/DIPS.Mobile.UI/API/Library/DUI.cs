using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Pickers;

namespace DIPS.Mobile.UI
{
    public static class DUI
    {
        public static void Init(){}
        public static IMauiContext? GetCurrentMauiContext => Application.Current?.MainPage?.Window.Handler.MauiContext;

        public static async Task RemoveViewsLocatedOnTopOfPage()
        {
            if (BottomSheetService.IsBottomSheetOpen())
            {
                await BottomSheetService.CloseCurrentBottomSheet();    
            }
            
#if __IOS__
            /*if (DateTimePickerService.IsOpen())
            { 
                _ = DateTimePickerService.Close();
            }*/
            
#elif __ANDROID__
            /*if (DatePickerService.IsOpen())
            {
                DatePickerService.Close();
            }*/
#endif
            
        }

    }
}
