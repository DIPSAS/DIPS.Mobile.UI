using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;
using SkiaSharp.Extended.UI.Controls.Themes;

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
        
        public static void EnsureSkLottieResourcesAdded()
        {
            // try register with the current app
            var merged = Application.Current?.Resources?.MergedDictionaries;
            if (merged == null)
            {
                return;
            }

            if (merged.All(dic => dic.GetType() != typeof(SKLottieViewResources)))
            {
                merged.Add(new SKLottieViewResources());
            }
        }

        private static partial void RemovePlatformSpecificViewsLocatedOnTopOfPage();

    }
}
