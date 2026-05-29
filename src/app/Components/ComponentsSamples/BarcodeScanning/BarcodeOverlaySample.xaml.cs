using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace Components.ComponentsSamples.BarcodeScanning;

public partial class BarcodeOverlaySample
{
    private readonly BarcodeScanner m_barcodeScanner;
    private BarcodeScanningResultBottomSheet? m_barCodeResultBottomSheet;
    private View? m_topToolbarView;

    public BarcodeOverlaySample()
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
            
            CameraPreview.AddTopToolbarView(GetTopToolbarView());
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

    private View GetTopToolbarView()
    {
        return m_topToolbarView ??= new VerticalStackLayout
        {
            Spacing = 4,
            Children =
            {
                new Label
                {
                    Text = "Scan barcode",
                    Style = Styles.GetLabelStyle(LabelStyle.UI200),
                    TextColor = Colors.White,
                    HorizontalTextAlignment = TextAlignment.Center
                },
                new Label
                {
                    Text = "Point the camera at a barcode",
                    Style = Styles.GetLabelStyle(LabelStyle.UI100),
                    TextColor = Colors.White,
                    HorizontalTextAlignment = TextAlignment.Center,
                    Opacity = 0.7
                }
            }
        };
    }

    private Task HandleBarcodeAcceptedAsync(BarcodeScanResult barcodeScanResult)
    {
        if (m_barCodeResultBottomSheet is { HasBarCode: true })
        {
            return Task.CompletedTask;
        }

        m_barCodeResultBottomSheet = new BarcodeScanningResultBottomSheet();
        m_barCodeResultBottomSheet.Closed += BottomSheetClosed;
        m_barCodeResultBottomSheet.OpenWithBarCode(barcodeScanResult);
        m_barcodeScanner.PauseScanning(resetOverlay: false);
        return Task.CompletedTask;
    }

    private void BottomSheetClosed(object? sender, EventArgs e)
    {
        if (m_barCodeResultBottomSheet != null)
        {
            m_barCodeResultBottomSheet.Closed -= BottomSheetClosed;
        }

        m_barCodeResultBottomSheet = null;
        m_barcodeScanner.ResumeScanning();
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
