namespace Components.ComponentsSamples.BarcodeScanning;

public partial class BarcodeScanningHubSamples
{
    public BarcodeScanningHubSamples()
    {
        InitializeComponent();
        BasicScannerItem.Command = new Command(() => OpenModal(new BarcodeScanningSample()));
        OverlayScannerItem.Command = new Command(() => OpenModal(new BarcodeOverlaySample()));
    }

    private static void OpenModal(Page page)
    {
        Shell.Current.Navigation.PushModalAsync(new NavigationPage(page));
    }
}
