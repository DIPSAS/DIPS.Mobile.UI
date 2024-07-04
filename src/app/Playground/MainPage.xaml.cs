using DIPS.Mobile.UI.Resources.Icons;
using Playground.EirikSamples;
using Playground.HåvardSamples;
using Playground.SanderSamples;
using Playground.VetleSamples;

namespace Playground;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void GoToVetle(object sender, EventArgs e)
    {
        /*Shell.Current.Navigation.PushAsync(new VetlePage());*/
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

    private void GoToHåvard(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new HåvardPage());
    }

    private void GoToSander(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new SanderPage());
    }

    private void GoToEirik(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new EirikPage());

    }

    private async void TapMe(object sender, EventArgs e)
    {
        var tabBar = new TabBar {Route = $"root1"};

        tabBar.Items.Add(new Tab
        {
            Items =
            {
                new ShellContent
                {
                    ContentTemplate = new DataTemplate(() => new ContentPage(){Content = new Label()
                    {
                        Text = "Hello"
                    }})
                }
            }
        });
            
        Shell.Current.Items.Clear();
        Shell.Current.Items.Add(tabBar);

        await Shell.Current.GoToAsync($"//root1", true);
    }
}