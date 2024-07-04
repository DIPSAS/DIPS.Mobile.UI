using System.Windows.Input;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;
using DIPS.Mobile.UI.Components.Pages;
using DIPS.Mobile.UI.Resources.Icons;
using Microsoft.Maui.Controls.Internals;
using Playground.HåvardSamples;
using UIKit;
using Shell = DIPS.Mobile.UI.Components.Shell.Shell;

namespace Playground.VetleSamples;

public partial class VetlePage
{
    public VetlePage()
    {
        InitializeComponent();
        TestCommand = new Command(SwitchRoot);
    }

    private void Test123()
    {
        var tabBar = new TabBar {Route = "app"};

        tabBar.Items.Add(new Tab
        {
            Title = "Test",
            Icon = Icons.GetIcon(IconName.alert_fill),
            Items =
            {
                new ShellContent
                {
                    ContentTemplate = new DataTemplate(() => new VetleTestPage1())
                }
            }
        });

        Shell.Current.Items.Clear();
        Shell.Current.Items.Add(tabBar);

        _ = Shell.Current.GoToAsync("//app");
    }

    private void SwitchRoot()
    {
       

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(4000);
        
        /*
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
        });*/
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


    private void Entry_OnCompleted(object sender, EventArgs e)
    {
        
    }
    
    private void VisualElement_OnLoaded(object? sender, EventArgs e)
    {
#if __IOS__

        if (sender is not Grid grid) return;
            
        var insets = UIApplication.SharedApplication.KeyWindow!.SafeAreaInsets;
        grid.Padding = new Thickness(grid.Padding.Left, insets.Top, grid.Padding.Right, grid.Padding.Bottom + insets.Bottom);

#endif
    }

    private void SetSemanticFocus(object? sender, EventArgs e)
    {
        /*SignInButton.SetSemanticFocus();*/
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        Test123();
    }
}