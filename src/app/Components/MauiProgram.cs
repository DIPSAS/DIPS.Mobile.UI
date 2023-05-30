using Components.ComponentsSamples.Chips;
using DIPS.Mobile.UI.Components.FloatingActionButton.FloatingActionButtonMenu.NavigationMenuButton;
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
            .UseDIPSUI()
            .UseNavigationFab(pagesNotContaining =>
            {
                pagesNotContaining.Add(typeof(ChipsSamples));
            }, navigationMenuButtons =>
            {
                navigationMenuButtons.Add(new NavigationMenuButton
                {
                    Title = "Hei"
                });
                navigationMenuButtons.Add(new NavigationMenuButton
                {
                    Title = "Hei",
                    Icon = Icons.GetIcon(IconName.ascending_fill),
                    Command = new Command(_ => Shell.Current.DisplayAlert("Clicked!", "OK", "OK"))
                });
            });
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}