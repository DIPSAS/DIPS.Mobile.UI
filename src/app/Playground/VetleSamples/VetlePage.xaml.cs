using System.Windows.Input;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.API.Library.Android;
using DIPS.Mobile.UI.API.Tip;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.Lists;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Icons;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Playground.HåvardSamples;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Shell = DIPS.Mobile.UI.Components.Shell.Shell;

namespace Playground.VetleSamples;

public partial class VetlePage
{
    public VetlePage()
    {
        InitializeComponent();
        TestCommand = new Command(SwitchRoot); 

        _ = FuckThisShitUp();
        /*Shell.OnTabTapped += ShellOnOnTabTapped;*/

    }

    private void ShellOnOnTabTapped(string obj)
    {
        _ = DialogService.ShowMessage(config =>
        {
            config.SetTitle(obj);
        });
    }

    private async Task FuckThisShitUp()
    {
        var thread = new Thread (() => {
            while (true) {
                Thread.Sleep (5000);
                GC.Collect();
            }
        }) { IsBackground = true };
        thread.Start();
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
            Icon = Icons.GetIcon(IconName.alert_line),
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

    private string m_test1 = "Test123";
    private double? m_originalHeight;
    private bool m_ignoreScrollEvent;
    private bool m_firstTime;

    public bool TestBool
    {
        get => (bool)GetValue(TestBoolProperty);
        set => SetValue(TestBoolProperty, value);
    }

    private void SwitchRoot()
    {
        

    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

#if __IOS__
        var test = Platform.GetCurrentUIViewController();
#endif

        /*if(!m_firstTime)
            */
        m_firstTime = true;
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
        
        
        /*ContentControl.SelectorItem = e.Value;*/
        
        
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

    private void VisualElement_OnSizeChanged(object sender, EventArgs e)
    {
        m_ignoreScrollEvent = true;
    }

    public string Test
    {
        get => m_test1;
        set
        {
            if (value == m_test1)
            {
                return;
            }

            m_test1 = value;
            OnPropertyChanged();
        }
    }


    private void SearchBar_OnUnfocused(object sender, EventArgs e)
    {
        
    }

    private void ListItem_OnTapped(object sender, EventArgs e)
    {
        /*TipService.Show("Test", sender as View);*/
        TipService.Show("This is a test!", sender as View);

    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        _ = Navigation.PushModalAsync(new NavigationPage(new VetleTestPage1()));
    }

    private void Button_OnClicked2(object sender, EventArgs e)
    {
        _ = Navigation.PushModalAsync(new VetleTestPage1());
    }

    private void Button_OnClicked3(object sender, EventArgs e)
    {
        _ = Navigation.PushAsync(new VetleTestPage1());
    }

    private void Button_OnClicked4(object sender, EventArgs e)
    {
        StatusBarHandler.StatusBarColorOverride = Microsoft.Maui.Graphics.Colors.Aqua;
    }

    private void Button_OnClicked5(object sender, EventArgs e)
    {
        _ = Navigation.PushAsync(new VetleTestPage2());
    }

    private void Button_OnClicked6(object sender, EventArgs e)
    {
        StatusBarHandler.StatusBarColorOverride = null;
    }
}