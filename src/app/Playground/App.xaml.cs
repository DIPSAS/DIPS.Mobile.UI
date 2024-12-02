using DIPS.Mobile.UI.Resources.Icons;
using Playground.HåvardSamples;
using Playground.VetleSamples;
using FloatingNavigationButtonService =
    DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton.FloatingNavigationButtonService;
using Shell = DIPS.Mobile.UI.Components.Shell.Shell;

namespace Playground;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var shell = new Shell();
        var tabBar = new TabBar();
        var tab = new Tab();

        tab.Items.Add(new ShellContent()
        {
            ContentTemplate =
                new DataTemplate(() => new MainPage())
        });
        tabBar.Items.Add(tab);
        shell.Items.Add(tabBar);

        return new Window(shell);
    }

    protected override void OnStart()
    {
        base.OnStart();

        FloatingNavigationButtonService.AddFloatingNavigationButton(config =>
        {
            config.AddNavigationButton(string.Empty, "A button", IconName.comment_line, new Command(() => { }));
            config.AddNavigationButton(string.Empty, "Another button", IconName.bell_line, new Command(() => { }));
            config.AddNavigationButton(string.Empty, "This button?", IconName.alert_fill, new Command(() => { }));
            config.AddNavigationButton(string.Empty, "No button", IconName.close_line, new Command(() => { }));
        });
    }
}