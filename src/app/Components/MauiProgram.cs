using System.Collections.ObjectModel;
using Components.ComponentsSamples.Chips;
using DIPS.Mobile.UI.Components.FloatingActionButtons.ExtendedFloatingActionButton;
using DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingActionButton;
using DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingNavigationButton;
using DIPS.Mobile.UI.Resources.Icons;
using Microsoft.Extensions.Logging;

namespace Components;

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

