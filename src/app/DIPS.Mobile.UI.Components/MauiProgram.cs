using Microsoft.Extensions.Logging;

namespace DIPS.Mobile.UI.Components;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseDIPSUI();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}