namespace Components.ComponentsSamples.BarcodeScanning;

public partial class BarcodeScanningHubSamples
{
    public BarcodeScanningHubSamples()
    {
        InitializeComponent();
        BasicScannerItem.Command = new Command(() => OpenModal(new BarcodeScanningSample()));
        OverlayScannerItem.Command = new Command(() => OpenModal(new BarcodeOverlaySample()));
        TooltipScannerItem.Command = new Command(() => OpenModal(new BarcodeTooltipSample()));
        FeedbackScannerItem.Command = new Command(() => OpenModal(new BarcodeFeedbackSample()));
        CounterScannerItem.Command = new Command(() => OpenModal(new BarcodeCounterSample()));
    }

    private static void OpenModal(Page page)
    {
        Shell.Current.Navigation.PushModalAsync(new NavigationPage(page));
    }
}
