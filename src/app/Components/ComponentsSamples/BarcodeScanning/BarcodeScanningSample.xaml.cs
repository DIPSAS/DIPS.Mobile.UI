using System;
using System.Threading.Tasks;
using DIPS.Mobile.UI.API.Camera;
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
            await m_barcodeScanner.Start(CameraPreview, DidFindBarcode, CameraFailed);
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
        m_barcodeScanner.Stop();
    }

    private async void BottomSheetClosed(object? sender, EventArgs e)
    {
        if (m_barCodeResultBottomSheet != null)
        {
            m_barCodeResultBottomSheet.Closed -= BottomSheetClosed;
        }

        m_barCodeResultBottomSheet = null;
        _ = Start();
    }

    protected override async void OnAppearing()
    {
        _ = Start();
        base.OnAppearing();
    }
    
    private void ShowTip(object? sender, EventArgs e)
    {
        CameraPreview.ShowZoomSliderTip("Om strekkoden er liten, er det bedre å bruke zoom funksjonen istedet for å ha mobilen for nært strekkoden. Du kan også dra i slideren for å justere zoomen.");  
    }
}