using System.ComponentModel;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.MemoryManagement;
using LightInject;
using MemoryLeakTests.Tests;

namespace MemoryLeakTests;

public partial class App
{
    public App()
    {
        InitializeComponent();

        Container = new ServiceContainer(options => options.EnablePropertyInjection = false);
        Container.Register<MainPage>();
        TestRegistrator.Register(Container);
        
        var shell = new DIPS.Mobile.UI.Components.Shell.Shell()
        {
            ShouldGarbageCollectPreviousPage = true
        };
        var tabBar = new TabBar();
        var tab = new Tab();
        
        tab.Items.Add(new ShellContent()
        {
            ContentTemplate =
                new DataTemplate(() => Container.GetInstance<MainPage>())

        });
        tabBar.Items.Add(tab);
        shell.Items.Add(tabBar);
        MainPage = shell;
    }

    public static ServiceContainer Container { get; set; }
}