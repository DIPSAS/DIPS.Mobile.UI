using DIPS.Mobile.UI.API.Camera.BarcodeScanning;

namespace Components.ComponentsSamples.BarcodeScanning;

public partial class BarcodeScanningSample
{
    private readonly BarcodeScanner m_barcodeScanner;

    public BarcodeScanningSample()
    {
        InitializeComponent();
        m_barcodeScanner = new BarcodeScanner();
    }

    private async void StartScanning(object? sender, EventArgs e)
    {
        await Start();
    }

    private async Task Start()
    {
        try
        {
            await m_barcodeScanner.Start(Preview, barcode =>
            {
                m_barcodeScanner.Stop();
                Application.Current.MainPage.DisplayAlert("Woah!", barcode.RawValue, "Ok");
            });
        }
        catch (Exception exception)
        {
            Application.Current.MainPage.DisplayAlert("Failed, check console!", exception.Message, "Ok");
            Console.WriteLine(exception);
        }
    }

    private void StopScanning(object? sender, EventArgs e)
    {
        m_barcodeScanner.Stop();
    }

    protected override void OnDisappearing()
    {
        m_barcodeScanner.Stop();
        base.OnDisappearing();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}