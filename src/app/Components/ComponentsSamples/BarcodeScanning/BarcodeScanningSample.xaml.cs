using System;
using System.Threading.Tasks;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning;

namespace Components.ComponentsSamples.BarcodeScanning;

public partial class BarcodeScanningSample
{
    private readonly BarcodeScanner m_barcodeScanner;
    private BarcodeScanningResultBottomSheet? m_barCodeResultBottomSheet;

    public BarcodeScanningSample()
    {
        InitializeComponent();
        m_barcodeScanner = new BarcodeScanner();
    }

    
    private async Task Start()
    {
        try
        {
            await m_barcodeScanner.Start(CameraPreview, DidFindBarcode);
        }
        catch (Exception exception)
        {
            await Application.Current?.MainPage?.DisplayAlert("Failed, check console!", exception.Message, "Ok")!;
            Console.WriteLine(exception);
        }
    }

    private void DidFindBarcode(BarcodeScanResult barcodeScanResult)
    {
        if (m_barCodeResultBottomSheet is
            {
                HasBarCode: true
            })
        {
            return;
        }

        m_barCodeResultBottomSheet = new BarcodeScanningResultBottomSheet();
        m_barCodeResultBottomSheet.Closed += BottomSheetClosed;
        m_barCodeResultBottomSheet.OpenWithBarCode(barcodeScanResult);
    }

    private async void BottomSheetClosed(object? sender, EventArgs e)
    {
        if (m_barCodeResultBottomSheet != null)
        {
            m_barCodeResultBottomSheet.Closed -= BottomSheetClosed;
        }

        m_barCodeResultBottomSheet = null;
    }

    protected override async void OnAppearing()
    {
        _ = Start();
        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        m_barcodeScanner.Stop();
        base.OnDisappearing();
    }

    public void OnSleep()
    {
        m_barcodeScanner.Stop();
    }

    public void OnResume()
    {
        _ = Start();
    }

    private void ShowTip(object? sender, EventArgs e)
    {
        CameraPreview.ShowZoomSliderTip("Om strekkoden er liten, er det bedre å bruke zoom funksjonen isteden for å ha mobilen for nært strekkoden.");
    }
}