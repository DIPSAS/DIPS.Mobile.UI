using System.Windows.Input;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;
using DIPS.Mobile.UI.Components.Pages;
using DIPS.Mobile.UI.Resources.Icons;
using Microsoft.Maui.Controls.Internals;
using Playground.HåvardSamples;

namespace Playground.VetleSamples;

public partial class VetlePage
{
    public VetlePage()
    {
        InitializeComponent();
        TestCommand = new Command(Test123);
    }

    private void Test123()
    {
        _ = Navigation.PushModalAsync(new NavigationPage(new VetleTestPage1()));
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(4000);
        
        ToolbarItems.Add(new ContextMenuToolbarItem()
        {
            IconImageSource = Icons.GetIcon(IconName.alert_fill),
            ContextMenu = new ContextMenu()
            {
                Title = "Test",
                ItemsSource = new List<IContextMenuItem>()
                {
                    new ContextMenuItem()
                    {
                        Title = "Test",
                        Command = TestCommand
                    }
                }
            }
        });
    }

    public ICommand TestCommand { get; }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        
        
    }




    private void Switch_OnToggled(object sender, ToggledEventArgs e)
    {
        ShouldHideFloatingNavigationMenuButton = e.Value;
    }

    
}