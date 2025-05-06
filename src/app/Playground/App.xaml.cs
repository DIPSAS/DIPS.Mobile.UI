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
        var tabBar = new TabBar() { Route = "app" };
        var tab = new Tab() { Route = "tab1", Title = "Main", Icon = Icons.GetIcon(IconName.mail_open_line) };
        var tab2 = new Tab() { Route = "tab2", Title = "Test", Icon = Icons.GetIcon(IconName.placeholdericon_fill)};
        
        tab.Items.Add(new ShellContent()
        {
            ContentTemplate =
                new DataTemplate(() => new MainPage())
        });
        tab2.Items.Add(new ShellContent()
        {
            ContentTemplate =
                new DataTemplate(() => new TestTab())
        });
        tabBar.Items.Add(tab);
        tabBar.Items.Add(tab2);
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