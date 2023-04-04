using Microsoft.Maui.LifecycleEvents;

namespace DIPS.Mobile.UI.Components;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        var shell = new Shell.Shell();
        var tabBar = new TabBar();
        var tab = new Tab();
        tab.Items.Add(new ShellContent(){ContentTemplate = new DataTemplate(() => new MainPage())});
        tabBar.Items.Add(tab);
        shell.Items.Add(tabBar);
        MainPage = shell;
    }
}