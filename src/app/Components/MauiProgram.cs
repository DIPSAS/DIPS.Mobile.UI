using Microsoft.Extensions.Logging;

namespace Components;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseDIPSUI()
            .UseNavigationFab();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}