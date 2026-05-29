using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Resources.Sizes;

namespace Playground.EirikSamples;

/// <summary>
/// Reproduction for Arena.Mobile#1415:
/// After scanning a barcode and discarding the result (closing the bottom sheet),
/// the camera is visible but barcode detection no longer works.
///
/// Steps:
/// 1. Open this page
/// 2. Scan any barcode/QR code
/// 3. Close the bottom sheet (discard result)
/// 4. Try scanning again — observe that detection does not resume
/// </summary>
public partial class BarcodeScanResumeRepro
{
    private readonly BarcodeScanner m_barcodeScanner;

    public BarcodeScanResumeRepro()
    {
        InitializeComponent();
        m_barcodeScanner = new BarcodeScanner();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = StartScanning();
    }

    protected override void OnDisappearing()
    {
        m_barcodeScanner.StopAndDispose();
        base.OnDisappearing();
    }

    private async Task StartScanning()
    {
        try
        {
            await m_barcodeScanner.Start(new BarcodeScannerStartOptions
            {
                Preview = CameraPreview,
                OnCameraFailed = e => DisplayAlert("Camera failed", e.Message, "OK"),
                OnBarcodeAcceptedAsync = OnBarcodeAccepted,
                Strategy = new ScanRectangleBarcodeScanStrategy
                {
                    WidthFraction = 0.8f,
                    HeightFraction = 0.3f
                }
            });

            // Add top content — simulates a patient banner like SamplingPatientScanRepro
            var patientBanner = new Frame
            {
                BackgroundColor = Color.FromArgb("#333333"),
                CornerRadius = 8,
                Padding = new Thickness(12, 8),
                Margin = new Thickness(Sizes.GetSize(SizeName.size_3), 0),
                VerticalOptions = LayoutOptions.Start,
                Content = new Label
                {
                    Text = "Ola Nordmann \u2014 01019012345",
                    TextColor = Microsoft.Maui.Graphics.Colors.White,
                    FontSize = 14,
                }
            };
            CameraPreview.AddTopToolbarView(patientBanner);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[BarcodeScanResumeRepro] Start failed: {ex}");
        }
    }

    private Task OnBarcodeAccepted(BarcodeScanResult result)
    {
        m_barcodeScanner.PauseScanning(resetOverlay: false);

        var sheet = new BottomSheet { Title = "Scan Result" };
        sheet.Content = new VerticalStackLayout
        {
            Padding = 16,
            Children =
            {
                new Label { Text = $"Barcode: {result.Barcode.RawValue}", Margin = new Thickness(0, 0, 0, 8) },
                new Label { Text = $"Format: {result.Barcode.Format}" }
            }
        };

        sheet.Closed += (_, _) =>
        {
            Console.WriteLine("[BarcodeScanResumeRepro] Sheet closed, calling ResumeScanning()");
            m_barcodeScanner.ResumeScanning();
        };

        sheet.Open();
        return Task.CompletedTask;
    }
}
