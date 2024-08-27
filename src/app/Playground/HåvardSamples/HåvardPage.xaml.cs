using DIPS.Mobile.UI.Components.Alerting.Dialog;

namespace Playground.HåvardSamples;

public partial class HåvardPage
{
    private readonly HåvardPageViewModel m_håvardPageViewModel;

    public HåvardPage()
    {
        InitializeComponent();
        m_håvardPageViewModel = BindingContext as HåvardPageViewModel;
        // m_håvardPageViewModel.PropertyChanged += (sender, args) =>
        // {
        //     if (args.PropertyName == "LongText")
        //     {
        //         truncatingLabel.ForceUpdateText(m_håvardPageViewModel.LongText);
        //     }
        // };
    }
    
    private void ContextMenuItem_OnDidClick(object sender, EventArgs e)
    {
        DialogService.ShowMessage("You tapped it", "yey!", "Ok");
    }

    private void ModalNavigation(object sender, EventArgs e)
    {
        var navigationpage = new NavigationPage(new SecondRootPage()
        {
            AutomationId = "First Modal Page",
            BindingContext = new Something("First Modal", new SomeOtherThing(true)),
            Content = new Button(){Text = "Nav next", Command = new Command(() =>
            {
                var secondModalPage = new SecondRootPage()
                {
                    AutomationId = "Second Modal Page",
                    BindingContext = new Something("Second Modal", new SomeOtherThing(true)),
                    Content = new Button(){Text = "Last, drop it like its hot", Command = new Command(() =>
                    {
                        var navigationPage =
                            Shell.Current.Navigation.ModalStack.FirstOrDefault(p => p is NavigationPage) as
                                NavigationPage;
                        var stack = navigationPage.Navigation.NavigationStack;
                        var pages = stack.Take(stack.Count - 1).ToList();
                        foreach (var page in pages)
                        {
                            navigationPage.Navigation
                                .RemovePage(page); // remove all pages except the one currently showing
                        }

                        Shell.Current.Navigation.PopModalAsync();
                    })}
                };
                
                Shell.Current.Navigation.PushModalAsync(secondModalPage);
            })}
        });
        Shell.Current.Navigation.PushModalAsync(navigationpage);
    }

    private void Modal(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushModalAsync(new ContentPage() {Content = new Label() {Text = "Normal page"}});
    }

    private void SwapRoot(object sender, EventArgs e)
    {
        SwapRoot(new DataTemplate(() => new SecondRootPage()));
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
        Shell.Current.Items.RemoveAt(0);
        Shell.Current.Items.Add(tabBar);
    }
}

public class SecondRootPage : ContentPage
{
    public SecondRootPage()
    {
        BindingContext = new Something("Second Modal", new SomeOtherThing(true));
        Content = new Button()
        {
            Text = "Swap again",
            Command = new Command(() => HåvardPage.SwapRoot(new DataTemplate(() => new MainPage())))
        };
        // DeviceDisplay.MainDisplayInfoChanged += DeviceDisplayOnMainDisplayInfoChanged;
    }

    private void DeviceDisplayOnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        
    }
}