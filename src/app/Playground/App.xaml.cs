using DIPS.Mobile.UI.Resources.Icons;
using FloatingNavigationButtonService = DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton.FloatingNavigationButtonService;

namespace Playground;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        
        FloatingNavigationButtonService.AddFloatingNavigationButton(config =>
        {
            config.AddNavigationButton(string.Empty, "Innstillinger", IconName.arrow_right_s_line, new Command(() => {}));
            config.AddNavigationButton(string.Empty, "Pasient", IconName.ascending_fill, new Command(() => { }));
            config.AddNavigationButton(string.Empty, "Labsvar",  IconName.ascending_fill, new Command(() => { }));
            config.AddNavigationButton(string.Empty, "Timebok", IconName.descending_fill, new Command(() => { }));
        });
    }
}