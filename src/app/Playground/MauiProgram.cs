using DIPS.Mobile.UI;
using DIPS.Mobile.UI.API.Builder;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace Playground;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseDIPSUI();
        
        builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Bold.ttf", "Header");
            fonts.AddFont("OpenSans-Medium.ttf", "UI");
            fonts.AddFont("OpenSans-Regular.ttf", "Body");
        });
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}