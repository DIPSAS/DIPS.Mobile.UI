using DIPS.Mobile.UI.API.Builder;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.ContextMenus;
using Microsoft.Extensions.Logging;

namespace Playground;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseDIPSUI(options =>
            {
                options.HandleContextMenuGlobalClicks(OnContextMenuItemClicked);
                /*options.EnableAutomaticMemoryLeakResolving();*/
                options.EnableCustomHideSoftInputOnTapped();
            });
        
        builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Bold.ttf", "Header");
            fonts.AddFont("OpenSans-SemiBold.ttf", "UI");
            fonts.AddFont("OpenSans-Regular.ttf", "Body");
        });
        
#if DEBUG
        builder.Logging.AddDebug();
        DUI.IsDebug = true;
        DUI.ShouldLogDebug = true;
#endif
        return builder.Build();
    }

    private static void OnContextMenuItemClicked(GlobalContextMenuClickMetadata metadata)
    {
        Console.WriteLine($@"Clicked context menu item with title {metadata.ContextMenuItem.Title}!, menu: {metadata.ContextMenu.Mode}");
    }
}