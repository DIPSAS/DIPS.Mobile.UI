using DIPS.Mobile.UI.Components.BottomSheets;

namespace DIPS.Mobile.UI.API.Library
{
    public static partial class DUI
    {
        public static void Init(){}
        public static IMauiContext? GetCurrentMauiContext => Application.Current?.MainPage?.Window.Handler.MauiContext;

        public static void RemoveViewsLocatedOnTopOfPage()
        {
            if (BottomSheetService.IsBottomSheetOpen())
            {
                _ = BottomSheetService.CloseCurrentBottomSheet(false);    
            }
            
            RemovePlatformSpecificViewsLocatedOnTopOfPage();
        }

        private static partial void RemovePlatformSpecificViewsLocatedOnTopOfPage();

    }
}
