using DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingNavigationButton;
using DIPS.Mobile.UI.Resources.Icons;

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
            config.AddNavigationButton(string.Empty, "Button 1", IconName.arrow_right_s_line, new Command(() => {}));
            config.AddNavigationButton(string.Empty, "Button 2", IconName.ascending_fill, new Command(() => { }));
            config.AddNavigationButton(string.Empty, "Button 3",  IconName.ascending_fill, new Command(() => { }));
            config.AddNavigationButton(string.Empty, "Button 4", IconName.descending_fill, new Command(() => { }));
        });
    }
}