using DIPS.Mobile.UI.API.Camera.BarcodeScanning;

namespace Components.ComponentsSamples.BarcodeScanning;

public partial class BarcodeScanningResultBottomSheet
{
    public BarcodeScanningResultBottomSheet()
    {
        InitializeComponent();
    }

    public async void OpenWithBarCode(BarcodeScanResult barcodeScanResult)
    {
        await this.Open();
        BarcodeScanResultView.BarcodeScanResult = barcodeScanResult;
        HasBarCode = true;
    }

    public bool HasBarCode { get; private set; }
}