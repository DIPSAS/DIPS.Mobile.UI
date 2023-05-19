using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Pickers;
using DIPS.Mobile.UI.Components.Pickers.DatePicker;
using DIPS.Mobile.UI.Components.Pickers.TimePicker;

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
            // Not yet implemented for iOS
            /*if (DatePickerService.IsOpen())
            { 
                _ = DateTimePickerService.Close();
            }*/
            
#elif __ANDROID__
            if (DatePickerService.IsOpen())
            {
                _ = DatePickerService.Close();
            }

            if (TimePickerService.IsOpen())
            {
                _ = TimePickerService.Close();
            }
#endif
            
        }

    }
}
