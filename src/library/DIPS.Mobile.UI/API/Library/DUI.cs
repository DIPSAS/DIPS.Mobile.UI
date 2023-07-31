using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;

namespace DIPS.Mobile.UI.API.Library
{
    public static partial class DUI
    {
        public static void Init()
        {
            PlatformInit();
        }

        private static partial void PlatformInit();
        
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
