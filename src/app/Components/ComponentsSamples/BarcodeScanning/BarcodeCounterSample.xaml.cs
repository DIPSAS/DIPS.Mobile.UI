using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning;

namespace Components.ComponentsSamples.BarcodeScanning;

public partial class BarcodeCounterSample
{
    private const int RequiredScanCount = 5;
    private readonly BarcodeScanner m_barcodeScanner;
    private readonly BarcodeScannerStartOptions m_barcodeScannerStartOptions;
    private readonly HashSet<string> m_acceptedBarcodes = [];

    public BarcodeCounterSample()
    {
        InitializeComponent();
        m_barcodeScanner = new BarcodeScanner();
        m_barcodeScannerStartOptions = new BarcodeScannerStartOptions
        {
            Preview = CameraPreview,
            OnCameraFailed = CameraFailed,
            ScanRectangle = new BarcodeScanRectangleOptions
            {
                WidthFraction = 0.8f,
                HeightFraction = 0.3f
            },
            Completion = new BarcodeScanCompletionOptions
            {
                RequiredCount = RequiredScanCount,
                OnCompletedAsync = OnScanCountCompletedAsync
            },
            OnBarcodeAcceptedAsync = HandleBarcodeAcceptedAsync,
            ValidateBarcodeAsync = ValidateBarcodeAsync,
            OnBarcodeRejectedAsync = OnBarcodeRejectedAsync
        };
    }

    private async Task Start()
    {
        try
        {
            await m_barcodeScanner.Start(m_barcodeScannerStartOptions);
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

    private Task HandleBarcodeAcceptedAsync(BarcodeScanResult barcodeScanResult)
    {
        m_acceptedBarcodes.Add(barcodeScanResult.Barcode.RawValue);
        Console.WriteLine($"Accepted barcode: {barcodeScanResult.Barcode.RawValue}");
        return Task.CompletedTask;
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

    private Task OnBarcodeRejectedAsync(BarcodeScanResult barcodeScanResult, BarcodeScanValidationResult validationResult)
    {
        Console.WriteLine($"Rejected barcode: {barcodeScanResult.Barcode.RawValue}. {validationResult.ErrorMessage}");
        return Task.CompletedTask;
    }

    private Task OnScanCountCompletedAsync()
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
