using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace Playground.VetleSamples;

/// <summary>
/// Repro: When navigation bar is hidden in a modal, the status bar text color is not changed.
/// Open this page modally with a NavigationPage wrapper (nav bar hidden) and observe
/// the status bar text remains dark instead of switching to light for the camera background.
/// </summary>
public partial class BarcodeNoNavBarStatusBarRepro
{
    private readonly BarcodeScanner m_barcodeScanner;

    public BarcodeNoNavBarStatusBarRepro()
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
                OnBarcodeAcceptedAsync = HandleBarcodeAcceptedAsync
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

    protected override bool OnBackButtonPressed()
    {
        Close();
        return true;
    }

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
