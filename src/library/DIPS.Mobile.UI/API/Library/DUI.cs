using DIPS.Mobile.UI.Components.BottomSheets;

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

            // if (DatePickerRenderer.IsOpen())
            // {
            //     DatePickerRenderer.Close();
            // }
        }

    }
}
