using System.Diagnostics;
using DIPS.Mobile.UI.Resources.Icons;
using Playground.EirikSamples;
using Playground.HåvardSamples;
using Playground.SanderSamples;
using Playground.VetleSamples;
using Playground.VetleSamples.CollectionViewTests;

namespace Playground;

public partial class MainPage
{
    public MainPage()
    {
        DeviceDisplay.MainDisplayInfoChanged += DeviceDisplayOnMainDisplayInfoChanged;
        InitializeComponent();
    }

    private void DeviceDisplayOnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        
    }

    private async void GoToVetle(object sender, EventArgs e)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        await Shell.Current.Navigation.PushAsync(new VetlePage());
        stopWatch.Stop();
        Console.WriteLine(stopWatch.ElapsedMilliseconds);
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
        Shell.Current.Navigation.PushModalAsync(new EirikPage());
        
        // Shell.Current.Navigation.PushAsync(new EirikPage());

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
                    ContentTemplate = new DataTemplate(() => new SecondRootPage())
                }
            }
        });
            
        Shell.Current.Items.Clear();
        Shell.Current.Items.Add(tabBar);

        await Shell.Current.GoToAsync($"//root1", true);
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is null)
        {
            
        }
    }

    private void GoToCollectionViewTests(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new CollectionViewTests());
    }
}