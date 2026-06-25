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

    private async void GoToModalNavigationBarBackNavigationSample(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushModalAsync(new NavigationPage(new ModalNavigationBarBackNavigationSample.ModalNavigationBarBackNavigationSample()));
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

    private void GoToAlertViewRepro(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new HåvardSamples.AlertViewReproPage());
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

    private void GoToItemPickerRefreshRepro(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new ItemPickerRefreshRepro());
    }

    private void GoToPatientListMenuFlingRepro(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new PatientListMenuFlingReproPage());
    }

    private void GoToDatePickerFlingRepro(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new DatePickerFlingReproPage());
    }

    private void GoToCollectionViewTests(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new CollectionViewTests());
    }

    private void GoToBarcodeScanResumeRepro(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new BarcodeScanResumeRepro());
    }

    private void GoToBarcodeScanResumeReproModal(object sender, EventArgs e)
    {
        var scannerPage = new BarcodeScanResumeRepro();
        var navigationPage = new NavigationPage(scannerPage);
        scannerPage.ToolbarItems.Add(new ToolbarItem
        {
            Text = "Close",
            Command = new Command(() => Shell.Current.Navigation.PopModalAsync())
        });
        Shell.Current.Navigation.PushModalAsync(navigationPage);
    }

    private void GoToSamplingPatientScanRepro(object sender, EventArgs e)
    {
        var scannerPage = new SamplingPatientScanRepro();
        var navigationPage = new NavigationPage(scannerPage);
        Shell.Current.Navigation.PushModalAsync(navigationPage);
    }
}