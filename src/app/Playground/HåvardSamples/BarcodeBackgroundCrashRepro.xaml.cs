using DIPS.Mobile.UI.API.Camera.BarcodeScanning;
using DIPS.Mobile.UI.Components.BottomSheets;

namespace Playground.HåvardSamples;

/// <summary>
/// Reproduction for Arena.Mobile#1573:
/// Android crashes when backgrounding the app while the barcode scanner is active.
///
/// This mimics the Arena.Mobile BarcodeScannerPage lifecycle:
/// - OnAppearing → Start scanning
/// - App backgrounded (Window.Stopped) → StopAndDispose()
/// - App foregrounded (Window.Resumed) → Start() on the same BarcodeScanner instance
///
/// Steps to reproduce:
/// 1. Open this page
/// 2. Scan a barcode (or just let camera run)
/// 3. Background the app (home button or app switcher)
/// 4. Observe crash — RejectedExecutionException from ML Kit tasks
/// </summary>
public partial class BarcodeBackgroundCrashRepro
{
    private readonly BarcodeScanner m_scanner;
    private bool m_hasFoundBarcode;

    public BarcodeBackgroundCrashRepro()
    {
        InitializeComponent();
        m_scanner = new BarcodeScanner();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (Window is not null)
        {
            // Use Deactivated/Activated (= Android OnPause/OnResume) instead of
            // Stopped/Resumed (= OnStop/OnResume). Arena.Mobile's IAppEvents.DidPauseSession
            // fires at OnPause time when CameraX is still running and frames are in-flight.
            // Window.Stopped fires too late — CameraX has already stopped the camera by then.
            Window.Deactivated += OnWindowDeactivated;
            Window.Activated += OnWindowActivated;
        }

        _ = StartScanning();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        StopScanning();

        if (Window is not null)
        {
            Window.Deactivated -= OnWindowDeactivated;
            Window.Activated -= OnWindowActivated;
        }
    }

    private void OnWindowDeactivated(object? sender, EventArgs e)
    {
        // Simulates Arena.Mobile IAppEvents.DidPauseSession() — fires at OnPause,
        // while camera is still active and ML Kit tasks are in-flight
        Console.WriteLine("[BarcodeBackgroundCrashRepro] Window.Deactivated — calling StopScanning()");
        StopScanning();
    }

    private void OnWindowActivated(object? sender, EventArgs e)
    {
        // Simulates Arena.Mobile IAppEvents.DidContinueSession()
        Console.WriteLine("[BarcodeBackgroundCrashRepro] Window.Activated — calling StartScanning()");
        _ = StartScanning();
    }

    private async Task StartScanning()
    {
        try
        {
            await m_scanner.Start(new BarcodeScannerStartOptions
            {
                Preview = CameraPreview,
                OnCameraFailed = e =>
                {
                    Console.WriteLine($"[BarcodeBackgroundCrashRepro] Camera failed: {e.Message}");
                },
                OnBarcodeAcceptedAsync = OnBarcodeAccepted,
            });

            Console.WriteLine("[BarcodeBackgroundCrashRepro] Scanner started");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[BarcodeBackgroundCrashRepro] Start failed: {ex}");
        }
    }

    private Task OnBarcodeAccepted(BarcodeScanResult result)
    {
        if (m_hasFoundBarcode)
            return Task.CompletedTask;

        m_hasFoundBarcode = true;
        m_scanner.PauseScanning(resetOverlay: false);

        Console.WriteLine($"[BarcodeBackgroundCrashRepro] Barcode found: {result.Barcode.RawValue}");

        var sheet = new BottomSheet { Title = "Skanneresultat" };
        sheet.Content = new VerticalStackLayout
        {
            Padding = 16,
            Children =
            {
                new Label
                {
                    Text = $"Strekkode: {result.Barcode.RawValue}",
                    Margin = new Thickness(0, 0, 0, 8)
                },
                new Label { Text = $"Format: {result.Barcode.Format}" },
            }
        };

        sheet.Closed += (_, _) =>
        {
            m_hasFoundBarcode = false;
            m_scanner.ResumeScanning();
        };

        sheet.Open();
        return Task.CompletedTask;
    }

    private void StopScanning()
    {
        Console.WriteLine("[BarcodeBackgroundCrashRepro] StopScanning — calling StopAndDispose()");
        m_hasFoundBarcode = false;
        m_scanner.StopAndDispose();
    }
}
