using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace Components.ComponentsSamples.BarcodeScanning;

public partial class BarcodeTooltipSample
{
    private readonly BarcodeScanner m_barcodeScanner;
    private BarcodeScanningResultBottomSheet? m_barCodeResultBottomSheet;

    public BarcodeTooltipSample()
    {
        InitializeComponent();
        m_barcodeScanner = new BarcodeScanner();
    }

    private async Task Start()
    {
        try
        {
            await m_barcodeScanner.Start(CameraPreview, DidFindBarcode, CameraFailed, settings =>
            {
                settings.ShowScanRectangle = true;
                settings.TooltipView = new Label
                {
                    Text = "Point the camera at a barcode",
                    Style = Styles.GetLabelStyle(LabelStyle.UI200),
                    TextColor = Colors.White,
                    HorizontalTextAlignment = TextAlignment.Center
                };
            });
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
        if (m_barCodeResultBottomSheet is { HasBarCode: true })
        {
            return;
        }

        m_barCodeResultBottomSheet = new BarcodeScanningResultBottomSheet();
        m_barCodeResultBottomSheet.Closed += BottomSheetClosed;
        m_barCodeResultBottomSheet.OpenWithBarCode(barcodeScanResult);
        m_barcodeScanner.StopAndDispose();
    }

    private void BottomSheetClosed(object? sender, EventArgs e)
    {
        if (m_barCodeResultBottomSheet != null)
        {
            m_barCodeResultBottomSheet.Closed -= BottomSheetClosed;
        }

        m_barCodeResultBottomSheet = null;
        _ = Start();
    }

    protected override void OnAppearing()
    {
        _ = Start();
        base.OnAppearing();
    }

    private void Close(object? sender, EventArgs e)
    {
        Shell.Current.Navigation.PopModalAsync();
    }
}
