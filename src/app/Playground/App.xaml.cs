using DIPS.Mobile.UI.Resources.Icons;
using Playground.HåvardSamples;
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
            config.AddNavigationButton(string.Empty, "A button", IconName.comment_line, new Command(() => {}));
            config.AddNavigationButton(string.Empty, "Another button", IconName.bell_line, new Command(() => { }));
            config.AddNavigationButton(string.Empty, "This button?",  IconName.alert_fill, new Command(() => { }));
            config.AddNavigationButton(string.Empty, "No button", IconName.close_line, new Command(() => { }));
        });
    }
}