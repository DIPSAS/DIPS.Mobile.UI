using DIPS.Mobile.UI.API.Builder;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Extensions.Logging;

namespace MemoryLeakTests;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseDIPSUI(options => options.EnableAutomaticModalHandlerDisconnection())
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
        DUI.IsDebug = true;
        DUI.ShouldLogDebug = true;
#endif
        //Handlers
        builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler(typeof(MyView), typeof(MyViewHandler));
        });

        return builder.Build();
    }
}