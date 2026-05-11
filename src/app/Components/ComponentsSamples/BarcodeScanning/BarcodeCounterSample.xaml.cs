using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning;

namespace Components.ComponentsSamples.BarcodeScanning;

public partial class BarcodeCounterSample
{
    private const int RequiredScanCount = 5;
    private readonly BarcodeScanner m_barcodeScanner;
    private readonly BarcodeScanningSettings m_barcodeScanningSettings;
    private readonly HashSet<string> m_acceptedBarcodes = [];

    public BarcodeCounterSample()
    {
        InitializeComponent();
        m_barcodeScanner = new BarcodeScanner();
        m_barcodeScanningSettings = new BarcodeScanningSettings
        {
            RequiredScanCount = RequiredScanCount,
            ShowScanRectangle = true,
            ScanRectangleWidthFraction = 0.8f,
            ScanRectangleHeightFraction = 0.3f,
            OnValidBarcodeScanned = DidFindBarcode,
            ValidateBarcodeAsync = ValidateBarcodeAsync,
            OnInvalidBarcodeScannedAsync = OnInvalidBarcodeScannedAsync,
            OnRequiredScanCountCompletedAsync = OnRequiredScanCountCompletedAsync
        };
    }

    private async Task Start()
    {
        try
        {
            await m_barcodeScanner.Start(CameraPreview, CameraFailed, m_barcodeScanningSettings);
        }
        catch (Exception exception)
        {
            await Application.Current?.MainPage?.DisplayAlert("Failed, check console!", exception.Message, "Ok")!;
            Console.WriteLine(exception);
        }
    }

    private void CameraFailed(CameraException e)
    {
        App.Current.MainPage.DisplayAlert("Something failed!", e.Message, "Ok");
    }

    private void DidFindBarcode(BarcodeScanResult barcodeScanResult)
    {
        m_acceptedBarcodes.Add(barcodeScanResult.Barcode.RawValue);
        Console.WriteLine($"Accepted barcode: {barcodeScanResult.Barcode.RawValue}");
    }

    private async Task<BarcodeScanValidationResult> ValidateBarcodeAsync(string barcode)
    {
        await Task.Delay(150);

        if (string.IsNullOrWhiteSpace(barcode))
        {
            return BarcodeScanValidationResult.Invalid();
        }

        return m_acceptedBarcodes.Contains(barcode)
            ? BarcodeScanValidationResult.Invalid()
            : BarcodeScanValidationResult.Valid();
    }

    private Task OnInvalidBarcodeScannedAsync(BarcodeScanResult barcodeScanResult, BarcodeScanValidationResult validationResult)
    {
        Console.WriteLine($"Rejected barcode: {barcodeScanResult.Barcode.RawValue}. {validationResult.ErrorMessage}");
        return Task.CompletedTask;
    }

    private Task OnRequiredScanCountCompletedAsync()
    {
        Console.WriteLine("Required barcode scan count completed.");
        return Task.CompletedTask;
    }

    protected override void OnAppearing()
    {
        _ = Start();
        base.OnAppearing();
    }

    private void Close(object? sender, EventArgs e)
    {
        m_barcodeScanner.StopAndDispose();
        Shell.Current.Navigation.PopModalAsync();
    }
}
