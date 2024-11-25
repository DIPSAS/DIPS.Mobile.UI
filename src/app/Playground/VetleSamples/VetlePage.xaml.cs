using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Resources.Icons;
using Playground.HåvardSamples;
using Shell = DIPS.Mobile.UI.Components.Shell.Shell;

namespace Playground.VetleSamples;

public partial class VetlePage
{
    public VetlePage()
    {
        InitializeComponent();
        TestCommand = new Command(SwitchRoot);

        
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        
        TestBool = BindingContext is VetlePageViewModel;
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

    public static readonly BindableProperty TestBoolProperty = BindableProperty.Create(
        nameof(TestBool),
        typeof(bool),
        typeof(VetlePage));

    public bool TestBool
    {
        get => (bool)GetValue(TestBoolProperty);
        set => SetValue(TestBoolProperty, value);
    }

    private void SwitchRoot()
    {
       

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(1000);

        /*Button.IsVisible = true;*/

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
            
        var insets = UIKit.UIApplication.SharedApplication.KeyWindow!.SafeAreaInsets;
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

    private void SwapRoot(object sender, EventArgs e)
    {
        SwapRoot(new DataTemplate(() => new MainPage()));
    }
    
    public static void SwapRoot(DataTemplate dataTemplate)
    {
        var tabBar = new TabBar();
        var tab = new Tab();

        tab.Items.Add(new ShellContent()
        {
            ContentTemplate =
                dataTemplate
        });
        tabBar.Items.Add(tab);
        Microsoft.Maui.Controls.Shell.Current.Items.RemoveAt(0);
        Microsoft.Maui.Controls.Shell.Current.Items.Add(tabBar);
    }

    private async void ListItem_OnTapped(object sender, EventArgs e)
    {
        await Task.Delay(2000);
        DialogService.ShowMessage("Hllo", "Hello", "OK");
    }
}