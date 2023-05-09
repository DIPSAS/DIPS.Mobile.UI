using Components.ComponentsSamples;
using Components.ComponentsSamples.BottomSheets;
using Components.ComponentsSamples.ContextMenus;
using Components.ComponentsSamples.Pickers;
using Components.ComponentsSamples.Searching;
using Components.Resources.LocalizedStrings;
using Components.ResourcesSamples;
using Components.ResourcesSamples.Colors;
using Components.ResourcesSamples.Sizes;
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
        tab.Items.Add(new ShellContent()
        {
            ContentTemplate =
                new DataTemplate(() => new MainPage(new List<SampleType> {SampleType.Resources, SampleType.Components,},
                    REGISTER_YOUR_SAMPLES_HERE.RegisterSamples()))
        });
        tabBar.Items.Add(tab);
        shell.Items.Add(tabBar);
        MainPage = shell;

    }
    
}