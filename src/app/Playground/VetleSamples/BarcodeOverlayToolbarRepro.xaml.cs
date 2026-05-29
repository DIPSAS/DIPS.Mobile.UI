using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace Playground.VetleSamples;

/// <summary>
/// Repro: Android's GraphicsView overlay spans the entire top and bottom toolbar.
/// Open this page modally with a NavigationPage wrapper and observe the overlay
/// extending behind the navigation bar and bottom safe area on Android.
/// </summary>
public partial class BarcodeOverlayToolbarRepro
{
    private readonly BarcodeScanner m_barcodeScanner;

    public BarcodeOverlayToolbarRepro()
    {
        InitializeComponent();
        m_barcodeScanner = new BarcodeScanner();
    }

    private async Task Start()
    {
        try
        {
            await m_barcodeScanner.Start(new BarcodeScannerStartOptions
            {
                Preview = CameraPreview,
                OnCameraFailed = CameraFailed,
                OnBarcodeAcceptedAsync = HandleBarcodeAcceptedAsync,
                Strategy = new ScanRectangleBarcodeScanStrategy
                {
                    WidthFraction = 0.8f,
                    HeightFraction = 0.3f
                }
            });

            CameraPreview.AddTopToolbarView(new BoxView
            {
                Color = Colors.White,
                HeightRequest = 50,
                WidthRequest = 200,
                HorizontalOptions = LayoutOptions.Center
            });
            CameraPreview.AddBottomToolbarView(CreateCloseButton());
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    private void CameraFailed(CameraException e)
    {
        Console.WriteLine($"Camera failed: {e.Message}");
    }

    private Task HandleBarcodeAcceptedAsync(BarcodeScanResult barcodeScanResult)
    {
        return Task.CompletedTask;
    }

    protected override void OnAppearing()
    {
        _ = Start();
        base.OnAppearing();
    }

    private void Close()
    {
        m_barcodeScanner.StopAndDispose();
        Shell.Current.Navigation.PopModalAsync();
    }

    private void Close(object? sender, EventArgs e) => Close();

    private Button CreateCloseButton()
    {
        var button = new Button
        {
            Text = "Close",
            TextColor = Colors.White,
            BackgroundColor = Colors.Transparent,
            HorizontalOptions = LayoutOptions.Center
        };
        button.Clicked += (_, _) => Close();
        return button;
    }
}
