using Shell = DIPS.Mobile.UI.Components.Shell.Shell;

namespace Components;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        var shell = new Shell();
        var tabBar = new TabBar();
        var tab = new Tab();
        tab.Items.Add(new ShellContent(){ContentTemplate = new DataTemplate(() => new MainPage())});
        tabBar.Items.Add(tab);
        shell.Items.Add(tabBar);
        MainPage = shell;

    }
    
}